namespace RentAds.Parser;

public interface IRentMiner
{
  Task<IReadOnlyCollection<Post>> Collect();
}
