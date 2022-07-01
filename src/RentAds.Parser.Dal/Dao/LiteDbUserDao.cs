using LiteDB;

namespace RentAds.Parser.Dal;

internal class LiteDbUserDao : IUserDao
{
  private const string _dbUser = "./external/User.db";

  public LiteDbUserDao()
  {
    using var db = new LiteDatabase(_dbUser);
    var channels = db.GetCollection<UserDbModel>("users");

    channels.EnsureIndex(x => x.UserId, true);
  }

  public IEnumerable<long> GetUsers()
  {
    using var db = new LiteDatabase(_dbUser);

    return db
      .GetCollection<UserDbModel>("users")
      .FindAll()
      .Select(x => x.UserId)
      .ToArray();
  }

  public void AddUser(long userId)
  {
    using var db = new LiteDatabase(_dbUser);

    var dbCollection = db.GetCollection<UserDbModel>("users");

    if (dbCollection.FindOne(x => x.UserId == userId) is not null)
    {
      return;
    }

    dbCollection.Insert(new UserDbModel { UserId = userId });
  }

  public void DeleteUser(long userId)
  {
    using var db = new LiteDatabase(_dbUser);

    var dbCollection = db.GetCollection<UserDbModel>("users");
    var user = dbCollection.FindOne(x => x.UserId == userId);

    if (user is null)
    {
      return;
    }

    dbCollection.Delete(user.Id);
  }

  internal class UserDbModel
  {
    public int Id { get; set; }
    public long UserId { get; set; }
  }
}
