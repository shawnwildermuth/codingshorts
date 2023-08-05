using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using libc.translation;

namespace JsonLocalization.Web.Localization;

public class TextLocalizer : Localizer
{
  public TextLocalizer() : base(CreateSource())
  {
  }

  private static ILocalizationSource CreateSource()
  {
    var node = new JsonObject();
    var prefix = "JsonLocalization.Web.Localization.";
    var assembly = Assembly.GetExecutingAssembly();
    var sources = assembly.GetManifestResourceNames()
      .Where(n => n.StartsWith(prefix))
      .ToArray();

    foreach (var src in sources)
    {
      using var stream = assembly.GetManifestResourceStream(src);
      if (stream is not null)
      {
        using var reader = new StreamReader(stream);
        var locale = src.Substring(prefix.Length, 2);

        node.Add(locale, JsonNode.Parse(reader.ReadToEnd()));
      }
    }

    var doc = JsonDocument.Parse(node.ToJsonString());
    return new JsonLocalizationSource(doc, PropertyCaseSensitivity.CaseSensitive);
  }
}
