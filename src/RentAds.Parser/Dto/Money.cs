namespace RentAds.Parser;

public record Money(int value, string? currency)
{
  public static Money None = new(default, default);

  public override string ToString() => $"{value}{currency}";
}
