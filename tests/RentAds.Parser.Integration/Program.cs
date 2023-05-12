using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentAds.Parser;
using RentAds.Parser.Telegram;
using TL;

IConfiguration configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  .Build();

var sp = BuildServiceProvider(configuration);

var clientProvider = sp.GetService<IClientProvider>();
var tclient = await clientProvider.Get();

var alldialogs = await tclient.Messages_GetAllDialogs();
var chats = alldialogs.chats;

Console.OutputEncoding = Encoding.UTF8;

var peers = chats
  .Where(c => c.Value.ToInputPeer() is InputPeerChannel)
  .Select(c =>
  {
    var ipc = (InputPeerChannel)c.Value.ToInputPeer();
    var accessHash = ipc.access_hash;
    var id = c.Value.ID;
    var ic = new InputChannel(id, accessHash);
    var title = c.Value.Title;

    return new
    {
      ic,
      id,
      accessHash,
      title
    };
  });

var channels = peers
  .Select(p => 
  {
    try
    {
      var ch = tclient.Channels_GetChannels(new[] { p.ic }).GetAwaiter().GetResult();

      var channel = new Channel(p.ic, p.id, p.title, p.accessHash, ch.chats.Values.ToArray());
      return channel;
    }
    catch
    {
      // private channel exception
      return null;
    }
  })
  .Where(p => p is not null)
  .ToList();

foreach (var ch in channels)
{
  Console.WriteLine($"{ch.Id} :: {ch.Title} :: {ch.AccessHash}");
  foreach (var chat in ch.Chats)
  {
    Console.WriteLine($"- {chat}");
  }
}

Console.ReadLine();

static ServiceProvider BuildServiceProvider(IConfiguration configuration)
{
  var services = new ServiceCollection();

  services.AddSingleton<ILogger, Logger>();
  services.AddSingleton<AppConfiguration>();

  services.Configure<AppConfiguration>(configuration, (o) => o.BindNonPublicProperties = true);

  RentAds.Parser.DependencyInjection.Configure(services);
  RentAds.Parser.Telegram.DependencyInjection.Configure(services);

  return services.BuildServiceProvider();
}

record Channel(InputChannel InputChannel, long Id, string Title, long AccessHash, ChatBase[] Chats);
