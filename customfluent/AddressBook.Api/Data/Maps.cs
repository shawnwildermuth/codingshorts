using Mapster;

namespace AddressBook.Api.Data;

public static class Maps
{
  public static IServiceCollection AddMapsterMaps(this IServiceCollection svcs)
  {
    var config = TypeAdapterConfig.GlobalSettings;

    config.NewConfig<BookEntryModel, BookEntry>()
      .Ignore(e => e.Addresses);

    config.NewConfig<BookEntry, BookEntryLookupModel>()
      .Map(dest => dest.DisplayName, src => DisplayName(src));

    svcs.AddSingleton(config);

    return svcs;
  }

  public static string DisplayName(BookEntry src)
  {
    if (!string.IsNullOrWhiteSpace(src.LastName) &&
            !string.IsNullOrWhiteSpace(src.FirstName) &&
            string.IsNullOrWhiteSpace(src.MiddleName))
    {
      return $"{src.LastName}, {src.FirstName} {src.MiddleName}";
    }
    else if (!string.IsNullOrWhiteSpace(src.LastName) &&
        !string.IsNullOrWhiteSpace(src.FirstName))
    {
      return $"{src.LastName}, {src.FirstName}";
    }
    else if (!string.IsNullOrWhiteSpace(src.LastName))
    {
      return $"{src.LastName}";
    }
    else if (!string.IsNullOrWhiteSpace(src.FirstName))
    {
      return $"{src.FirstName}";
    }
    else if (!string.IsNullOrWhiteSpace(src.CompanyName))
    {
      return $"{src.CompanyName}";
    }
    else
    {
      return "Unnamed";
    }
  }
}
