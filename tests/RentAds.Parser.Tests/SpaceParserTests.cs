using Xunit;

namespace RentAds.Parser.Tests;

public class SpaceParserTests
{
  [Theory]
  [InlineData("10 м.", "10кв.м.")]
  [InlineData("100 м.", "100кв.м.")]
  [InlineData("100 м.кв.", "100кв.м.")]
  [InlineData("100 кв.м.", "100кв.м.")]
  [InlineData("100 кв.", "100кв.м.")]
  [InlineData("9 м.", null)]
  [InlineData("10 м", null)]
  [InlineData("10 кв", null)]
  [InlineData("10кв", null)]
  public void SpaceParser_Parses(string text, string output)
  {
    Assert.Equal(output, SpaceParser.Parse(text)?.ToString());
  }
}

