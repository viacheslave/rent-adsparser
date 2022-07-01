namespace RentAds.Parser;

public interface IUserInteractor
{
  Task SendOutRentals(IEnumerable<Rent> rents);
}
