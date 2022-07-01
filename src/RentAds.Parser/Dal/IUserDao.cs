namespace RentAds.Parser;

public interface IUserDao
{
  void AddUser(long userId);

  void DeleteUser(long userId);

  IEnumerable<long> GetUsers();
}
