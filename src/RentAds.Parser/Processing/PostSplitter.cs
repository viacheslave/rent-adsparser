using System.Text.RegularExpressions;

namespace RentAds.Parser;

internal static class PostSplitter
{
  private static readonly Regex _regex = new Regex("Оренда|Продаж", RegexOptions.Multiline);

  public static IReadOnlyList<Post> Split(Post post)
  {
    if (string.IsNullOrEmpty(post.Message))
    {
      return new[] { post };
    }

    var indices = _regex.Matches(post.Message)
      .Cast<Match>()
      .Select(x => x.Index).ToArray();

    indices = new[] { 0 }.Concat(indices).ToArray();

    var result = new List<Post>();

    for (int i = 0; i < indices.Length; i++)
    {
      var start = indices[i];
      var end = (i == indices.Length - 1) ? post.Message.Length : indices[i + 1];

      if (start != end)
      {
        result.Add(post with { Message = post.Message.Substring(start, end - start) });
      }
    }

    return result;
  }
}
