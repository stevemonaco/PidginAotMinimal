using Pidgin;
using PidginAotMinimal;

string entryInput = "80=ab";
var entry = Working.Parser.Parse(entryInput).Value;
Console.WriteLine($"{entry.Hex}, {entry.Text}");

string labelInput = "[end]";
var label = Broken.Parser.Parse(labelInput).Value;
Console.WriteLine($"{label.Name}");