using Quartz;

namespace RentAds.Parser.Jobs;

[DisallowConcurrentExecution]
public class FetchPostsJob : IJob
{
  private readonly IRentMiner _routines;

  public FetchPostsJob(IRentMiner routines)
  {
    _routines = routines;
  }

  public async Task Execute(IJobExecutionContext context)
  {
    await _routines.Collect();
  }
}
