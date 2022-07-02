using Xunit;

namespace RentAds.Parser.Tests;

public class TypeParserTests
{
  [Theory]
  [InlineData("контент оренда контент")]
  [InlineData("контент Оренда контент")]
  [InlineData("контент сдам контент")]
  [InlineData("контент сдаємо контент")]
  [InlineData("контент сдаю контент")]
  [InlineData("контент здам контент")]
  [InlineData("контент здаємо контент")]
  [InlineData("контент здаю контент")]
  [InlineData("контент пропоную контент")]
  public void TypeParser_IsRent(string text)
  {
    Assert.True(TypeParser.IsRent(text));
  }

  [Theory]
  [InlineData("контент орендую контент")]
  [InlineData("контент продам контент")]
  [InlineData("контент продаж контент")]
  public void TypeParser_IsNotRent(string text)
  {
    Assert.False(TypeParser.IsRent(text));
  }
}

