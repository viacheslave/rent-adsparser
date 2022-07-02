using System;

namespace RentAds.Parser;

internal static class RentBuilder
{
  public static IReadOnlyCollection<Rent> Build(IReadOnlyCollection<Post> posts)
  {
    return posts
      .SelectMany(post => PostSplitter.Split(post))
      .Select(post => Build(post))
      .Where(rent => !rent.IsRejected)
      .OrderBy(rent => rent.Date)
      .ToList();
  }

  private static Rent Build(Post post)
  {
    var isRent = TypeParser.IsRent(post.Message);

    if (!isRent)
    {
      return new Rent { Post = post, IsRejected = true };
    }

    var price = PriceParser.Parse(post.Message);
    var rooms = RoomsParser.Parse(post.Message);
    var address = AddressParser.Parse(post.Message);
    var apartmentComplex = AppartmentComplexParser.Parse(post.Message);
    var space = SpaceParser.Parse(post.Message);

    return new Rent()
    {
      Price = price,
      Rooms = rooms,
      Space = space,
      Address = address,
      AppartmentComplex = apartmentComplex,

      Post = post,
      IsRejected = false
    };
  }
}
