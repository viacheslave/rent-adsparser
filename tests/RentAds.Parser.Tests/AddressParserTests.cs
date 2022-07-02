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

  [InlineData("просп. А. Прізвище")]
  [InlineData("просп. Б. Прізвище")]
  [InlineData("просп. В. Прізвище")]
  [InlineData("просп. Г. Прізвище")]
  [InlineData("просп. Д. Прізвище")]
  [InlineData("просп. Ж. Прізвище")]
  [InlineData("просп. З. Прізвище")]
  [InlineData("просп. І. Прізвище")]
  [InlineData("просп. К. Прізвище")]
  [InlineData("просп. Л. Прізвище")]
  [InlineData("просп. М. Прізвище")]
  [InlineData("просп. Н. Прізвище")]
  [InlineData("просп. О. Прізвище")]
  [InlineData("просп. П. Прізвище")]
  [InlineData("просп. Р. Прізвище")]
  [InlineData("просп. С. Прізвище")]
  [InlineData("просп. Т. Прізвище")]
  [InlineData("просп. У. Прізвище")]
  [InlineData("просп. Ф. Прізвище")]
  [InlineData("просп. Х. Прізвище")]
  [InlineData("просп. Ц. Прізвище")]
  [InlineData("просп. Ч. Прізвище")]
  [InlineData("просп. Ш. Прізвище")]
  [InlineData("просп. Щ. Прізвище")]
  [InlineData("просп. Ю. Прізвище")]
  [InlineData("просп. Я. Прізвище")]

  [InlineData("вул. Кость Левицького")]
  [InlineData("вул. У Левицького")]
  [InlineData("вул. Червоної Калини")]
  [InlineData("вул. Ч.Калини")]
  [InlineData("вул. Під Голоском")]
  [InlineData("вул. Під  Голоском")]
  [InlineData("вул. Янки Н")]
  [InlineData("вул. Олени Степанівни")]
  [InlineData("вул. Лесі Українки")]
  [InlineData("вул. Джорджа Вашингтона")]
  [InlineData("вул. Дж. Вашингтона")]
  [InlineData("вул. Дж.Вашингтона")]
  [InlineData("вул. Богдана Хмельницького")]
  [InlineData("вул. Ак. Павлова")]
  [InlineData("вул. Героїв УПА")]
  [InlineData("вул. Гер. УПА")]
  [InlineData("вул. Кн. Ольги")]
  [InlineData("вул. Княгині Ольги")]
  [InlineData("вул. Володимира Великого")]
  [InlineData("вул. Вол. Великого")]
  public void AddressParser_Parses(string text)
  {
    Assert.Equal(text, AddressParser.Parse(text)?.ToString());
  }

  [Theory]
  [InlineData("вул. Вол. Великого ", "вул. Вол. Великого")]
  [InlineData("вул. Вол. Великого.", "вул. Вол. Великого")]
  [InlineData("вул. Вол. Великого,", "вул. Вол. Великого")]
  [InlineData("вул. Вол. Великого)", "вул. Вол. Великого")]
  public void AddressParser_Parses_Expected_Diff(string text, string expected)
  {
    Assert.Equal(expected, AddressParser.Parse(text)?.ToString());
  }
}

