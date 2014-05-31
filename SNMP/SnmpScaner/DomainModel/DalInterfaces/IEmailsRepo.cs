namespace DomainModel.DalInterfaces
{
    public interface IEmailsRepo
    {
        bool IsEmailExists(string email);
    }
}
