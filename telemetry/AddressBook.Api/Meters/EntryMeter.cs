using System.Diagnostics.Metrics;

namespace AddressBook.Api.Meters;

public class EntryMeter
{
  public Meter Meter { get; private set; }
  public Counter<int> ReadsCounter { get; private set; }
  public static readonly string MeterName = "AddressBook.Meters";

  public EntryMeter()
  {
    Meter = new Meter(MeterName, "1.0.0");
    ReadsCounter = Meter.CreateCounter<int>("entry.reads",
      description: "Counts the number of reads of any entry.");

  }
}
