using MessageApp.DataAccess;
using MessageApp.Models;
using MessageApp.ServiceCommunicator;
using MessageApp.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessageApp.ViewModels
{
    public class NewMessageViewModel : ViewModelBase
    {
        IMessageDataRepository _dataRepository;
        protected Message _message;
        DelegateCommand send;
        DelegateCommand clear;
        protected bool isSending;

        public NewMessageViewModel(IWebCommunicatorAsync<Message> webCommunicator, IMessageDataRepository dataRepository):base(webCommunicator)
        {
            _dataRepository = dataRepository;
            send = new DelegateCommand(CanSendMessage,SendMessage);
            clear = new DelegateCommand(CanClearMessage, ClearMessage);
            _message = new Message();
            CountryCodes = new List<CountryCode>(dataRepository.GetCountryDialingCodes());
            IsSending = false;
        }

        public Message Message
        {
            get { return _message; }
            protected set
            {
                this._message = value;
                RaisePropertychanged("Message");
            }
        }

        public bool IsSending
        {
            get { return isSending; }
            protected set
            {
                this.isSending = value;
                RaisePropertychanged("IsSending");
            }
        }

        public List<CountryCode> CountryCodes { get; protected set; }

        public ICommand Send
        {
            get { return send; }
        }

        public ICommand Clear
        {
            get { return clear; }
        }

        /// <summary> 
        /// This is a top level command function.
        /// </summary>
        private async void SendMessage()
        {
            IsSending = true;
            OperationResultArgs CommandResult;
            bool isError = true;
            string statusText = string.Empty;

            if (Message.Validate())
            {
                var Result = await WebCommunicator.Post(Message);

                statusText = Message.Status.Text;
                if (Result == HttpStatusCode.OK)
                {
                    isError = false;
                    ClearMessage();
                }
            }
            else
            {
                statusText =  "Invalid Message." ;
            }

            CommandResult = new OperationResultArgs
            {
                IsError = isError,
                ResultText = statusText
            };

            _dataRepository.AddMessage(Message);

            RaiseNotification(this, CommandResult);
            IsSending = false;
        }

        private bool CanSendMessage()
        {
            return !IsSending && Message.Validate();
        }

        private void ClearMessage()
        {
            Message = new Message();
        }

        private bool CanClearMessage()
        {
            return !IsSending;
        }
    }
}
