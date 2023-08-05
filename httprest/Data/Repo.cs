using Bogus;

namespace Dealership.Data;

public class Repo
{
    public Repo()
    {

    var lotId = 0;
    var lotNum = 'A';
    var lotGen = new Faker<Lot>()
      .UseSeed(12345)
      .RuleFor(a => a.Id, f => ++lotId)
      .RuleFor(a => a.Name, f => new String(lotNum++, 1));

    var salesId = 0;

    var salesGen = new Faker<Employee>()
      .UseSeed(12345)
      .RuleFor(a => a.Id, f => ++salesId)
      .RuleFor(a => a.FirstName, f => f.Name.FirstName())
      .RuleFor(a => a.LastName, f => f.Name.LastName())
      .RuleFor(a => a.Commission, f => f.Random.Float(0.01f,0.25f))
      .RuleFor(a => a.Picture, f => f.Image.LoremFlickrUrl(240, 320, keywords: "headshot"));

    Employees = salesGen.Generate(15);


    var carId = 0;

    var cars = new Faker<Vehicle>()
      .UseSeed(12345)
      .RuleFor(a => a.Id, f => ++carId)
      .RuleFor(a => a.Make, f => f.Vehicle.Manufacturer())
      .RuleFor(a => a.Model, f => f.Vehicle.Model())
      .RuleFor(a => a.VehicleType, f => f.Vehicle.Type())
      .RuleFor(a => a.Vin, f => f.Vehicle.Vin())
      .RuleFor(a => a.New, f => f.Random.Bool(.7f))
      .RuleFor(a => a.Year, f => f.Random.Int(1940,2023))
      .RuleFor(a => a.Picture, f => f.Image.LoremFlickrUrl(keywords: "Car"))
      .RuleFor(a => a.SalesAssociate, f => f.PickRandom(Employees))
      .RuleFor(a => a.Lot, f => lotGen.Generate());

    Cars = cars.Generate(50);
  }

  public List<Vehicle> Cars { get; set; }
  public List<Employee> Employees { get; set; }
}
