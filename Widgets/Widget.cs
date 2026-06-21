using BurdUISharpClone.Core;
using BurdUISharpClone.Engine;

namespace BurdUISharpClone.Widgets;

public abstract class Widget : UIElement
{
    public bool IsHovered;
    public bool IsClicked;

    public virtual bool Focusable => true;

    protected bool IsFocused => Focus.HasFocus(this);


    public override void Update()
    {
        Focus.Register(this);

        if (TryGetInput(out var keyInfo))
        {
            HandleInput(keyInfo);
        }
    }


    protected virtual void HandleInput(ConsoleKeyInfo keyInfo) // que hace cada widget con la tecla
    {
    }

    protected static bool IsActivationKey(ConsoleKeyInfo keyInfo)
    {
        return keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Spacebar;
    }


    protected bool TryGetInput(out ConsoleKeyInfo key) // centralizamos la comprobacion de focus y tecla
    {
        key = default;

        if (!IsFocused || !Input.LastKey.HasValue)
        {
            return false;
        }

        key = Input.LastKey.Value;
        return true;
    }


    protected void BeginDraw()
    {
        if (IsFocused)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        Console.SetCursorPosition((int)X, (int)Y);
    }


    protected void EndDraw()
    {
        Console.ResetColor();
    }
}