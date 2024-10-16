namespace CleaningUp;

public class Worker : BackgroundService
{
  private readonly ILogger<Worker> _logger;

  public Worker(ILogger<Worker> logger)
  {
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {

      for (var x = 0; x < 1000; ++x)
        for (var y = 0; x < 500; ++y)
        {
          var v = new Vector(x, y);
        }

      await Task.Delay(1000, stoppingToken);
    }
  }

  class Vector(int x, int y) 
  {
    public override string ToString() => $"{x}, {y}";

  }
}
