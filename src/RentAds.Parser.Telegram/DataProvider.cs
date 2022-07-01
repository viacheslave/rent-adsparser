using Microsoft.Extensions.Logging;
using TL;

namespace RentAds.Parser.Telegram;

internal class DataProvider : IDataProvider
{
  private readonly ILogger _logger;
  private readonly IClientProvider _clientProvider;
  private readonly IHistoryDataProvider _historyDataProvider;
  private readonly IChannelsProvider _channelsProvider;

  public DataProvider(
    ILogger logger,
    IClientProvider clientProvider,
    IHistoryDataProvider historyDataProvider,
    IChannelsProvider channelsProvider)
  {
    _logger = logger;
    _clientProvider = clientProvider;
    _historyDataProvider = historyDataProvider;
    _channelsProvider = channelsProvider;
  }

  public async Task WarmUp()
  {
    await _clientProvider.Get();
  }

  public async Task<IReadOnlyList<Post>> GetPosts()
  {
    var channels = _channelsProvider.GetChannels();
    var lastRead = _historyDataProvider.GetLastReadIds().ToDictionary(x => x.Key, x => x.Value);

    var posts = new List<Post>();

    foreach (var channelInfo in channels)
    {
      var lastId = lastRead.GetValueOrDefault(channelInfo.id, long.MaxValue);
      var channelPosts = await GetPosts(channelInfo, lastId);

      if (channelPosts.Count > 0)
      {
        _logger.LogInformation($"Channel {channelInfo.id}: {channelPosts.Count} new posts");

        _logger.LogInformation(
          $"Channel {channelInfo.id}: {lastRead.GetValueOrDefault(channelInfo.id)} -> {channelPosts[0].PostId}" +
          $"(+{channelPosts[0].PostId - lastRead.GetValueOrDefault(channelInfo.id)})");

        lastRead[channelInfo.id] = channelPosts[0].PostId;
      }

      posts.AddRange(channelPosts);
    }

    _historyDataProvider.Save(lastRead);

    return posts;
  }

  private async Task<IReadOnlyList<Post>> GetPosts(ChannelInfo channelInfo, long? lastId)
  {
    var peer = new InputPeerChannel(channelInfo.id, channelInfo.accessHash);
    var posts = new List<Post>();

    var now = DateTime.UtcNow;

    int limit = lastId is null ? 1 : 20;

    var addOffset = 0;

    var client = await _clientProvider.Get();

    while (true)
    {
      var messages = await client.Messages_GetHistory(peer, limit: limit, add_offset: addOffset);

      _logger.LogInformation($"Client call: {peer.channel_id}, limit {limit}, addOffset: {addOffset}");

      var chunk = GetPosts(lastId.GetValueOrDefault(long.MaxValue), messages);
      posts.AddRange(chunk);

      if (lastId is null)
      {
        break;
      }

      if (messages.Messages[^1].ID <= lastId.GetValueOrDefault(long.MaxValue))
      {
        break;
      }

      if (messages.Messages[^1].Date < now.AddHours(-1))
      {
        break;
      }

      addOffset += limit;
    }

    return posts;
  }

  private static IReadOnlyCollection<Post> GetPosts(long lastId, Messages_MessagesBase messages)
  {
    var posts = new List<Post>();

    foreach (var message in messages.Messages)
    {
      if (message.ID == lastId)
      {
        break;
      }

      var post = PostBuilder.Build(message);
      if (post is not null)
      {
        posts.Add(post);
      }
    }

    return posts;
  }
}
