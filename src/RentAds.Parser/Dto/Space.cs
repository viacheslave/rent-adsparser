namespace RentAds.Parser;

public record Space(int value)
{
  public override string ToString() => $"{value}кв.м.";
}
