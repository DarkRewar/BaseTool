# Base Tool
The perfect starter tool to put in your Unity projects!

## Description

## Installation

This tool is made for the Unity package manager. How to install:

- Go to `Window > Package Manager` ;
- Click the plus button-dropdown on the top-left of the window ;
- Select `Add package from git URL` ;
- Paste this URL : `https://github.com/DarkRewar/BaseTool.git`

## Key features

### Dev Console

You can add/remove you own command to the `Console` by using :

```csharp
BaseTool.Core.Consoles.Console.AddCommand("<command>", "<description>", MethodCallback);

BaseTool.Core.Consoles.Console.RemoveCommand("<command>");
```

Here is an implementation inside a `MonoBehaviour`:

```csharp
using BaseTool.Core.Consoles;

public class AddCustomCommand : MonoBehaviour
{
    public void OnEnable()
    {
        Console.AddCommand("<my-command>", "<description>", Callback);
    }
    
    public void OnDisable()
    {
        Console.RemoveCommand("<my-command>");
    }

    private void Callback(ConsoleArguments args)
    {
        Console.Write($"Callback command with {args}");
    }
}
```

The command callback passes a `ConsoleArguments` as parameter. 
This is an handler to parse arguments from the command.

For example, the command `mycommand test 99 -h -number 123` will 
parse arguments like : 

```csharp
args[0]; // test
args[1]; // 99
args["h"]; // null
args["number"]; // 123

// You can check if an argument exists
args.Exists("h"); // true
```