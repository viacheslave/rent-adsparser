namespace RentAds.Parser;

internal interface IUserManager
{
  void AddUser(long userId);

  void DeleteUser(long userId);

  IEnumerable<long> GetUsers();
}
