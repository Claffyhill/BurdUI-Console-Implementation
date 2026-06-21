using BurdUISharpClone.Core;

namespace BurdUISharpClone.Layouts;

public class HorizontalLayout : UIElement
{
    private List<UIElement> children = new();

    public void Add(UIElement element)
    {
        children.Add(element);
    }

    public override void Update()
    {
        float currentX = X;

        foreach (var child in children)
        {
            child.SetPosition(currentX, Y);
            currentX += 18; // separación fija simple entre elementos

            child.Update();
        }
    }

    public override void Draw()
    {
        foreach (var child in children)
        {
            child.Draw();
        }
    }
}