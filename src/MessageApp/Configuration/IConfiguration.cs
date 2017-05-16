using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp.Configuration
{
    public interface IConfiguration
    {
        string GetValue(string key);
    }
}
