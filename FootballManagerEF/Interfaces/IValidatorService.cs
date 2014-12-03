using System;
namespace FootballManagerEF.Interfaces
{
    public interface IValidatorService
    {
        bool DataGridIsValid();
        bool SendErrorToUser();
    }
}
