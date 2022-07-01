namespace RentAds.Parser;

internal class UserManager : IUserManager
{
  private readonly IUserDao _userDao;

  public UserManager(IUserDao userDao)
  {
    _userDao = userDao;
  }

  public void AddUser(long userId) => _userDao.AddUser(userId);

  public void DeleteUser(long userId) => _userDao.DeleteUser(userId);

  public IEnumerable<long> GetUsers() => _userDao.GetUsers();
}
