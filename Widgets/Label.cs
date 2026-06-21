using BurdUISharpClone.Core;
using BurdUISharpClone.Engine;

namespace BurdUISharpClone.Widgets;

public class Label : Widget
{
    public string Text = "";

    public override bool Focusable => false; // El label no es interactivo

    public override void Update()
    {
        base.Update(); // Registrar (aunque no sea focusable)
    }

    public override void Draw()
    {
        Console.WriteLine(Text);
    }
}