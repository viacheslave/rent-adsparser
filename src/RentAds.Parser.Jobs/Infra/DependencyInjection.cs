using Microsoft.Extensions.DependencyInjection;

namespace RentAds.Parser.Jobs;

public static class DependencyInjection
{
  public static void Configure(IServiceCollection services)
  {
    services.AddSingleton<SchedulerManager>();
    services.AddSingleton<FetchPostsJob>();
  }
}
