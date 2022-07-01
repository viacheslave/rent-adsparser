using Xunit;

namespace RentAds.Parser.Tests;

public class AddressParserTests
{
  [Theory]
  [InlineData("вул.Театральна")]
  [InlineData("вул. Театральна")]
  [InlineData("вулиці Театральна")]
  [InlineData("вулиця Театральна")]
  [InlineData("проспект Театральний")]
  [InlineData("проспекті Театральний")]
  [InlineData("просп Театральний")]
  [InlineData("просп. Театральний")]
  [InlineData("вул. Кость Левицького")]
  [InlineData("вул. У Левицького")]
  [InlineData("вул. Червоної Калини")]
  [InlineData("вул. Ч. Калини")]
  [InlineData("вул. Ч.Калини")]
  [InlineData("вул. Під Голоском")]
  [InlineData("вул. Янки Н")]
  [InlineData("вул. Олени Степанівни")]
  [InlineData("вул. Лесі Українки")]
  [InlineData("вул. Л. Українки")]
  [InlineData("вул. Джорджа Вашингтона")]
  [InlineData("вул. Дж. Вашингтона")]
  [InlineData("вул. Дж.Вашингтона")]
  [InlineData("вул. Б. Хмельницького")]
  [InlineData("вул. Богдана Хмельницького")]
  [InlineData("вул. Ак. Павлова")]
  [InlineData("вул. Ш. Руставелі")]
  [InlineData("вул. Ю. Н")]
  [InlineData("вул. Героїв УПА")]
  [InlineData("вул. Гер. УПА")]
  [InlineData("вул. Г. УПА")]
  [InlineData("вул. Кн. Ольги")]
  [InlineData("вул. Княгині Ольги")]
  [InlineData("вул. Володимира Великого")]
  [InlineData("вул. Вол. Великого")]
  [InlineData("вул. В. Великого")]
  public void AddressParser_Parses(string text)
  {
    Assert.Equal(text, AddressParser.Parse(text)?.ToString());
  }
}

