using System.Diagnostics.CodeAnalysis;
using static System.Console;

WriteLine("Starting...");

var braves = new Team()
{
  Name = "Braves",
  //NumberOfPlayers = 26,
  TicketPrice = 89.99m
};

public class Team
{
  // public Team()
  // {

  // }

  // [SetsRequiredMembers]
  // public Team(string name)
  // {
  //   Name = name;
  // }

  public required string Name { get; set; }
  public int NumberOfPlayers { get; set; }
  public required decimal TicketPrice { get; set; }
}