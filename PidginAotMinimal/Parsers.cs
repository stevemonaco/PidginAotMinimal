using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace PidginAotMinimal;

public record Label(string Name, int LineBreaks);

public static class Parsers
{
    private static readonly Parser<char, string> _endLine = String(@"\n");
    private static readonly Parser<char, char> _idChar = Token(IsIdentifierChar);
    private static readonly Parser<char, string> _idParser = _idChar.AtLeastOnceString();

    public static Parser<char, Label> Parser { get; }

    static Parsers()
    {
        // Does not trigger AOT warning
        //Parser = _idParser
        //    .Between(Char('['), Char(']'))
        //    .Select(x => new Label(x, 0));

        // IL3054
        Parser = _idParser
            .Between(Char('['), Char(']'))
            .Then(_endLine.Many(), (t, u) => new Label(t, u.Count()));
    }

    private static bool IsIdentifierChar(char c)
    {
        return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9');
    }
}
