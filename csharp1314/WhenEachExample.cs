using static System.Console;

namespace CSharpNew;

public static class WhenEachExample
{
  public static async Task ShowFileLengths()
  {
    List<string> files = new(Directory.GetFiles("/ourpictures/2025/groningen"));

    var ops = files.Select(f => File.ReadAllBytesAsync(f)).ToArray();

    await foreach (var result in Task.WhenEach(ops))
    {
      if (result.IsCompletedSuccessfully)
      {
        WriteLine(result.Result.Length);
      }
      else
      {
        WriteLine("Failed");
      }
    }
  }
}
