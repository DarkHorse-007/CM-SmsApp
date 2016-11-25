using MessageApp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MessageApp.Configuration;

namespace MessageApp.ServiceCommunicator
{
    public class CMMessagingService : MessageServiceBase
    {
        public CMMessagingService(IMessageFormatter formatter, IConfiguration appSettings)
        {
            this.Formatter = formatter;
            this.Formatter.AddMetadata(ConfigKeys.ProductToken, appSettings.GetValue(ConfigKeys.ProductToken));
            this.GatewayBaseAddress = new Uri(appSettings.GetValue(ConfigKeys.BaseAddress));
            this.PostUri = appSettings.GetValue(ConfigKeys.PostUri);
            this.MediaType = "application/xml";
        }

        public override async Task<HttpStatusCode> Get(IList<Message> param)
        {
            try
            {
                var Result = await Client.GetAsync(GetUri);
                if(Result.IsSuccessStatusCode)
                {
                    string ResultString = await Result.Content.ReadAsStringAsync();
                    JsonConvert.PopulateObject(ResultString, param);
                }
                return Result.StatusCode;
            }
            catch (Exception Expn)
            {
                return HttpStatusCode.ServiceUnavailable;
            }
        }

        public override async Task<HttpStatusCode> Post(Message param)
        {
            try
            {
                string MessageString = Formatter.FormatMessage(param);
                var httpContent = new StringContent(MessageString, Encoding.UTF8, MediaType);
                // Post message.
                var Result = await Client.PostAsync(PostUri,httpContent);
                
                // If status is '200 OK' means message went through sms gateway service.
                if (!Result.IsSuccessStatusCode)
                {
                    param.Status = new DeliveryStatus
                                        {
                                            Code = DeliveryStatusCode.ServerError,
                                            Text = string.Format("Failed to deliver message.{0}{1}", 
                                                                    Environment.NewLine, 
                                                                    Result.ReasonPhrase)
                                        };
                }
                else
                {
                    param.Status = await ProcessResponse(Result);
                }

                return Result.StatusCode;
            }
            catch(Exception Expn)
            {
                param.Status = new DeliveryStatus
                                    {
                                        Code = DeliveryStatusCode.ClientError,
                                        Text = string.Format("Failed to deliver message.{0}Error : {1}", 
                                                                Environment.NewLine, 
                                                                HttpStatusCode.ServiceUnavailable)
                                    };
                return HttpStatusCode.ServiceUnavailable;
            }
        }

        /// <summary>
        /// Process response string returned by CM API and categorise the success, failure and type of error returned.
        /// </summary>
        private async Task<DeliveryStatus> ProcessResponse(HttpResponseMessage Result)
        {
            DeliveryStatusCode StatusCode;
            //If post is successful, Read result string.
            string ResponseString = await Result.Content.ReadAsStringAsync();
            try
            {
                if (string.IsNullOrEmpty(ResponseString))
                {
                    StatusCode = DeliveryStatusCode.Delivered;
                    ResponseString = "Message delivered successfully.";
                }
                else if (ResponseString.Equals("Error: ERROR Invalid product token.") || 
                         ResponseString.ToLower().Contains("rejected") || 
                         ResponseString.ToLower().Contains("unroutable"))
                {
                    StatusCode = DeliveryStatusCode.NotDelivered;
                }
                else if (ResponseString.ToLower().Contains("unknown error"))
                {
                    StatusCode = DeliveryStatusCode.ServerError;
                }
                else
                {
                    StatusCode = DeliveryStatusCode.ClientError;
                }
            }
            catch (Exception Expn)
            {
                StatusCode = DeliveryStatusCode.Undefined;
            }

            return new DeliveryStatus { Code = StatusCode, Text = ResponseString };
        }
    }
}
