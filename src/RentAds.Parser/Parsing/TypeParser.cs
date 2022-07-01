using System.Text.RegularExpressions;

namespace RentAds.Parser;

internal static class TypeParser
{
  private static readonly Regex _rent = new Regex("((оренда)|((з|с)да(ю|є|м))|(пропону))",
    RegexOptions.IgnoreCase | RegexOptions.Multiline);

  public static bool IsRent(string text)
  {
    if (string.IsNullOrEmpty(text))
    {
      return false;
    }

    return _rent.IsMatch(text);
  }
}
