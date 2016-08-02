namespace FootballManagerEF.Interfaces
{
    public interface IMailerService
    {
        bool SendStats();
        bool SendTeams();
        bool SendOKToUser();
        bool SendCancellation();
    }
}
