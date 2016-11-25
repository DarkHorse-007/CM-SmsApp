using MessageApp.DataAccess;
using MessageApp.Models;
using MessageApp.ServiceCommunicator;
using MessageApp.ViewModels;
using System.Windows;
using Dialogs = MessageApp.Dialogs;

namespace MessageApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MessagesViewModel _MessagesVM;
        private NewMessageViewModel _NewMessageVM;

        public MainWindow(IWebCommunicatorAsync<Message> communicator, IMessageDataRepository dataRepository)
        {
            InitializeComponent();
           
            _MessagesVM = new MessagesViewModel(communicator, dataRepository);
            _NewMessageVM = new NewMessageViewModel(communicator, dataRepository);

            _MessagesVM.Notification += ShowPopup;
            _NewMessageVM.Notification += ShowPopup;

            this.MessagesControl.DataContext = _MessagesVM;
            this.NewMessageControl.DataContext = _NewMessageVM;
        }

        protected void ShowPopup(object sender, OperationResultArgs EventArgument)
        {
            Dialogs.MessageBox messageBox = new Dialogs.MessageBox();
            messageBox.Owner = this;
            messageBox.DataContext = EventArgument;

            this.Dispatcher.Invoke(() => messageBox.ShowDialog());
        }
    }
}
