using System.Text.RegularExpressions;

namespace RentAds.Parser;

internal static class AppartmentComplexParser
{
  private static readonly Regex _appartmentComplex = new Regex("(?<ap>жк\\s((\\\".*\\\")|(«.*»)|[^.\\s]{1,}))",
    RegexOptions.IgnoreCase | RegexOptions.Multiline);

  public static string? Parse(string text)
  {
    var match = ParserHelper.ParseSingle(_appartmentComplex, text);
    if (match is null)
    {
      return null;
    }

    var appartmentComplex = match.Groups["ap"].Value;
    return appartmentComplex;
  }
}
