#pragma warning disable CS8321 // Unused Methods
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using AutoMapper;
using MappingTest;
using MappingTest.Entities;
using MappingTest.Fakers;
using MappingTest.Models;
using Mapster;
using static System.Console;

var watch = new Stopwatch();

var numberToGenerate = 1000;
WriteLine($"Generating {numberToGenerate} customers");

watch.Restart();
var customers = GenerateFakeCustomers();
watch.Stop();
WriteLine($"Generation of {customers.Count()} took {watch.ElapsedMilliseconds}ms");

var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CustomerProfile>()));
var mapster = new MapsterMapper.Mapper(new CustomerConfig());

StringBuilder bldr = new();

bldr.AppendLine("Technique,Direction,Items,Milliseconds,Ticks");

Accumulator amTotal = new("Automapper");
Accumulator mpTotal = new("Mapster");
Accumulator exTotal = new("Pure C#");

for (var x = 0; x < 1000; ++x)
{
  TestAutomapper();
  TestMapster();
  TestExtensionMethods();
}

var results = bldr.ToString();
File.WriteAllText("results.csv", results);

amTotal.Dump();
mpTotal.Dump();
exTotal.Dump();

void TestAutomapper()
{
  watch.Restart();
  var models = mapper.Map<IEnumerable<CustomerModel>>(customers);
  watch.Stop();
  amTotal.ToModel.Add(watch.ElapsedTicks);
  ToResult("Automapper", "entities->models", watch.ElapsedMilliseconds, watch.ElapsedTicks);

  watch.Restart();
  var entities = mapper.Map<IEnumerable<Customer>>(models);
  watch.Stop();
  amTotal.FromModel.Add(watch.ElapsedTicks);
  ToResult("Automapper", "models->entities", watch.ElapsedMilliseconds, watch.ElapsedTicks);

  WriteAssert(customers, models, entities);
}

void TestMapster()
{
  watch.Restart();
  var models = mapster.From(customers).AdaptToType<IEnumerable<CustomerModel>>();
  watch.Stop();
  mpTotal.ToModel.Add(watch.ElapsedTicks);
  ToResult("Mapster", "entities->models", watch.ElapsedMilliseconds, watch.ElapsedTicks);

  watch.Restart();
  var entities = mapster.From(models).AdaptToType<IEnumerable<Customer>>();
  watch.Stop();
  mpTotal.FromModel.Add(watch.ElapsedTicks);
  ToResult("Mapster", "models->entities", watch.ElapsedMilliseconds, watch.ElapsedTicks);

  WriteAssert(customers, models, entities);
}

void TestExtensionMethods()
{
  watch.Restart();
  var models = customers.ToModels();
  watch.Stop();
  exTotal.ToModel.Add(watch.ElapsedTicks);
  ToResult("Extensions", "entities->models", watch.ElapsedMilliseconds, watch.ElapsedTicks);

  watch.Restart();
  var entities = mapster.From(models).AdaptToType<IEnumerable<Customer>>();
  watch.Stop();
  exTotal.FromModel.Add(watch.ElapsedTicks);
  ToResult("Extensions", "models->entities", watch.ElapsedMilliseconds, watch.ElapsedTicks);

  WriteAssert(customers, models, entities);
}

void WriteTest(IEnumerable<CustomerModel> models)
{
  var model = models.ElementAt(450);
  var address1 = model.Address1;
  var lineTotal = model.Orders?.First().OrderItems?.First().LineTotal;
  WriteLine(@$"Testing Valid Mapping:
Address1: {address1}
LineTotal: {lineTotal}");
}

void WriteCollection<T>(IEnumerable<T> list)
{
  var options = new JsonSerializerOptions(JsonSerializerOptions.Default);
  options.WriteIndented = true;

  WriteLine(JsonSerializer.Serialize(list, options));
}

void WriteItem<T>(T item)
{
  var options = new JsonSerializerOptions(JsonSerializerOptions.Default);
  options.WriteIndented = true;

  WriteLine(JsonSerializer.Serialize(item, options));
}


List<Customer> GenerateFakeCustomers()
{

  // create shared demo data
  var customers = new CustomerFaker().Generate(numberToGenerate);

  // Once Generated, fix up the customer ID as Bogus doesn't allow parameters
  foreach (var c in customers)
  {
    if (c.Orders is not null)
    {
      foreach (var o in c.Orders)
      {
        o.CustomerId = c.Id;
      }
    }
  }

  return customers!;
}

void ToResult(string technique, string direction, long milliseconds, long ticks)
{
  bldr.AppendLine($"{technique},{direction},{numberToGenerate},{milliseconds},{ticks}");
}

void WriteAssert(List<Customer> customers, IEnumerable<CustomerModel> models, IEnumerable<Customer> entities)
{
  var index = Random.Shared.Next(0, numberToGenerate - 1);
  var customer = customers.ElementAt(index);
  var model = models.ElementAt(index);
  var entity = entities.ElementAt(index);

  if ((customer?.Orders == null) != (model?.Orders == null))
  {
    WriteLine("FAILED: Orders doesn't match");
    WriteLine($"Number: {index}");
    WriteLine($"{customer?.Orders} != {entity.Orders}");
  }
  else if (customer?.Orders?.FirstOrDefault()?.OrderItems?.FirstOrDefault()?.UnitPrice !=
      model?.Orders?.FirstOrDefault()?.OrderItems?.FirstOrDefault()?.UnitPrice)
  {
    WriteLine("FAILED: OrderItem doesn't match");
    WriteLine($"Number: {index}");
    WriteLine($"{customer?.Orders?.FirstOrDefault()?.OrderItems?.FirstOrDefault()?.UnitPrice} != {model?.Orders?.FirstOrDefault()?.OrderItems?.FirstOrDefault()?.UnitPrice}");
  }
  else if (customer?.Address is not null && 
           entity?.Address is not null && 
           customer?.Address?.Address1 != entity.Address?.Address1)
  {
    WriteLine("FAILED: Address doesn't match");
    WriteLine($"Number: {index}");
    WriteLine($"{customer?.Address?.Address1} != {entity.Address?.Address1}");
  }

}

public class Accumulator
{
  public Accumulator(string technique)
  {
    Technique = technique;
  }

  public List<long> ToModel { get; set; } = new();
  public List<long> FromModel { get; set; } = new();
  public string Technique { get; set; } = "";

  public void Dump()
  {
    var normalizedTo = ToModel.ToList();
    normalizedTo.Sort();
    normalizedTo.RemoveAt(0); // first object
    normalizedTo.RemoveAt(normalizedTo.Count() - 1);

    var normalizedFrom = FromModel.ToList();
    normalizedFrom.Sort();
    normalizedFrom.RemoveAt(0); // first object
    normalizedFrom.RemoveAt(normalizedTo.Count() - 1);

    if (normalizedTo.Count() != (ToModel.Count() - 2)) WriteLine("FAILED TO NORMALIZE TO");
    if (normalizedFrom.Count() != (FromModel.Count() - 2)) WriteLine("FAILED TO NORMALIZE FROM");


    WriteLine($"{Technique} - Per Run");
    WriteLine($"  Average   ToModel {(normalizedTo.Average()),6:N0} ticks");
    WriteLine($"  Average FromModel {(normalizedFrom.Average()),6:N0} ticks");
    WriteLine($"  Min/Max   ToModel {(normalizedTo.Min()),6:N0}/{(normalizedTo.Max()),6:N0}");
    WriteLine($"  Min/Max FromModel {(normalizedFrom.Min()),6:N0}/{(normalizedFrom.Max()),6:N0}");
    WriteLine(new string('-', 80));
  }

}
