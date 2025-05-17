
using System.Text;

namespace GrammarTools;

public enum StringLiteralKind
{
    Regular,
    Verbatim,
    Raw
}

public static class StringLiteralTransformer
{
    public static string ToLiteral(string content, StringLiteralKind kind)
    {
        return kind switch
        {
            StringLiteralKind.Regular => EscapeAsRegular(content),
            StringLiteralKind.Verbatim => EscapeAsVerbatim(content),
            StringLiteralKind.Raw => EscapeAsRaw(content),
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };
    }

    private static string EscapeAsRegular(string content)
    {
        var sb = new StringBuilder();
        sb.Append('"');
        foreach (var c in content)
        {
            sb.Append(c switch
            {
                '\' => "\\",
                '"' => "\"",
                '
' => "\n",
                '' => "\r",
                '	' => "\t",
                _ => c.ToString()
            });
        }
        sb.Append('"');
        return sb.ToString();
    }

    private static string EscapeAsVerbatim(string content)
    {
        var escaped = content.Replace(""", """");
        return $"@"{escaped}"";
    }

    private static string EscapeAsRaw(string content)
    {
        var quoteCount = MaxQuoteSequence(content) + 1;
        var quotes = new string('"', quoteCount);
        return $"{quotes}{content}{quotes}";
    }

    private static int MaxQuoteSequence(string content)
    {
        int max = 0, count = 0;
        foreach (var c in content)
        {
            if (c == '"')
            {
                count++;
                max = Math.Max(max, count);
            }
            else
            {
                count = 0;
            }
        }
        return max;
    }
}
