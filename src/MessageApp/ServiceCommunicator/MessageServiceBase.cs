using MessageApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace MessageApp.ServiceCommunicator
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MessageServiceBase : IWebCommunicatorAsync<Message>
    {
        protected IMessageFormatter Formatter;
        protected HttpClient Client;
        protected string MediaType;

        protected string PostUri { get; set; }
        protected string GetUri { get; set; }

        protected Uri GatewayBaseAddress
        {
            set
            {
                Client.BaseAddress = value;
            }
        }

        protected MessageServiceBase()
        {
            Client = new HttpClient();
        }

        public abstract Task<HttpStatusCode> Post(Message param);
        public abstract Task<HttpStatusCode> Get(IList<Message> param);
    }
}
