using static System.Console;
using SampleGenerator;

foreach (var name in ClassNames.Names)
{
  WriteLine(name);
}
public class Foo { }
public class Bar : Foo { }
public class Quux : Bar { }
