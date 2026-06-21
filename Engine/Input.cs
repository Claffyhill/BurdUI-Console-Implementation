namespace BurdUISharpClone.Engine;

public static class Input
{
    public static ConsoleKeyInfo? LastKey;

    public static void Update()
    {
        if (Console.KeyAvailable)
        {
            LastKey = Console.ReadKey(true);
        }
        else
        {
            LastKey = null;
        }
    }
}