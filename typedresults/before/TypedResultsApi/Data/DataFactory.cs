using Bogus;

namespace TypedResultsApi.Data;

public class DataFactory
{
  private List<Person> People;
  private List<Job> Jobs;
  
  public DataFactory()
  {
    var personId = 0;
    var peopleGenerator = new Faker<Person>()
      .UseSeed(32768)
      .RuleFor(p => p.Id, f => personId++)
      .RuleFor(p => p.FullName, f => f.Name.FullName())
      .RuleFor(p => p.Phone, f => f.Phone.PhoneNumber())
      .RuleFor(p => p.Email, f => f.Internet.Email());

    People = peopleGenerator.Generate(25);

    var jobId = 0;
    var jobGenerator = new Faker<Job>()
      .UseSeed(32768)
      .RuleFor(p => p.Id, f => ++jobId)
      .RuleFor(p => p.Title, f => f.Name.JobTitle())
      .RuleFor(p => p.Description, f => f.Lorem.Paragraphs(5))
      .RuleFor(p => p.PostedDate, f => f.Date.Recent(21));

    Jobs = jobGenerator.Generate(100);
  }

  public IEnumerable<Person>? GetAll()
  {
    return People.ToList();
  }

  public Person? Get(int id)
  {
    return People.Find(p => p.Id == id);
  }

  public Person? Create(Person model)
  {
    var person = model;
    person.Id = People.Max(p => p.Id) + 1;
    return person;
  }

  public IEnumerable<Job>? GetAllJobs()
  {
    return Jobs;
  }
}