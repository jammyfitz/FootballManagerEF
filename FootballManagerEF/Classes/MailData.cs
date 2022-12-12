using System.Collections.Generic;

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
