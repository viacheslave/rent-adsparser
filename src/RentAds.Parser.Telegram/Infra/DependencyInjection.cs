using Microsoft.Extensions.DependencyInjection;

namespace RentAds.Parser.Telegram;

public static class DependencyInjection
{
  public static void Configure(IServiceCollection services)
  {
    services.AddSingleton<IUserInteractor, UserInteractor>();
    services.AddSingleton<IClientProvider, ClientProvider>();
    services.AddSingleton<IDataProvider, DataProvider>();
  }
}
