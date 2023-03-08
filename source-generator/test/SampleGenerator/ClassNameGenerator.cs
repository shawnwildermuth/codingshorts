using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SampleGenerator;

[Generator]
public class ClassNameGenerator : IIncrementalGenerator
{
  public void Initialize(IncrementalGeneratorInitializationContext context)
  {

    var provider = context.SyntaxProvider.CreateSyntaxProvider(
      predicate: static (node, _) => node is ClassDeclarationSyntax,
      transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
    )
      .Where(m => m is not null);

    var compilation = context.CompilationProvider.Combine(provider.Collect());

    context.RegisterSourceOutput(compilation,
      static (spc, source) => Execute(source.Left, source.Right, spc));

  }

  static void Execute(Compilation compilation,
    ImmutableArray<ClassDeclarationSyntax> typeList,
    SourceProductionContext context)
  {
    //if (!Debugger.IsAttached) Debugger.Launch();

    StringBuilder bldr = new();

    if (typeList.IsDefaultOrEmpty)
    {
      var desc = new DiagnosticDescriptor(
                "CN0001",
                "No Classes",
                "Cannot locate any classes",
                "Problem",
                DiagnosticSeverity.Warning,
                true);

      context.ReportDiagnostic(Diagnostic.Create(desc, Location.None));

    }
    else
    {
      foreach (var syntax in typeList)
      {

        var symbol = compilation
          .GetSemanticModel(syntax.SyntaxTree)
          .GetDeclaredSymbol(syntax) as INamedTypeSymbol;

        bldr.AppendLine();
        bldr.Append($"    \"{symbol.ToDisplayString()}\",");
      }
    }
 
    if (bldr.Length > 0) bldr.Length--; // remove comma

    var theCode = $$"""
          namespace SampleGenerator;
          public static class ClassNames
          {
            public static List<string> Names = new List<string>() 
            {
              {{bldr.ToString()}}
            };
          }
          """;

    context.AddSource("YourClassesByName.g.cs", theCode);


  }
}


