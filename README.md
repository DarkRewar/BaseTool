# Base Tool

The perfect starter tool to put in your Unity projects!

# Description

# Installation

This tool is made for the Unity package manager and is available for Unity 2021 LTS and further.

How to install:

- Go to `Window > Package Manager` ;
- Click the plus button-dropdown on the top-left of the window ;
- Select `Add package from git URL` ;
- Paste this URL : `https://github.com/DarkRewar/BaseTool.git`
- Or if you want to test the preview : `https://github.com/DarkRewar/BaseTool.git#develop`

# Documentation

1. [Core](#core)
    - [Setup Wizard](#setup-wizard)
    - [Dev Console](#dev-console)
    - [Injector](#injector)
    - [Cooldown](#cooldown)
    - [MonoSingleton](#monosingleton)
    - [ValueListener](#valuelistener)
    - [SerializableDictionary](#serializabledictionary)
    - [GameEvent](#game-events)
    - [Class Extensions](#class-extensions)
    - [Math Utils](#math-utils)
    - [Tree](#tree)
    - [Interfaces](#core-interfaces)
2. [Movement](#movement)
    - [Components](#movement-components)
    - [Interfaces](#movement-interfaces)
3. [Shooter](#shooter)
    - [Sample](#shooter-sample)
    - [Components](#shooter-components)
    - [Interfaces](#shooter-interfaces)
    - [Weapon](#weapon)
4. [RPG](#rpg) [WIP]
5. [Roguelite](#roguelite) [WIP]
6. [UI](#ui)
7. [Editor](#editor)
    - [Todo List](#todo-list)
    - [MinMaxAttribute](#minmaxattribute)
    - [IfAttribute](#ifattribute)
    - [IfNotAttribute](#ifnotattribute)
    - [ReadOnlyAttribute](#readonlyattribute)

## Core

### Setup Wizard

By default, BaseTool include every modules in the project. Each module is an Assembly which can be enabled or disabled using the setup wizard.

To open the setup wizard, go to the topbar and open `Window > BaseTool > Setup`.

`Core` is the main module which is mandatory to let BaseTool work. Other modules are all optional. If you only want essential features, untick every modules.

![setup_wizard](./Documentation~/Wizard/setup_wizard.png)

### Dev Console

You can add/remove you own command to the `Console` by using :

```csharp
BaseTool.Console.AddCommand("<command>", "<description>", MethodCallback);

BaseTool.Console.RemoveCommand("<command>");
```

Here is an implementation inside a `MonoBehaviour`:

```csharp
using BaseTool;

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
using BaseTool;
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

### ValueListener

If you want to use an Observer Pattern for a value, and you don't want
to implement the entire change event handler, the `ValueListener<T>` lets
you do that for you.

You need to declare a `ValueListener<T>` of your type as a field or a property.
I recommend to declare it as readonly to avoid loosing the `OnChanged` event references.

Value is implicitly casted to or from the value type you want. That means you can initialize
your object using the value directly (see following example).

```csharp
using BaseTool;
using UnityEngine;
using UnityEngine.UI;

public class MyComponent : MonoBehaviour
{
    public readonly ValueListener<int> Lifepoints = 100;
    public readonly ValueListener<string> Nickname = new();

    public Text NameLabel;
    public Text LifeLabel;

    public void Start()
    {
        Lifepoints.OnChanged += (oldLife, newLife) => 
            LifeLabel.text = $"{oldLife} -> {newLife}/100";
        Nickname.OnChanged += (_, newName) => 
            NameLabel.text = newName;

        Nickname.Value = "MyName";
        string name = Nickname;
        Debug.Log(name);
    }
} 
```

### SerializableDictionary

You can get dictionaries in inspector by using the `SerializableDictionary` class. It will serialize the dictionary
but also draw it like a extended list.

```csharp
using BaseTool;
using UnityEngine;

public class MyComponent : MonoBehaviour
{
    public SerializableDictionary<string, GameObject> ObjectPool;
}
```

The code above will display this dictionary in the inspector.
It also tells you when two keys already exists in the dictionary.

![serializable_dictionary_drawer](./Documentation~/Editor/serializable_dictionary_drawer.png)

### Game Events

This feature allows you to create custom events using ScriptableObjects.

It is based upon three elements:
- `GameEvent` which is the ScriptableObject that handle the event channel ;
- `GameEventTrigger` which triggers the event on the channel ;
- `GameEventReceiver` which processes actions when the event is triggered in the channel.

What is the purpose of this architecture? Well, it allows you to trigger multiple actions from only one trigger.
For example: the player enters a zone of battle, it will close the door, spawn enemies and play the battle music.

Also, this is really useful for multi-scene game events. It is impossible to reference a gameobject from a scene to another.
That's why subscribing to a SO GameEvent speed up development and let you interoperate events between runtime loaded scenes.
For example: you have two loaded scenes in your level, the player passes a point that enables platforms in another scene.

You can totally inherit from those classes if you want to make custom game event, more specific or with alternate triggers.

#### `GameEvent`

In the Project window, right click in the folder you want to place the game event
and then follow `Create > BaseTool > Events > Game Event`.

#### `GameEventTrigger`

This is the component you should use to trigger game events.
It is recommended to use a `Collider` with this component because 
it depends on `OnTriggerEnter()` and/or `OnCollisionEnter()` Unity calls to work properly.

|Property|Type|Description|
|---|---|---|
|Trigger Once|`bool`|If checked, this component will trigger the event only once.|
|Trigger Type|`GameEventTriggerType`|How the game event will be processed: Trigger, Collision or both.|
|Trigger Tags|`List<string>`|List of authorized tags that will trigger the event.|
|Game Event|`GameEvent`|The game event SO to trigger (optional).|
|Generic Events|`UnityEvent`|Additional callbacks that you can use (optional).|

![game_event_trigger.png](./Documentation~/Core/Events/game_event_trigger.png)

#### `GameEventReceiver`

This is the component you should use to process callbacks from a game event.
You can add it on any elements you want, as long as the objects is active 
(to allow event subscription in the `OnEnable()` method).

|Property|Type|Description|
|---|---|---|
|Game Event|`GameEvent`|The game event SO to listen to.|
|OnTriggered|`UnityEvent`|Additional callbacks that you can use (optional).|

![game_event_receiver.png](./Documentation~/Core/Events/game_event_receiver.png)

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
using BaseTool; 

MathUtils.Modulo(1, 5); // = 1
MathUtils.Modulo(6, 5); // = 1
MathUtils.Modulo(-1, 5); // = 4
MathUtils.Modulo(-3, 5); // = 2
```

### Tree

A generic tree system following a parent/child link. Here is a following example based on a GameObject hierarchy (but would work for a file, UI or node hierarchy too).

```csharp
using BaseTool;
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

### <span id="core-interfaces">Interfaces</span>

#### `IDamageable`

Interface used to expose a component that can take damages (from a hit, an attack or a fall).

```csharp
public interface IDamageable
{
    public void TakeDamages(double damages);
}
```

## Movement

The `Movement` module contains most of components used for movement, jump, camera rotation.
You can enable it, if you want to create one of the following game archetype:

- FPS
- ~~TPS~~
- Platformer
- Arcade
- ~~Top-down~~
- ~~Twin-stick~~

By default, the `Movement` module is enabled but can be disabled in the [Setup Wizard](#setup-wizard).
This module is located under the `BaseTool.Movement` namespace.

### <span id="movement-components">Components</span>

#### `OldMovementInput`

If you are not using the new input system, you can add this component on your player to quickly
setup a player movement based on the old input system.

This component manages a `IMovable` and/or a `IJumpable` component ; if they are found, the inputs are processed and sent to the component.

#### `FirstPersonController`

This component manages a first-person view based on a GameObject/Camera hierarchy.
In this kind a architecture, the component is placed at the root of the player object hierarchy.
Then, the `Camera` is under, as a child. The `FirstPersonController` references the camera,
using `GetComponentInChildren` or referencing it from the inspector.

![first_person_structure](./Documentation~/Movement/first_person_structure.png)

**Caution** : this component requires a `Rigidbody` to work properly and is **not** using the Unity `CharacterController`.

#### `SideViewController`

A light side-view controller. It only manages the movement of the object. The architecture is quite simple, you need to add this component on the element that can move, on its root (recommended).

#### `JumpController`

This component allows any object to jump, with a quick setup.

![jump_controller](./Documentation~/Movement/jump_controller.png)

|Property|Description|
|---|---|
|Rigidbody|The `Rigidbody` of the jumpable element.|
|Jump Force|The velocity to apply when the element needs to jump.|
|Fall Multiplier|The velocity multiplier when the element is falling.|
|Jump Count|The number of allowed jumps.|
|Ground Mask|The `LayerMask` to check when the element touches the ground.|
|Ground Check Offset|The `Vector3` offset if your collision check is not on the ground.|
|Ground Check Size|The radius of the collision check.|
|Coyote Effect Delay|The delay of the coyote effect ; the time allowed to the element to jump even if it is not on the ground anymore.|

### <span id="movement-interfaces">Interfaces</span>

#### `IMovable`

This interface can be use to expose a component as a moving object. It is used to send movement inputs. See [`OldMovementInput`](#oldmovementinput) and [`FirstPersonController`](#firstpersoncontroller) for more information.

```csharp
public interface IMovable
{
    void Move(Vector2 move);

    void Rotate(Vector2 rotation);
}
```

#### `IJumpable`

This interface can be use to expose a component as a jumping object. It is used to send jump inputs. See [`OldMovementInput`](#oldmovementinput) and [`FirstPersonController`](#firstpersoncontroller) for more information.

```csharp
public interface IJumpable
{
    public bool CanJump { get; }

    public void Jump();
}
```

## Shooter

The `Shooter` module contains most of components used for weapons related games.
You can enable it, if you want to create one of the following game archetype:

- FPS
- TPS
- Arcade shooter
- Looter shooter
- RTS

By default, the `Shooter` module is enabled but can be disabled in the [Setup Wizard](#setup-wizard).
This module is located under the `BaseTool.Shooter` namespace.

### <span id="shooter-sample">Sample</span>

The package include a shooter sample project using most of the primary components to begin creating a FPS game.

### <span id="shooter-components">Components</span>

#### `OldShootInput`

Simple component that handles input (from the old input system) and calls `IShootable` shoot and reload
methods. It could be used with the [`ShootController`](#shootcontroller) component as well.

#### `ShootController`

This component can be added from the **AddComponent** menu by following `BaseTool > Shooter > Shoot Controller`. It implements `IShootable` and `IShootController` interfaces. 

It authorizes a GameObject to use a shoot logic and send shoot and reload informations to other components.

#### `WeaponController`

This component can be added from the **AddComponent** menu by following `BaseTool > Shooter > Weapon Controller`.

It is used to update, instantiate and swap weapons.

#### `WeaponSwitcher`

This component can be added from the **AddComponent** menu by following `BaseTool > Shooter > Weapon Switcher`.

#### `WeaponProjectile`

This component can be added from the **AddComponent** menu by following `BaseTool > Shooter > Weapon Projectile`.

It is used to add the projectile behaviour on a GameObject. This is for weapon purpose ; if a weapon must shoot projectiles instead of a raycast, the GameObject must have this component.

### <span id="shooter-interfaces">Interfaces</span>

#### `IShootable`

This interface must be used on a component that can shoot. E.g. the player or enemies. 
It forces the implementation of shooting and reloading method.

```csharp
public interface IShootable
{
    public bool CanShoot { get; }
    public void ShootPressed();
    public void ShootReleased();
    public void Reload();
}
```

#### `IShootController`

This interface must be used by a component that declares and exposes its shooting callbacks.
It is not mandatory but allows other components to understand that some logics could be executed
in the shoot process. For example, when you want a component that triggers animations when it shoots.

```csharp
public interface IShootController
{
    public event Action OnStartShoot;
    public event Action OnStopShoot;
    public event Action OnReload;
}
```

### Weapon

#### `Weapon`

This is the main object used for every weapons. You can create any type of weapon using this base.
To create a new one, right click in your project window, then `Create > BaseTool > Shooter > Weapon`.

#### `WeaponCategory`

This object refers to a category that could be assigned to a weapon. It is used to sort weapons or identify ammos. To create a new one, right click in your project window, then `Create > BaseTool > Shooter > Weapon Category`.

## RPG

[still in development]

## Roguelite

[still in development]

## UI

[coming soon]

## Editor

### Misc Buttons

The package contains useful buttons/links directly accesible from the `Window` menu.
Here are their functions:

- `Window > BaseTool > Documentation` will open the documentation ;
- `Window > BaseTool > Report a bug...` will redirect you to the [issue](https://github.com/DarkRewar/BaseTool/issues) page ;
- `Window > BaseTool > Open Data Folder` will open the `Application.dataPath` in the Explorer/Finder ;
- `Window > BaseTool > Open Persistent Data Folder` will open the `Application.persistentDataPath` in the Explorer/Finder.

### Todo List

If you go to `Window > BaseTool > Todo List`, you can get an editor window that opens.
It will list you every TODO and FIXME entries found in your project.

![todo_list.png](./Documentation~/Editor/todo_list.png)

Entries are grouped by assemblies and can be filtered by tags. You can also by developpers to entries.
How does it work? In your C# script, inside your project, you can add todo and fixme comments. The tool
will detect them and add them to the list. To add some, you must follow those rules:

- start by a comment and a todo, fix or fixme : `//Todo`, `//Fix` or `//Fixme` ;
- (optionnal) you can add meta data between parenthesis:
    - if it begins by `@`, it will be detected as a dev name ;
    - if it begins by `#`, it will be detected as a tag to filter entries ;
- (optionnal) you can add `:` to seperate the begin of comment and content ;
- end with the message of the todo/fix you want to display.

You can see some following examples:

```csharp
//TODO a normal todo
//todo can be case insensitive
//Todo(@MyDeveloperName) : you can add names and punctation
//Todo(@MyName #core #engine #gameplay) : you can also add tags to filter entries
//Fixme : will be displayed
//Fix works like fixme
```

### `MinMaxAttribute`

This attribute allows you to put a slider range for a value in the inspector. It is used to create a range using `Vector2`.

![min_max_attribute](./Documentation~/Editor/min_max_attribute.png)

```csharp
using BaseTool;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    [MinMax(0, 20)]
    public Vector2 MinMaxTest = new(5, 15);

    public bool IsValueInRange(float value) => 
        value.IsBetween(MinMaxTest.x, MinMaxTest.y);
}
```

### `IfAttribute`

This attribute can display its property from inspector only if condition is checked.

```csharp
using BaseTool;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    public bool UseProjectile = true;

    [If(nameof(UseProjectile))]
    public GameObject ProjectilePrefab;
}
```

### `IfNotAttribute`

This attribute can hide its property from inspector only if condition is checked.

```csharp
using BaseTool;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    public bool UseRaycast = true;

    [IfNot(nameof(UseRaycast))]
    public GameObject ProjectilePrefab;
}
```

### `ReadOnlyAttribute`

This attribute can mark the field or property as disabled in inspector (unchangeable).

```csharp
using BaseTool;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    [ReadOnly]
    public int Lifepoints = 10;
}
```