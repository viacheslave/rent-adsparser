using Xunit;

namespace RentAds.Parser.Tests;

public class PostSplitterTests
{
  [Fact]
  public void PostSplitter_Returns_Original()
  {
    var content = "some content";

    var result = PostSplitter.Split(BuildPost(content));

    Assert.Equal(content, result[0].Message);
  }

  [Fact]
  public void PostSplitter_Returns_OriginalMatch()
  {
    var content = "Продаж1 content";

    var result = PostSplitter.Split(BuildPost(content));

    Assert.Equal(content, result[0].Message);
  }

  [Fact]
  public void PostSplitter_Split_EmptyStart()
  {
    var content = "Продаж1 content Оренда 33 Оренда";

    var result = PostSplitter.Split(BuildPost(content));

    var expected = new List<string>
    {
      "Продаж1 content ",
      "Оренда 33 ",
      "Оренда"
    };

    for (int i = 0; i < expected.Count; i++)
    {
      Assert.Equal(result[i].Message, expected[i]);
    }
  }

  [Fact]
  public void PostSplitter_Split_Arbirtary()
  {
    var content = "33 Продаж1 оренда content Оренда 33 Оренда";

    var result = PostSplitter.Split(BuildPost(content));

    var expected = new List<string>
    {
      "33 ",
      "Продаж1 оренда content ",
      "Оренда 33 ",
      "Оренда"
    };

    for (int i = 0; i < expected.Count; i++)
    {
      Assert.Equal(result[i].Message, expected[i]);
    }
  }

  private static Post BuildPost(string message) => new Post(default, default, message, default);
}

