# Base Tool
The perfect starter tool to put in your Unity projects!

# Description

# Installation

This tool is made for the Unity package manager. How to install:

- Go to `Window > Package Manager` ;
- Click the plus button-dropdown on the top-left of the window ;
- Select `Add package from git URL` ;
- Paste this URL : `https://github.com/DarkRewar/BaseTool.git`

# Documentation

1. [Core](#core)
    - [Setup Wizard](#setup-wizard)
    - [Dev Console](#dev-console)
    - [Injector](#injector)
    - [Cooldown](#cooldown)
    - [MonoSingleton](#monosingleton)
    - [Class Extensions](#class-extensions)
    - [Math Utils](#math-utils)
    - [Tree](#tree)
2. [Movement](#movement)
3. [Shooter](#shooter)
4. [RPG](#rpg) [WIP]
5. [Roguelite](#roguelite) [WIP]
6. [UI](#ui)
7. [Editor](#editor)
    - [MinMaxAttribute](#minmaxattribute)

## Core

### Setup Wizard

By default, BaseTool include every modules in the project. Each module is an Assembly which can be enabled or disabled using the setup wizard.

To open the setup wizard, go to the topbar and open `Window > BaseTool > Setup`.

`Core` is the main module which is mandatory to let BaseTool work. Other modules are all optional. If you only want essential features, untick every modules.

![setup_wizard](./Documentation~/Wizard/setup_wizard.png)

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

### MonoSingleton

You can create a singleton `MonoBehaviour` directly by inheriting from the `MonoSingleton`.

```csharp
using BaseTool;

public class MyUniquePlayer : MonoSingleton<MyUniquePlayer>
{
    public int Life = 1;
}

public class GameManager : MonoBehaviour
{
    public void UpdatePlayerLife(int damages)
    {
        MyUniquePlayer.Instance.Life -= damages;
    }
}
```

### Class Extensions

This package contains many class extensions for mainly Unity primary classes. Here are the current extensions:

- [Camera Extensions](./Documentation~/Extensions.md#camera-extensions)
- [Color Extensions](./Documentation~/Extensions.md#color-extensions)
- [List Extensions](./Documentation~/Extensions.md#list-extensions)
- [Number Extensions](./Documentation~/Extensions.md#number-extensions)
- [Random Extensions](./Documentation~/Extensions.md#random-extensions)
- [String Extensions](./Documentation~/Extensions.md#string-extensions)
- [Vector Extensions](./Documentation~/Extensions.md#vector-extensions)

Go to the full documentation : [Class Extensions](./Documentation~/Extensions.md)

### Math Utils

Methods available from the `MathUtils` static class:

#### `Modulo(int index, int count)`

Because `%` is broken on C# when you want to get a negative modulo (e.g. you want the index -1 of an array), this method is a replacement of the symbol. 

```csharp
using BaseTool.Generic.Utils; 

MathUtils.Modulo(1, 5); // = 1
MathUtils.Modulo(6, 5); // = 1
MathUtils.Modulo(-1, 5); // = 4
MathUtils.Modulo(-3, 5); // = 2
```

### Tree

A generic tree system following a parent/child link. Here is a following example based on a GameObject hierarchy (but would work for a file, UI or node hierarchy too).

```csharp
using BaseTool.Generic.Utils;
using UnityEngine;

// Create a tree from a GameObject
Tree<GameObject> cameraTree = new(GameObject.Find("Camera"));
// Add a child to the camera tree
cameraTree.AddChild(GameObject.Find("WeaponRender"));

// Create a tree from a GameObject
Tree<GameObject> tree = new(GameObject.Find("Root"));
// Add a child from a GameObject
tree.AddChild(GameObject.Find("PlayerRender"));
// Add a tree to anothe tree
tree.AddChild(cameraTree);

tree.Parent; // = null
tree.Current; // = Root (GameObject)
cameraTree.Parent; // = tree (Tree<GameObject>)

foreach(Tree<GameObject> child in tree)
{
    child.Parent;
    child.Current;
    child.Children;
}
```

## Movement

[coming soon]

## Shooter

[coming soon]

## RPG

[still in development]

## Roguelite

[still in development]

## UI

[coming soon]

## Editor

### MinMaxAttribute

This attribute allows you to put a slider range for a value in the inspector. It is used to create a range using `Vector2`.

![min_max_attribute](./Documentation~/Editor/min_max_attribute.png)

```csharp
using BaseTool.Generic.Extensions;
using BaseTool.Tools.Attributes;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    [MinMax(0, 20)]
    public Vector2 MinMaxTest = new(5, 15);

    public bool IsValueInRange(float value) => 
        value.IsBetween(MinMaxTest.x, MinMaxTest.y);
}
```