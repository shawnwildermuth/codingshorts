using static System.Console;
using Info = (string Name, string Description);


WriteLine("Hello from C# 12");

//var me = new Person("Shawn", "Wildermuth");

//WriteLine(me.Formatted());

//public class Person(string firstName, string lastName)
//{
//  //public string FirstName => firstName;
//  //public string LastName => lastName;

//  public string Formatted()
//  {
//    return $"{lastName}, {firstName}";
//  }
//}

//public class Manager(string title, string firstName, string lastName) 
//  : Person(firstName, lastName)
//{
//  public string Title => title;
//}

//public record Person
//{
//  public Person(string firstName, string lastName)
//  {
//    FirstName = firstName;
//    LastName = lastName;
//  }

//  public string FirstName { get; init; }
//  public string LastName { get; init; }
//}

Info data = ("Shawn", "recorded video");
OutputInfo(data);

void OutputInfo(Info info)
{
  WriteLine($"{info.Name} - {info.Description}");
}

var write = (string msg = "") => WriteLine(msg);

write("Hello");
write();