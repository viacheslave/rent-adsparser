using Microsoft.Extensions.Logging;
using Serilog;

public class Logger : Microsoft.Extensions.Logging.ILogger
{
  private Serilog.Core.Logger _logger;

  public Logger()
  {
    _logger = new LoggerConfiguration()
      .MinimumLevel.Debug()
      .WriteTo.File("logs/trace.log", rollingInterval: RollingInterval.Day)
      .CreateLogger();
  }

  public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();

  public bool IsEnabled(LogLevel logLevel) => true;

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
  {
    if (state is not Microsoft.Extensions.Logging.Internal.FormattedLogValues values)
    {
      return;
    }

    foreach (var value in values)
    {
      switch (logLevel)
      {
        case LogLevel.Information:
          _logger.Information(value.Value.ToString());
          break;
      }
    }
  }
}
