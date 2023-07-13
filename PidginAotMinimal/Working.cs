using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace PidginAotMinimal;

public record TableEntry(string Hex, string Text);

public static class Working
{
    private static readonly Parser<char, char> _entrySeparator = Char('=').Labelled("Entry separator ('=')");

    private static Parser<char, string> _hexSequenceParser = Token(IsHexDigit)
        .Repeat(2)
        .Select(x => string.Concat(x))
        .AtLeastOnceString()
        .Labelled("Hexadecimal Sequence");

    public static Parser<char, TableEntry> Parser { get; }

    static Working()
    {
        Parser = _hexSequenceParser.Then(_entrySeparator, (t, u) => t)
            .Then(AnyCharExcept('[', ']').Many().Before(End), (t, u) => new TableEntry(t, string.Concat(u)));
    }

    private static bool IsHexDigit(char c)
    {
        return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
    }
}
