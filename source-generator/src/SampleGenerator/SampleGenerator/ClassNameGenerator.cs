using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampleGenerator;

[Generator]
public class ClassNameGenerator : IIncrementalGenerator
{
  public void Initialize(IncrementalGeneratorInitializationContext context)
  {
    var provider = context.SyntaxProvider.CreateSyntaxProvider(
      predicate: (c, _) => c is ClassDeclarationSyntax,
      transform: (n, ct) => n.SemanticModel.GetDeclaredSymbol((ClassDeclarationSyntax)n.Node, ct)?.ToDisplayString())
        .Where(m => m is not null);

    context.RegisterSourceOutput(provider.Collect(), Execute);
  }

  private void Execute(SourceProductionContext context, ImmutableArray<string> classDisplayStrings)
  {
    //if (!Debugger.IsAttached) Debugger.Launch();
    if (classDisplayStrings.Length == 0)
    {
      var desc = new DiagnosticDescriptor(
        "SG0001",
        "No Classes Found",
        "No classes declared in the actual project. Might be a console app.",
        "Problem",
        DiagnosticSeverity.Warning,
        true);

      context.ReportDiagnostic(Diagnostic.Create(desc, Location.None));
    }
    var bldr = new StringBuilder();

    foreach (var classDisplayString in classDisplayStrings)
    {
      bldr.AppendLine();
      bldr.Append($"      \"{classDisplayString}\",");
    }

    if (bldr.Length > 0) bldr.Length--;

    var code = $$"""
      namespace SampleSourceGenerator
      {
        public static class ClassNames
        {
          public static List<string> Names = new List<string>()
          {
            {{bldr.ToString()}}
          };
        }
      }
      """;

    context.AddSource("ClassNames.g.cs", code);
  }
}
