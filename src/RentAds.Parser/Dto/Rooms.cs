namespace RentAds.Parser;

public record Rooms(int value)
{
  public override string ToString() => $"{value}-кімн.";
}
