namespace RentAds.Parser;

public interface IHistoryDataProvider
{
  IReadOnlyDictionary<long, long> GetLastReadIds();

  void Save(IReadOnlyDictionary<long, long> data);
}
