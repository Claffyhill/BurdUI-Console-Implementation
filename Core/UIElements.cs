namespace BurdUISharpClone.Core;
// esto es el corazon del proyecto
public abstract class UIElement
{
    public float X, Y;
    public float Widht, Height;

    public virtual void SetPosition(float x, float y)
    {
        X = x;
        Y = y;
    }

    public abstract void Update(); // logica
    public abstract void Draw(); // "dibujar" en consola

    // Estamos definiendo la posicion, el tamaño 
}