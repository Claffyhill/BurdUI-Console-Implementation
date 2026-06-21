using BurdUISharpClone.Core;
using BurdUISharpClone.Engine;

namespace BurdUISharpClone.Widgets;

public class RadioButton : Widget
{
    private static readonly Dictionary<string, RadioButton> SelectedByGroup = new();

    public string Group = "default";
    public string Text = "";
    public int Id;
    public bool Value;

    protected override void HandleInput(ConsoleKeyInfo keyInfo)
    {
        if (IsActivationKey(keyInfo))
        {
            Select();
        }
    }

    private void Select()
    {
        if (SelectedByGroup.TryGetValue(Group, out var selected) && selected != this)
        {
            selected.Value = false;
        }

        Value = true;
        SelectedByGroup[Group] = this;
    }

    public override void Draw()
    {
        BeginDraw();

        string prefix = IsFocused ? ">" : " ";
        string mark = Value ? "*" : " ";

        Console.WriteLine($"{prefix}[{mark}] {Text}");

        EndDraw();
    }
}