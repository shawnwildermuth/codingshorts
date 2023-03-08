using static System.Console;
using SampleSourceGenerator;

foreach (var className in ClassNames.Names)
{
  WriteLine(className);
}

public class Foo { }
public class Bar { }
public class Quux { }
