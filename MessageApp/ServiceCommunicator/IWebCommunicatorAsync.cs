using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp.ServiceCommunicator
{
    public interface IWebCommunicatorAsync<T>
    {
        Task<HttpStatusCode> Post(T param);
        Task<HttpStatusCode> Get(IList<T> param);
    }
}
