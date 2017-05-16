using MessageApp.DataAccess;
using MessageApp.Models;
using MessageApp.ServiceCommunicator;
using MessageApp.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessageApp.ViewModels
{
    public class MessagesViewModel : ViewModelBase
    {
        DelegateCommand Refresh;
        ObservableCollection<Message> _messageCollection;
        IMessageDataRepository _dataRepository;
        bool isRefreshing;

        public MessagesViewModel(IWebCommunicatorAsync<Message> webCommunicator, IMessageDataRepository dataRepository) : base(webCommunicator)
        {
            Refresh = new DelegateCommand(CanRefreshMessages, RefreshMessageCollection);
            _dataRepository = dataRepository;
            _messageCollection = new ObservableCollection<Message>(_dataRepository.GetAllMessages());
            IsRefreshing = false;
        }

        public ObservableCollection<Message> Messages
        {
            get { return _messageCollection; }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                this.isRefreshing = value;
                RaisePropertychanged("IsRefreshing");
            }
        }

        public ICommand RefreshMessages
        {
            get { return Refresh; }
        }

        private async void RefreshMessageCollection()
        {
            IsRefreshing = true;
            await Task.Run(() =>
                                {
                                    var Result = _dataRepository.GetAllMessages();
                                    if (null != Result)
                                    {
                                        foreach (Message m in Result)
                                        {
                                            if (!Messages.Contains(m))
                                            {
                                                Messages.Add(m);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var CommandResult = new OperationResultArgs
                                        {
                                            IsError = true,
                                            ResultText = string.Format("Failed to get messages from server.{0}Error : {1}", Environment.NewLine, Result.ToString())
                                        };
                                        RaiseNotification(this, CommandResult);
                                    }
                                });
            IsRefreshing = false;
        }

        private bool CanRefreshMessages()
        {
            return true;
        }
    }
}
