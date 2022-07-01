using LiteDB;

namespace RentAds.Parser.Dal;

internal class LiteDbChannelHistoryDao : IChannelHistoryDao
{
  private const string _dbChannelHistory = "./external/ChannelHistory.db";

  public LiteDbChannelHistoryDao()
  {
    using var db = new LiteDatabase(_dbChannelHistory);
    var channels = db.GetCollection<ChannelDbModel>("channels");

    channels.EnsureIndex(x => x.ChannelId, true);
  }

  public IReadOnlyDictionary<long, long> GetChannelHistory()
  {
    using var db = new LiteDatabase(_dbChannelHistory);

    return db
      .GetCollection<ChannelDbModel>("channels")
      .FindAll()
      .ToDictionary(x => x.ChannelId, x => x.PostId);
  }

  public void SaveChannelHistory(IReadOnlyDictionary<long, long> data)
  {
    using var db = new LiteDatabase(_dbChannelHistory);

    var dbCollection = db.GetCollection<ChannelDbModel>("channels");

    foreach (var dataItem in data)
    {
      var dbModel = dbCollection.FindOne(r => r.ChannelId == dataItem.Key);
      if (dbModel is not null)
      {
        dbModel.PostId = dataItem.Value;
        dbCollection.Update(dbModel);
      }
      else
      {
        dbModel = new ChannelDbModel { ChannelId = dataItem.Key, PostId = dataItem.Value };
        dbCollection.Insert(dbModel);
      }
    }
  }

  internal class ChannelDbModel
  {
    public int Id { get; set; }
    public long ChannelId { get; set; }
    public long PostId { get; set; }
  }
}
