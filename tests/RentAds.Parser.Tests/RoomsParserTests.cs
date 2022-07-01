using Xunit;

namespace RentAds.Parser.Tests;

public class RoomsParserTests
{
  [Theory]
  [InlineData("1 км", "1-кімн.")]
  [InlineData("2 км", "2-кімн.")]
  [InlineData("2 кім", "2-кімн.")]
  [InlineData("2-кім", "2-кімн.")]
  [InlineData("2-но кім", "2-кімн.")]
  [InlineData("2-х кім", "2-кімн.")]
  [InlineData("2х кім", "2-кімн.")]
  [InlineData("2 к", null)]
  public void RoomsParser_Parses(string text, string output)
  {
    Assert.Equal(output, RoomsParser.Parse(text)?.ToString());
  }
}
