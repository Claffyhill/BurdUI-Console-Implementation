using BurdUISharpClone.Core;
using BurdUISharpClone.Engine;

namespace BurdUISharpClone.Widgets;

public class TextBox : Widget
{
    public string Text = "";
    public string Placeholder = "";
    public int MaxLength = 32;

    protected override void HandleInput(ConsoleKeyInfo keyInfo)
    {
        var key = keyInfo.Key;

        if (key == ConsoleKey.Backspace)
        {
            if (Text.Length > 0)
            {
                Text = Text[..^1];
            }

            return;
        }

        if (key == ConsoleKey.Enter || key == ConsoleKey.Tab || key == ConsoleKey.Escape)
        {
            return;
        }

        char character = keyInfo.KeyChar;

        if (!char.IsControl(character) && Text.Length < MaxLength)
        {
            Text += character;
        }
    }

    public override void Draw()
    {
        BeginDraw();

        string prefix = IsFocused ? ">" : " ";
        string content = Text;

        if (content.Length == 0 && !string.IsNullOrEmpty(Placeholder))
        {
            content = Placeholder;
        }

        if (IsFocused)
        {
            content += "_";
        }

        Console.WriteLine($"{prefix}[TextBox: {content}]");

        EndDraw();
    }
}