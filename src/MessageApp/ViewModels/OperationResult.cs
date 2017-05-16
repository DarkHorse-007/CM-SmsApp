using System;

namespace MessageApp.ViewModels
{
    public class OperationResultArgs:EventArgs
    {
        public bool IsError { get; set; }
        public string ResultText { get; set; }
    }
}