using MessageApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp.ServiceCommunicator
{
    public interface IMessageFormatter
    {
        void AddMetadata(string key, string value);
        string FormatMessage(Message msg);
    }
}
