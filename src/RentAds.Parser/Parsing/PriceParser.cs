using System.Text.RegularExpressions;

namespace RentAds.Parser;

internal static class PriceParser
{
  private static readonly Regex _uah = new Regex("(?<price>\\d{1,5}(\\s\\d{1,5})?(\\s)?)грн",
    RegexOptions.Multiline | RegexOptions.IgnoreCase);

  private static readonly Regex _usdPost = new Regex("(?<price>\\d{1,5}(\\s\\d{1,5})?(\\s)?)(\\$|дол|уо|у\\.о\\.)",
    RegexOptions.Multiline | RegexOptions.IgnoreCase);

  private static readonly Regex _usdPre = new Regex("\\$(?<price>(\\s)?\\d{1,5}(\\s\\d{1,5})?)",
    RegexOptions.Multiline | RegexOptions.IgnoreCase);

  private static readonly Regex _arbirtary = new Regex("(?<price>\\d{1,2}(\\s?(\\d00|тис)))");

  public static Money? Parse(string text)
  {
    return
      Parse(text, _uah, "грн.") ??
      Parse(text, _usdPost, "$") ??
      Parse(text, _usdPre, "$") ??
      Parse(text, _arbirtary, "грн.") ??
      null;
  }

  private static Money? Parse(string text, Regex regex, string currency)
  {
    var match = ParserHelper.ParseSingle(regex, text);
    if (match is null)
    {
      return null;
    }

    var price = match.Groups["price"].Value;

    price = price.Replace(" ", "");

    if (int.TryParse(price, out var value))
    {
      return new Money(value, currency);
    }

    return null;
  }
}
