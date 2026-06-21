using BurdUISharpClone.Widgets;

namespace BurdUISharpClone.Engine;

public static class Focus
{
    public static List<Widget> Focusables = new();

    public static int Index = -1;


    public static Widget? Current =>
        (Index >= 0 && Index < Focusables.Count)
        ? Focusables[Index]
        : null;


    public static void Register(Widget w)
    {
        if (w.Focusable && !Focusables.Contains(w))
        {
            Focusables.Add(w);

            if (Index == -1)
                Index = 0;
        }
    }


    public static bool HasFocus(Widget w)
    {
        return Current == w;
    }


    public static void Next()
    {
        if (Focusables.Count == 0)
            return;

        Index = (Index + 1) % Focusables.Count;
    }


    public static void Previous()
    {
        if (Focusables.Count == 0)
            return;

        Index--;

        if (Index < 0)
            Index = Focusables.Count - 1;
    }
}