using System.Text.RegularExpressions;

namespace RentAds.Parser;

internal static class SpaceParser
{
  private static readonly Regex _space = new Regex("((?<space>\\d{2,3})\\s?(м([\\s\\.])|кв([\\s\\.])))",
    RegexOptions.IgnoreCase | RegexOptions.Multiline);

  public static Space? Parse(string text)
  {
    var match = ParserHelper.ParseSingle(_space, text);
    if (match is null)
    {
      return null;
    }

    var space = match.Groups["space"].Value;

    if (int.TryParse(space, out var value))
    {
      return new Space(value);
    }

    return null;
  }
}
