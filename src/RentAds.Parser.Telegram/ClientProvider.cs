namespace RentAds.Parser.Telegram;

internal class ClientProvider : IClientProvider
{
  private WTelegram.Client? _client;

  public async Task<WTelegram.Client> Get()
  {
    if (_client is not null)
    {
      return _client;
    }

    _client = new WTelegram.Client(Config);

    await _client.LoginUserIfNeeded();
    return _client;

    static string Config(string what)
    {
      switch (what)
      {
        case "api_id":
        case "api_hash":
        case "phone_number":
        case "verification_code":
        case "password":
          Console.Write($"{what}: ");
          return Read();
        case "session_pathname":
          return "./external/wtelegram.session";
        default:
          // let WTelegramClient decide the default config
          return null;
      }
    }
  }

  private static string Read()
  {
    var pass = string.Empty;
    ConsoleKey key;
    do
    {
      var keyInfo = Console.ReadKey(intercept: true);
      key = keyInfo.Key;

      if (key == ConsoleKey.Backspace && pass.Length > 0)
      {
        Console.Write("\b \b");
        pass = pass[0..^1];
      }
      else if (!char.IsControl(keyInfo.KeyChar))
      {
        Console.Write("*");
        pass += keyInfo.KeyChar;
      }
    } while (key != ConsoleKey.Enter);

    Console.WriteLine();
    return pass;
  }
}
