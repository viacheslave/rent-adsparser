using System.Text.RegularExpressions;

namespace RentAds.Parser;

internal static class AddressParser
{
  private static readonly Regex _address =
    new Regex("((вул(\\.|\\s|иці\\s|иця\\s))|(просп(\\.|\\s|ект\\s|екті\\s)))\\s?(кость|у|червоної|ч\\.|під|янки|олени|лесі|джорджа|дж\\.|б\\.|богдана|ак\\.|ш\\.|л\\.|ю\\.|володимира|вол\\.|в\\.|героїв|гер\\.|г\\.|княгині|кн\\.)?\\s?[^\\s.]*",
    RegexOptions.IgnoreCase | RegexOptions.Multiline);

  public static string? Parse(string text)
  {
    var match = ParserHelper.ParseSingle(_address, text);
    if (match is null)
    {
      return null;
    }

    var address = match.Value;
    return address;
  }
}
