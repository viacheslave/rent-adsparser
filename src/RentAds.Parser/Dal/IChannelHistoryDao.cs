namespace RentAds.Parser;

public interface IChannelHistoryDao
{
  IReadOnlyDictionary<long, long> GetChannelHistory();

  void SaveChannelHistory(IReadOnlyDictionary<long, long> data);
}
