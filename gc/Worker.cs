using System.Data.Common;

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

  record struct Vector(int x, int y) //: IDisposable
  {
        DbConnection? conn = null;
        //private bool disposedValue;

        // Unmanaged Resources?
        // Db Connections, File handles, and sockets
        //public void Dispose()
        //{
        //    conn?.Dispose();
        //}

        public override string ToString() => $"{x}, {y}";

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: dispose managed state (managed objects)
        //            conn?.Dispose();
        //        }

        //        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        //        // TODO: set large fields to null
        //        disposedValue = true;
        //    }
        //}

        //// TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ////~Vector()
        ////{
        ////    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        ////    Dispose(disposing: false);
        ////}

        //public void Dispose()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: true);
        //    //GC.SuppressFinalize(this);
        //}
    }
}
