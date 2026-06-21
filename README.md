# BurdUI# Console Implementation

A lightweight console-based UI framework inspired by the original [BurdUI#](https://github.com/davidespano/burdui-sharp). This project implements a complete widget and layout system for building text-based user interfaces in .NET.

## Features

- **6+ Widget Types**: Button, Checkbox, Label, TextBox, RadioButton, Slider
- **2 Layout Systems**: VerticalLayout, HorizontalLayout
- **Focus Management**: Tab/Arrow key navigation between widgets
- **Real-time Input**: Responsive console input with minimal latency
- **Event System**: Click handlers and input callbacks
- **Clean Architecture**: Object-oriented design with inheritance and composition

## Project Structure

```
BurdUISharpClone/
├── Core/
│   └── UIElements.cs          # Base UIElement class
├── Engine/
│   ├── Input.cs               # Input handling system
│   └── Focus.cs               # Focus management system
├── Layouts/
│   ├── VerticalLayout.cs      # Vertical widget stacking
│   └── HorizontalLayout.cs    # Horizontal widget arrangement
├── Widgets/
│   ├── Widget.cs              # Base Widget class
│   ├── Button.cs              # Clickable button with feedback
│   ├── Checkbox.cs            # Toggleable checkbox
│   ├── Label.cs               # Static text display
│   ├── TextBox.cs             # Text input field
│   ├── RadioButton.cs         # Exclusive selection widget
│   └── Slider.cs              # Numeric slider control
├── Program.cs                 # Demo application
└── BurdUISharpClone.csproj    # Project configuration
```

## Requirements

- **.NET 10.0** or higher
- **C# 12.0** or higher
- Console with ANSI color support (Windows 10+, macOS, Linux)

## Installation

### Clone the Repository

```bash
git clone https://github.com/Claffyhill/BurdUI-Console-Implementation.git
cd BurdUI-Console-Implementation
```

### Build the Project

```bash
dotnet build
```

### Run the Demo

```bash
dotnet run
```

## Usage

### Creating a Simple UI

```csharp
using BurdUISharpClone.Layouts;
using BurdUISharpClone.Widgets;

class Program
{
    static void Main()
    {
        // Create root layout
        var root = new VerticalLayout();

        // Add widgets
        var label = new Label { Text = "Hello, BurdUI#!" };
        var button = new Button { Text = "Click me", Id = 1 };
        var textBox = new TextBox { Placeholder = "Enter text..." };

        root.Add(label);
        root.Add(button);
        root.Add(textBox);

        // Main loop
        while (true)
        {
            Console.Clear();
            Input.Update();
            root.Update();
            root.Draw();
            System.Threading.Thread.Sleep(16); // ~60 FPS
        }
    }
}
```

## Widgets

### Button
Clickable button that triggers `OnClick` action when pressed (Enter/Space).

```csharp
var button = new Button
{
    Text = "Click me!",
    Id = 1
};
button.OnClick = () => Console.WriteLine("Button clicked!");
```

**Activation Key:** `Enter` or `Space`

### Checkbox
Toggleable boolean state widget.

```csharp
var checkbox = new Checkbox
{
    Id = 2
};
// Access state with: checkbox.Value (true/false)
```

**Display:**
- `[ ]` - Unchecked
- `[X]` - Checked

**Activation Key:** `Enter` or `Space`

### Label
Static text display (non-focusable).

```csharp
var label = new Label
{
    Text = "Display only"
};
```

### TextBox
Text input field with placeholder and max length.

```csharp
var textBox = new TextBox
{
    Placeholder = "Type here...",
    MaxLength = 20
};
// Access input with: textBox.Text
```

**Features:**
- Type characters
- Backspace to delete
- Cursor indicator when focused

### RadioButton
Exclusive selection within a group (only one can be selected).

```csharp
var radio1 = new RadioButton
{
    Id = 4,
    Text = "Option A",
    Group = "mode"
};

var radio2 = new RadioButton
{
    Id = 5,
    Text = "Option B",
    Group = "mode"  // Same group = exclusive
};
```

**Display:**
- `[ ] Option` - Unselected
- `[*] Option` - Selected

**Activation Key:** `Enter` or `Space`

### Slider
Numeric slider with range 0.0 to 1.0.

```csharp
var slider = new Slider { Id = 3 };
// Use Left/Right arrows to adjust
// Access value with: slider.Value (0.0 - 1.0)
```

## Layouts

### VerticalLayout
Stacks widgets vertically (column-based).

```csharp
var layout = new VerticalLayout();
layout.Add(widget1);
layout.Add(widget2);
layout.Add(widget3);
```

**Spacing:** Automatically adds 2 lines between widgets.

### HorizontalLayout
Arranges widgets horizontally (row-based).

```csharp
var row = new HorizontalLayout();
row.Add(radio1);
row.Add(radio2);
root.Add(row);
```

**Spacing:** 18 character units between widgets.

## Navigation

| Key | Action |
|-----|--------|
| `Tab` | Move focus to next widget |
| `↓ Down Arrow` | Move focus to next widget |
| `↑ Up Arrow` | Move focus to previous widget |
| `Enter` / `Space` | Activate focused widget |
| `Backspace` | Delete character in TextBox |

## Architecture

### Class Hierarchy

```
UIElement (abstract)
├── Widget (abstract)
│   ├── Button
│   ├── Checkbox
│   ├── Label
│   ├── TextBox
│   ├── RadioButton
│   └── Slider
└── Layout
    ├── VerticalLayout
    └── HorizontalLayout
```

### Core Systems

#### Input Engine (`Engine/Input.cs`)
- Reads console input asynchronously
- Stores last key press in `Input.LastKey`
- Prevents input buffering with single read per frame

#### Focus Management (`Engine/Focus.cs`)
- Maintains list of focusable widgets
- Tracks current focused widget
- Provides navigation methods: `Next()`, `Previous()`
- Prevents duplicate registrations

#### Widget Base Class (`Widgets/Widget.cs`)
- Implements `Update()` → `HandleInput()` → `Draw()` pattern
- Provides centralized input checking
- Offers `IsFocused` property
- Handles color rendering helpers

## Design Patterns

### Template Method Pattern
Each widget inherits from `Widget` and implements:
- `HandleInput(ConsoleKeyInfo)` - Define widget-specific input handling
- `Draw()` - Define widget-specific rendering

### Composite Pattern
Layouts contain widgets or other layouts, creating a tree structure.

### Observer Pattern
Widgets use callbacks (`OnClick` for Button) to notify parent code of events.

## Performance Considerations

- **Frame Rate:** ~60 FPS with `Thread.Sleep(16)` milliseconds
- **Input Latency:** Minimal (~16ms) for responsive typing in TextBox
- **Console Clear:** Full redraw each frame (can be optimized with dirty rectangles)

## Extending the Framework

### Creating a Custom Widget

```csharp
using BurdUISharpClone.Widgets;

public class ProgressBar : Widget
{
    public float Progress = 0.5f;
    
    protected override void HandleInput(ConsoleKeyInfo keyInfo)
    {
        if (IsActivationKey(keyInfo))
        {
            Progress += 0.1f;
            if (Progress > 1.0f) Progress = 0f;
        }
    }
    
    public override void Draw()
    {
        string prefix = IsFocused ? ">" : " ";
        int filled = (int)(Progress * 20);
        string bar = new string('█', filled) + new string('░', 20 - filled);
        Console.WriteLine($"{prefix}[{bar}] {Progress:P0}");
    }
}
```

### Creating a Custom Layout

```csharp
using BurdUISharpClone.Core;

public class GridLayout : UIElement
{
    private List<UIElement> children = new();
    private int columns = 2;
    
    public void Add(UIElement element) => children.Add(element);
    
    public override void Update()
    {
        float currentX = X;
        float currentY = Y;
        
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetPosition(currentX, currentY);
            children[i].Update();
            
            currentX += 20;
            if ((i + 1) % columns == 0)
            {
                currentX = X;
                currentY += 3;
            }
        }
    }
    
    public override void Draw()
    {
        foreach (var child in children)
            child.Draw();
    }
}
```

## Demo Application

The included `Program.cs` demonstrates:
- Creating a layout hierarchy
- Adding multiple widget types
- Handling user input
- Responding to widget events
- Managing focus navigation

Run it to see the framework in action:

```bash
dotnet run
```

## Known Limitations

- Console-based rendering (no graphics)
- Single-threaded (input blocks rendering in some scenarios)
- No advanced styling beyond ANSI colors
- Widgets limited to console width/height
- No clipboard support for TextBox

## Future Enhancements

- [ ] Dirty rectangle optimization (partial redraw)
- [ ] Async input handling
- [ ] More widget types (ComboBox, ListBox, etc.)
- [ ] Themes and styling system
- [ ] XML-based layout definition
- [ ] Unit tests
- [ ] Documentation wiki

## Credits

- Inspired by [BurdUI#](https://github.com/davidespano/burdui-sharp) by David Espanó
- Console UI patterns based on traditional TUI frameworks

## License

This project is open source and available for educational purposes.

## Author

Created as a university project exploring UI framework design in C#.

## Support

For issues, questions, or suggestions, open an issue on the [GitHub repository](https://github.com/Claffyhill/BurdUI-Console-Implementation).

---

**Happy UI building with BurdUI# Console! 🎉**
