
using Xunit;

namespace GrammarTools.Tests;

public class StringLiteralTransformerTests
{
    [Theory]
    [InlineData("Hello", "\"Hello\"", StringLiteralKind.Regular)]
    [InlineData("He said \"Hi\"", "\"He said \\\"Hi\\\"\"", StringLiteralKind.Regular)]
    [InlineData("Line\nBreak", "\"Line\\nBreak\"", StringLiteralKind.Regular)]
    [InlineData("Hello", "@\"Hello\"", StringLiteralKind.Verbatim)]
    [InlineData("He said "Hi"", "@\"He said ""Hi""\"", StringLiteralKind.Verbatim)]
    [InlineData("No quotes", """"No quotes"""", StringLiteralKind.Raw)]
    [InlineData("Multiple "" quotes", """"Multiple "" quotes"""", StringLiteralKind.Raw)]
    [InlineData("Quote """ inside", """""Quote """ inside""""", StringLiteralKind.Raw)]
    public void TransformsCorrectly(string input, string expected, StringLiteralKind kind)
    {
        var actual = StringLiteralTransformer.ToLiteral(input, kind);
        Assert.Equal(expected, actual);
    }
}
