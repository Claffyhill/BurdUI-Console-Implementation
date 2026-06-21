using BurdUISharpClone.Core;
using BurdUISharpClone.Engine;

namespace BurdUISharpClone.Widgets;

public class Slider : Widget
{
    public int Id;
    public float Value = 0f; // 0 - 1

    protected override void HandleInput(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.RightArrow)
        {
            Value += 0.1f;
            if (Value > 1f) Value = 0f;
        }
        else if (keyInfo.Key == ConsoleKey.LeftArrow){
            Value -= 0.1f;
            if (Value < 0f) Value = 0f;
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

        int bar = (int)(Value * 10);
        Console.Write($"{prefix}[Slider {Id}] ");

        for (int i = 0; i < 10; i++)
        {
            Console.Write(i < bar ? "█" : "-");
        }

        Console.WriteLine(" " + Value.ToString("0.0"));
        Console.ForegroundColor = previousColor;
    }
}