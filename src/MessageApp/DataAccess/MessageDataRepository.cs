using MessageApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageApp.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageDataRepository : IMessageDataRepository
    {
        MessageDataContext DataContext;

        public MessageDataRepository()
        {
            DataContext = new MessageDataContext();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AddMessage(Message message)
        {
            try
            {
                DataContext.Messages.Add(message);
                DataContext.SaveChanges();
                return true;
            }
            catch(Exception Expn)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<Message> GetAllMessages()
        {
            try
            {
                var data = from m in DataContext.Messages.Include("Status")
                           select m;

                return data.ToList();
            }
            catch(Exception expn)
            {
                return null;
            }
        }

        public IList<CountryCode> GetCountryDialingCodes()
        {
            return DataContext.CountryDialingCodes.ToList<CountryCode>();
        }
    }
}
