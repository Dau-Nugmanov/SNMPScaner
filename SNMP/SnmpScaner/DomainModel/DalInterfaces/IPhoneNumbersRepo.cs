namespace DomainModel.DalInterfaces
{
    public interface IPhoneNumbersRepo
    {
        bool IsPhoneNumberExists(string number);
    }
}
