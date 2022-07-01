using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentAds.Parser;
using RentAds.Parser.Jobs;

Console.WriteLine("Press 'q' to exit");

IConfiguration configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  .Build();

var sp = BuildServiceProvider(configuration);

var schedulerManager = sp.GetRequiredService<SchedulerManager>();
await schedulerManager.Setup(sp);

await sp.GetRequiredService<IDataProvider>().WarmUp();

while (Console.ReadLine() != "q") { }

await schedulerManager.Shutdown();

Console.WriteLine("Exiting"); 

static ServiceProvider BuildServiceProvider(IConfiguration configuration)
{
  var services = new ServiceCollection();

  services.AddSingleton<ILogger, Logger>();
  services.AddSingleton<AppConfiguration>();

  services.Configure<AppConfiguration>(configuration, (o) => o.BindNonPublicProperties = true);

  RentAds.Parser.DependencyInjection.Configure(services);
  RentAds.Parser.Jobs.DependencyInjection.Configure(services);
  RentAds.Parser.Dal.DependencyInjection.Configure(services);
  RentAds.Parser.Telegram.DependencyInjection.Configure(services);

  return services.BuildServiceProvider();
}
