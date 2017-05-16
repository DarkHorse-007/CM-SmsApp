using MessageApp.Configuration;
using MessageApp.DataAccess;
using MessageApp.Models;
using MessageApp.ServiceCommunicator;
using MessageApp.Views;
using System.Windows;

namespace MessageApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IWebCommunicatorAsync<Message> communicator = new CMMessagingService(new CMMessageFormatter(), new ApplicationConfiguration());
            IMessageDataRepository dataRepository = new MessageDataRepository();

            MainWindow window = new MainWindow(communicator, dataRepository);
            this.MainWindow = window;
            window.Show();
        }
    }
}
