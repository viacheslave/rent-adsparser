using Xunit;

namespace RentAds.Parser.Tests;

public class AppartmentComplexParserTests
{
  [Theory]
  [InlineData("ЖК Авалон", "ЖК Авалон")]
  [InlineData("ЖК \"Велика Оселя\"", "ЖК \"Велика Оселя\"")]
  [InlineData("ЖК «Велика Оселя»", "ЖК «Велика Оселя»")]
  public void AppartmentComplexParser_Parses(string text, string output)
  {
    Assert.Equal(output, AppartmentComplexParser.Parse(text)?.ToString());
  }
}

