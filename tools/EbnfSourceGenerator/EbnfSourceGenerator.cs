
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace EbnfGen;

[Generator]
public class EbnfSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var ebnfFiles = context.AdditionalTextsProvider
            .Where(file => file.Path.EndsWith(".ebnf"))
            .Select((text, _) => text);

        context.RegisterSourceOutput(ebnfFiles, (spc, file) =>
        {
            var content = file.GetText(spc.CancellationToken)?.ToString() ?? "";
            var name = Path.GetFileNameWithoutExtension(file.Path);
            var generatedCode = GenerateParserStub(name, content);
            spc.AddSource($"{name}_Parser.g.cs", SourceText.From(generatedCode, Encoding.UTF8));
        });
    }

    private static string GenerateParserStub(string name, string ebnf)
    {
        return $@"
namespace Generated.{name};

public static class {name}Parser
{{
    // This is a stub generated from .ebnf file '{name}.ebnf'
    public static void Parse(string input)
    {{
        // TODO: Parse according to grammar rules
    }}
}}";
    }
}
