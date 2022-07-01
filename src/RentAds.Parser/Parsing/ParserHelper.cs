using System.Text.RegularExpressions;

namespace RentAds.Parser;

internal static class ParserHelper
{
  public static Match? ParseSingle(Regex regex, string text)
  {
    if (string.IsNullOrEmpty(text))
    {
      return null;
    }

    var matches = regex.Matches(text);
    if (matches.Count == 0)
    {
      return null;
    }

    var match = matches[0];
    if (!match.Success)
    {
      return null;
    }

    return match;
  }
}
