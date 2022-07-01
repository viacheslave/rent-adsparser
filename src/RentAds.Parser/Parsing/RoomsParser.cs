using System.Text.RegularExpressions;

namespace RentAds.Parser;

internal static class RoomsParser
{
  private static readonly Regex _rooms = new Regex("((?<rooms>\\d{1})([\\s\\-\\w]*)?к(і)?м)",
    RegexOptions.IgnoreCase | RegexOptions.Multiline);

  public static Rooms? Parse(string text)
  {
    var match = ParserHelper.ParseSingle(_rooms, text);
    if (match is null)
    {
      return null;
    }

    var rooms = match.Groups["rooms"].Value;

    if (int.TryParse(rooms, out var value))
    {
      return new Rooms(value);
    }

    return null;
  }
}
