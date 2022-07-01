using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RentAds.Parser.Telegram;

internal class UserInteractor : IUserInteractor, IDisposable
{
  private readonly CancellationTokenSource _cts;
  private readonly TelegramBotClient _bot;
  private readonly ILogger _logger;
  private readonly AppConfiguration _appConfiguration;
  private readonly IUserDao _userDao;

  public UserInteractor(ILogger logger, IOptions<AppConfiguration> appConfiguration, IUserDao userDao)
  {
    _logger = logger;
    _appConfiguration = appConfiguration.Value;
    _userDao = userDao;

    var receiverOptions = new ReceiverOptions()
    {
      AllowedUpdates = Array.Empty<UpdateType>(),
      ThrowPendingUpdates = true,
    };

    _cts = new CancellationTokenSource();

    _bot = new TelegramBotClient(_appConfiguration.BotToken);

    _bot.StartReceiving(
      updateHandler: HandleUpdateAsync,
      pollingErrorHandler: PollingErrorHandler,
      receiverOptions: receiverOptions,
      cancellationToken: _cts.Token);
  }

  private async Task PollingErrorHandler(ITelegramBotClient client, Exception ex, CancellationToken ctx)
  {
    //return Task.CompletedTask;
  }

  private async Task HandleUpdateAsync(ITelegramBotClient client, Update upd, CancellationToken ctx)
  {
    if (upd.Message?.Entities?.Length > 0 && upd.Message.Entities[0].Type == MessageEntityType.BotCommand)
    {
      var from = upd.Message.From;
      if (from is null)
      {
        return;
      }

      var chatId = new ChatId(from.Id);

      switch (upd.Message.Text)
      {
        case "/subscribe":
          _userDao.AddUser(from.Id);
          _logger.LogInformation($"Subscribed {from.Id}");
          
          await _bot.SendTextMessageAsync(chatId, "You have been subscribed to the updated");
          break;

        case "/unsubscribe":
          _userDao.DeleteUser(from.Id);
          _logger.LogInformation($"Unsubscribed {from.Id}");
          
          await _bot.SendTextMessageAsync(chatId, "You have been unsubscribed to the updated");
          break;

        case "/test":

          var rent = new Rent
          {
            Post = new Post(23, 56, "Test Message", DateTime.Now),
            Price = new Money(5000, "грн."),
            Rooms = new Rooms(2),
            Space = new Space(57),
            Address = "вул. Червоної Калини",
            AppartmentComplex = "ЖК Сихів"
          };

          await SendOutRentals(
            BuildPayload(new[] { rent, rent }),
            chatId);

          break;
      }
    }
  }

  public async Task SendOutRentals(IEnumerable<Rent> rents)
  {
    var payload = BuildPayload(rents);

    foreach (var user in _userDao.GetUsers())
    {
      var chatId = new ChatId(user);
      await SendOutRentals(payload, chatId);
    }
  }

  private async Task SendOutRentals(string payload, ChatId chatId)
  {
    await _bot.SendTextMessageAsync(chatId, payload, ParseMode.Html);

    _logger.LogInformation($"@{chatId.Identifier} received a message");
  }

  private static string BuildPayload(IEnumerable<Rent> rents)
  {
    var messages = rents.Select(BuildMessage).ToArray();
    return string.Join('\n', messages);
  }

  private static string BuildMessage(Rent rent)
  {
    var message =
@$"
{rent}
<a href='{rent.Permalink}'>{rent.Id} ::: {rent.Date}</a>";

    return message;
  }

  public void Dispose() => _cts.Cancel();
}
