using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace PidginAotMinimal;

public record Label(string Name, int LineBreaks);

public static class Broken
{
    private static readonly Parser<char, string> _endLine = String(@"\n").Labelled("End of Line ('\\n')");
    private static readonly Parser<char, char> _idChar = Token(IsIdentifierChar).Labelled("Identifier character ([0-9][a-z][A-Z])");
    private static readonly Parser<char, string> _idParser = _idChar.AtLeastOnceString().Labelled("Id");

    public static Parser<char, Label> Parser { get; }

    static Broken()
    {
        Parser = _idParser
            .Between(Char('[').Labelled("Open ('[')"), Char(']').Labelled("Close (']')"))
            .Then(_endLine.Many(), (t, u) => new Label(t, u.Count()));
    }

    private static bool IsIdentifierChar(char c)
    {
        return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9');
    }
}
