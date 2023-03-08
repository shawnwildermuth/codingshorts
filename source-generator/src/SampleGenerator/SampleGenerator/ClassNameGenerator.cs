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
      transform: (n, _) => (ClassDeclarationSyntax)n.Node)
        .Where(m => m is not null);

    var compilation = context.CompilationProvider.Combine(provider.Collect());

    context.RegisterSourceOutput(compilation,
      (spc, source) => Execute(spc, source.Left, source.Right));
  }

  private void Execute(SourceProductionContext context,
    Compilation compilation,
    ImmutableArray<ClassDeclarationSyntax> typeList)
  {
    //if (!Debugger.IsAttached) Debugger.Launch();
    if (typeList.Length == 0)
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

    foreach (var syntax in typeList)
    {
      var symbol = compilation
        .GetSemanticModel(syntax.SyntaxTree)
        .GetDeclaredSymbol(syntax) as INamedTypeSymbol;

      if (symbol is not null)
      {
        bldr.AppendLine();
        bldr.Append($"      \"{symbol.ToDisplayString()}\",");
      }
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
