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

To toggle the dev console, press F4 key.

![image](https://github.com/DarkRewar/BaseTool/assets/7771426/98bfceaa-62d0-45e9-8cc4-f087e2f19a5c)

### Injector

You can "automatically" retrieve your components by using the `Injector.Process()` method.

To get your `Awake()`, `OnEnable()`, `Start()` or anything else clean, you can add attributes upon fields and properties you want to retrieve. You can use one of those five attributes following their exact method:
- `GetComponent`
- `GetComponents`
- `GetComponentInChildren`
- `GetComponentsInChildren`
- `GetComponentInParent`

```csharp
using BaseTool.Tools;
using BaseTool.Tools.Attributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MyComponent : MonoBehaviour
{
    [GetComponent, SerializeField]
    private Rigidbody _rigidbody;

    [GetComponent]
    public Rigidbody Rigidbody { get; private set; }

    [GetComponentInChildren]
    public Collider ChildCollider;

    [GetComponentsInChildren]
    public Collider[] ChildrenColliders;

    [GetComponentInParent]
    public Transform ParentTransform;

    void Awake() => Injector.Process(this);
}
```

### Cooldown

`Cooldown` is a class that can be used to delay a call, action or whatever you want to do. You can directly check the `Cooldown.IsReady` boolean or subscribe to the `Cooldown.OnReady` event.

```csharp
using BaseTool;
using UnityEngine;

public class MyComponent : MonoBehaviour
{
    [SerializeField]
    private Cooldown _cooldown = 2;

    void Start()
    {
        // Event method
        _cooldown.OnReady += OnCooldownIsReady;
    }

    void Update()
    {
        // In both way, you need to update the cooldown
        _cooldown.Update();

        // Update boolean check method
        if (_cooldown.IsReady)
        {
            _cooldown.Reset();
            // Do something when cooldown is ready
        }
    }

    private void OnCooldownIsReady()
    {
        // Do something when cooldown is ready
    }
}
```