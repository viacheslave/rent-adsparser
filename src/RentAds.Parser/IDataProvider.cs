namespace RentAds.Parser;

public interface IDataProvider
{
  Task WarmUp();
  Task<IReadOnlyList<Post>> GetPosts();
}
