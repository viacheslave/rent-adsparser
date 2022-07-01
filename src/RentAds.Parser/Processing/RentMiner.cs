using Microsoft.Extensions.Logging;

namespace RentAds.Parser;

internal class RentMiner : IRentMiner
{
  private readonly ILogger _logger;
  private readonly IDataProvider _dataProvider;
  private readonly IUserInteractor _rentSender;

  public RentMiner(ILogger logger, IDataProvider dataProvider, IUserInteractor rentSender)
  {
    _logger = logger;
    _dataProvider = dataProvider;
    _rentSender = rentSender;
  }

  public async Task<IReadOnlyCollection<Post>> Collect()
  {
    var posts = await _dataProvider.GetPosts();
    var rentals = RentBuilder.Build(posts);

    await _rentSender.SendOutRentals(rentals);

    foreach (var rental in rentals)
    {
      _logger.LogInformation(
        $"Rental {rental.Post.ChannelId}::{rental.Post.PostId} [{rental.Date}]: {rental}. " +
        $"Original Post: {rental.Post.Message}");
    }

    return posts;
  }
}
