namespace RentAds.Parser;

public record Post
{
  public string Message { get; init; }
  public DateTime Date { get; }

  public long PostId { get; }
  public long ChannelId { get; }

  public long OriginalPostId { get; private set; }
  public long OriginalChannelId { get; private set; }

  public bool HasOriginal { get; private set; }

  public (long, long) Identity => HasOriginal
    ? (OriginalPostId, OriginalChannelId)
    : (PostId, ChannelId);

  public Post(long postId, long channelId, string message, DateTime date)
  {
    PostId = postId;
    ChannelId = channelId;
    Message = message;
    Date = date;
  }

  public void SetOriginal(long postId, long channelId)
  {
    HasOriginal = true;

    OriginalPostId = postId;
    OriginalChannelId = channelId;
  }

  public override string ToString()
  {
    return $"{Date} :: {ChannelId} :: {PostId} :: {Message}";
  }
}
