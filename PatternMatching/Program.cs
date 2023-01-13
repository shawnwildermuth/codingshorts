using static System.Console;

Console.Clear();

Object? m = "Hello World";

ShowMe(new[] { Status.Planning, Status.Testing });

void ShowMe(Status[] statuses)
{
  string theMsg = statuses switch 
  {
    [_, Status.Testing] => "Testing and Planning",
    [_, Status.Working] => "Working but Complete",
    _ => "Not Valid"  
  };

  WriteLine(theMsg);
}


#region Types
record ProjectStatus(Status status, int daysBehind);

enum Status
{
  Planning,
  Working,
  Testing,
  Complete
}
#endregion