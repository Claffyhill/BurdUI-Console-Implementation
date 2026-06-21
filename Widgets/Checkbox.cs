using BurdUISharpClone.Core;
using BurdUISharpClone.Engine;

namespace BurdUISharpClone.Widgets;

public class Checkbox : Widget
{
    public bool Value;
    public int Id;

    protected override void HandleInput(ConsoleKeyInfo keyInfo)
    {
        if (IsActivationKey(keyInfo))
        {
            Value = !Value;
        }
    }

    public override void Draw()
    {
        string prefix = IsFocused ? ">" : " ";
        var previousColor = Console.ForegroundColor;

        if (IsFocused)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        Console.WriteLine($"{prefix}[Checkbox {Id}: {(Value ? "X" : " ")}]");
        Console.ForegroundColor = previousColor;
    }
}