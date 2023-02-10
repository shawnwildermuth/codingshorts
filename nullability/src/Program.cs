using static System.Console;

//#nullable enable
string? x = "";

User<string> user = new("Hello");

WriteLine(user.Name!.Length);

WriteLine(x is null ? "Null" : "Not Null");

class User<T> where T : notnull
{
  public int Id { get; set; }
  public T? Name { get; set; }

  public User(T name)
  {
    Name = name;
  }
}

//#nullable disable