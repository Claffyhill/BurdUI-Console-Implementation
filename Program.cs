
using BurdUISharpClone.Layouts;
using BurdUISharpClone.Widgets;
using BurdUISharpClone.Engine;

class Program
{
    static void Main()
    {
        var root = new VerticalLayout();
        var label = new Label
        {
            Text = "BurdUI# Demo"
        };

        var button = new Button
        {
            Text = "Click me!!",
            Id = 1
        };

        var textBox = new TextBox
        {
            Placeholder = "Type here...",
            MaxLength = 20
        };

        var radioOne = new RadioButton
        {
            Id = 4,
            Text = "Option A",
            Group = "mode"
        };

        var radioTwo = new RadioButton
        {
            Id = 5,
            Text = "Option B",
            Group = "mode"
        };

        var radioRow = new HorizontalLayout();
        radioRow.Add(radioOne);
        radioRow.Add(radioTwo);

        var checkboxOne = new Checkbox
        {
            Id = 2
        };

        var checkboxTwo = new Checkbox
        {
            Id = 6
        };

        var slider = new Slider { Id = 3 };

        root.Add(label);
        root.Add(button);
        root.Add(textBox);
        root.Add(radioRow);
        root.Add(checkboxOne);
        root.Add(checkboxTwo);
        root.Add(slider);

        while (true)
        {
            Console.Clear();

            Input.Update();
            root.Update(); // logica de los widgets
            root.Draw(); // render en consola

            if (Input.LastKey.HasValue)
            {
                var key = Input.LastKey.Value.Key;

                if (key == ConsoleKey.Tab || key == ConsoleKey.DownArrow)
                {
                    Focus.Next();
                }

                if (key == ConsoleKey.UpArrow)
                {
                    Focus.Previous();
                }
            }

            System.Threading.Thread.Sleep(16);
        }

    }
}