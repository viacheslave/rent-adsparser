using Xunit;

namespace RentAds.Parser.Tests;

public class PriceParserTests
{
  [Theory]
  [InlineData("10000грн.", "10000грн.")]
  [InlineData("10000грн", "10000грн.")]
  [InlineData("10000 грн.", "10000грн.")]
  [InlineData("10000 грн", "10000грн.")]
  [InlineData("10 000 грн", "10000грн.")]
  [InlineData("12 500 грн", "12500грн.")]
  [InlineData("12500 грн", "12500грн.")]
  [InlineData("12500грн", "12500грн.")]
  [InlineData("12500грн.", "12500грн.")]
  public void PriceParser_Parses_UAH(string text, string output)
  {
    Assert.Equal(output, PriceParser.Parse(text).ToString());
  }

  [Theory]
  [InlineData("1000$", "1000$")]
  [InlineData("1000 $", "1000$")]
  [InlineData("1 000 $", "1000$")]
  [InlineData("1 000$", "1000$")]
  [InlineData("$1000", "1000$")]
  [InlineData("$ 1000", "1000$")]
  [InlineData("$1 000", "1000$")]
  [InlineData("$ 1 000", "1000$")]
  [InlineData("1000 дол", "1000$")]
  [InlineData("1000 уо", "1000$")]
  [InlineData("1000 у.о.", "1000$")]
  public void PriceParser_Parses_USD(string text, string output)
  {
    Assert.Equal(output, PriceParser.Parse(text).ToString());
  }

  [Theory]
  [InlineData("10000", "10000грн.")]
  [InlineData("10 000", "10000грн.")]
  [InlineData("12500", "12500грн.")]
  [InlineData("12 500", "12500грн.")]
  public void PriceParser_Parses_UAH_NoCurrency(string text, string output)
  {
    Assert.Equal(output, PriceParser.Parse(text)?.ToString());
  }
}
