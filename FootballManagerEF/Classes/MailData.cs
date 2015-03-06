using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManagerEF.Interfaces
{
    public interface IMailHelper
    {
        string GetFromAddress();
        List<string> GetToAddresses();
        string GetSubject();
        string GetBody();
    }
}
