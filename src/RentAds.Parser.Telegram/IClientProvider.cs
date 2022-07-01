namespace RentAds.Parser.Telegram;

public interface IClientProvider
{
  Task<WTelegram.Client> Get();
}
