namespace RentAds.Parser;

public class Rent
{
  public Money? Price { get; init; }
  public Rooms? Rooms { get; init; }
  public Space? Space { get; init; }
  public string? Address { get; init; }
  public string? AppartmentComplex { get; init; }

  public Post Post { get; init; } = null!;
  public bool IsRejected { get; init; }

  public string Id => $"{Post.ChannelId} ::: {Post.PostId}";
  public DateTime Date => Post.Date;

  public string? Permalink => $"https://t.me/c/{Post.ChannelId}/{Post.PostId}";

  public override string ToString()
  {
    var parts = new object[] { $"<u>{Price}</u>", Rooms, Space, Address, AppartmentComplex }
      .Where(o => o is not null)
      .Select(o => o.ToString());

    return string.Join(' ', parts);
  }
}
