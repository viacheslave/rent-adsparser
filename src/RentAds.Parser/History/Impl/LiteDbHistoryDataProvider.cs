namespace RentAds.Parser;

internal class LiteDbHistoryDataProvider : IHistoryDataProvider
{
  private readonly IChannelHistoryDao _historyDao;

  public LiteDbHistoryDataProvider(IChannelHistoryDao historyDao)
  {
    _historyDao = historyDao;
  }

  public IReadOnlyDictionary<long, long> GetLastReadIds() => _historyDao.GetChannelHistory();

  public void Save(IReadOnlyDictionary<long, long> data) => _historyDao.SaveChannelHistory(data);
}
