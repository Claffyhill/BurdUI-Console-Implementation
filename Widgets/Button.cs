using BurdUISharpClone.Core;
using BurdUISharpClone.Engine;


namespace BurdUISharpClone.Widgets;

public class Button : Widget
{
    public string Text = ""; // estado
    public Action? OnClick; // evento
    public int Id;
    public string? RightText;
    private DateTime? RightTextUntilUtc;

    protected override void HandleInput(ConsoleKeyInfo keyInfo)
    {
        if (IsActivationKey(keyInfo))
        {
            RightText = "BUTTON PRESSED!";
            RightTextUntilUtc = DateTime.UtcNow.AddSeconds(1);
            OnClick?.Invoke();
        }
    }

    public override void Draw()
    {
        BeginDraw();

        if (RightTextUntilUtc.HasValue && DateTime.UtcNow >= RightTextUntilUtc.Value)
        {
            RightText = null;
            RightTextUntilUtc = null;
        }

        string prefix = IsFocused ? ">" : " ";
        string suffix = string.IsNullOrWhiteSpace(RightText) ? "" : $"   {RightText}";
        var previousColor = Console.ForegroundColor;

        Console.Write($"{prefix}[Button: {Text}]");

        if (!string.IsNullOrWhiteSpace(suffix))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(suffix);
            Console.ForegroundColor = previousColor;
        }

        Console.WriteLine();

        EndDraw();
    }
}