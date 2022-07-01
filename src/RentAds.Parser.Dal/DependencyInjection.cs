using Microsoft.Extensions.DependencyInjection;

namespace RentAds.Parser.Dal;

public static class DependencyInjection
{
  public static void Configure(IServiceCollection services)
  {
    services.AddSingleton<IUserDao, LiteDbUserDao>();
    services.AddSingleton<IChannelHistoryDao, LiteDbChannelHistoryDao>();
  }
}
