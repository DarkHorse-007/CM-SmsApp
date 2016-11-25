using MessageApp.Models;
using MessageApp.ServiceCommunicator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp.ViewModels
{
    public delegate void Notify(Object Sender, OperationResultArgs EventArgument);

    public class ViewModelBase : INotifyPropertyChanged
    {
        protected OperationResultArgs opResult;
        protected bool notify;
        protected IWebCommunicatorAsync<Message> WebCommunicator; 
        public event PropertyChangedEventHandler PropertyChanged;
        public event Notify Notification;

        public ViewModelBase(IWebCommunicatorAsync<Message> webCommunicator)
        {
            WebCommunicator = webCommunicator;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void RaiseNotification(Object Sender, OperationResultArgs EventArgument)
        {
            if(Notification != null)
            {
                Notification(Sender, EventArgument);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertychanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
