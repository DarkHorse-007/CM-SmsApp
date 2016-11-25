using MessageApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageApp.DataAccess
{
    public interface IMessageDataRepository
    {
        IList<Message> GetAllMessages();
        bool AddMessage(Message message);
        IList<CountryCode> GetCountryDialingCodes();
    }
}
