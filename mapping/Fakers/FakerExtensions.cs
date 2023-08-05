using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace MappingTest.Fakers;

public static class FakerExtensions
{
  public static T FromContext<T>(this Faker faker, string key)
  {
    var fakerContext = faker as IHasContext;
    return (T)fakerContext.Context[key];
  }

  public static Faker<T> WithContext<T>(this Faker<T> fakerT, string propertyName, object value) where T : class
  {
    var internals = fakerT as IFakerTInternal;
    var faker = internals.FakerHub;
    var fakerContext = faker as IHasContext;
    fakerContext.Context[propertyName] = value;
    return fakerT;
  }
}