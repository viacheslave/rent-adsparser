using Quartz;

namespace RentAds.Parser.Jobs;

/// <summary>
///  Quartz Scheduler manager
/// </summary>
public class SchedulerManager
{
  private IScheduler _scheduler;
  private TriggerKey _key;

  private static readonly TimeSpan _syncAlertsJobInterval = TimeSpan.FromMinutes(5);

  public async Task Setup(IServiceProvider sp)
  {
    _scheduler = await SchedulerBuilder.Create(
        new System.Collections.Specialized.NameValueCollection())
      .BuildScheduler();

    _scheduler.JobFactory = new SchedulerJobFactory(sp);

    await _scheduler.Start();

    var job = JobBuilder.Create<FetchPostsJob>()
      .WithIdentity(nameof(FetchPostsJob), nameof(FetchPostsJob))
      .Build();

    var trigger = TriggerBuilder.Create()
      .WithIdentity(nameof(FetchPostsJob), nameof(FetchPostsJob))
      .StartNow()
      .WithSimpleSchedule(x => x
        .WithInterval(_syncAlertsJobInterval)
        .RepeatForever())
      .Build();

    _key = trigger.Key;

    await _scheduler.ScheduleJob(job, trigger);
  }

  public async Task Shutdown()
  {
    await _scheduler.UnscheduleJob(_key);

    await _scheduler.Shutdown();
  }
}
