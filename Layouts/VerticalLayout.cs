using System.Runtime.CompilerServices;
using BurdUISharpClone.Core;

namespace BurdUISharpClone.Layouts;

// Esta es una interfaz modo arbol

public class VerticalLayout : UIElement
{
    private List<UIElement> children = new(); // guarda hijos
    public void Add(UIElement element)
    {
        children.Add(element);
    }

    public override void Draw() // propaga draw
    {
        foreach (var child in children)
        {
            child.Draw();
        }
    }

    public override void Update()
    {
        float currentY = Y;

        foreach (var child in children)
        {
            child.SetPosition(X, currentY);
            currentY += 2; // Deja una línea de separación entre widgets
            child.Update();
        }
    }
}