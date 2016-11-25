using System;
using System.Windows.Input;

namespace MessageApp.ViewModels.Commands
{
    delegate bool MyPredecate();
    delegate void MyAction();

    /// <summary>
    /// 
    /// </summary>
    class DelegateCommand : ICommand
    {
        MyPredecate canExecute;
        MyAction execute;

        public DelegateCommand(MyPredecate canExec, MyAction exec)
        {
            this.canExecute = canExec;
            this.execute = exec;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
              return canExecute();
        }

        public void Execute(object parameter)
        {
              execute();
        }
    }
}
