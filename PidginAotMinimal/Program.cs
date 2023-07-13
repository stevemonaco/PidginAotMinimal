using Pidgin;
using PidginAotMinimal;

string labelInput = @"[end]\n";
var label = Parsers.Parser.Parse(labelInput).Value;
Console.WriteLine($"{label.Name} {label.LineBreaks}");