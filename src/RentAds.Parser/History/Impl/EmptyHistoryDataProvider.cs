namespace RentAds.Parser;

internal class EmptyHistoryDataProvider : IHistoryDataProvider
{
  public IReadOnlyDictionary<long, long> GetLastReadIds()
  {
    return new Dictionary<long, long>();
  }

  public void Save(IReadOnlyDictionary<long, long> data)
  {
  }
}
