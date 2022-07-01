using Microsoft.Extensions.DependencyInjection;

namespace RentAds.Parser;

public static class DependencyInjection
{
  public static void Configure(IServiceCollection services)
  {
    services.AddSingleton<IChannelsProvider, ChannelsProvider>();
    services.AddSingleton<IRentMiner, RentMiner>();
    services.AddSingleton<IUserManager, UserManager>();

    //services.AddSingleton<IHistoryDataProvider, EmptyHistoryDataProvider>();
    services.AddSingleton<IHistoryDataProvider, LiteDbHistoryDataProvider>();

    services.AddSingleton<RentMiner>();
  }
}
