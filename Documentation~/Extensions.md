# Class Extensions

Table of contents:
- [Camera Extensions](#camera-extensions)
- [Color Extensions](#color-extensions)
- [List Extensions](#list-extensions)
- [Number Extensions](#number-extensions)
- [Random Extensions](#random-extensions)
- [String Extensions](#string-extensions)
- [Vector Extensions](#vector-extensions)

## Camera Extensions

Methods that extends from `Camera` :

- `GetOrthographicBounds()`

## Color Extensions

### `ChangeRed(float red)`, `ChangeGreen(float green)`,  `ChangeBlue(float blue)`, `ChangeAlpha(float alpha)`

Changes the `Color.r`, `Color.g`, `Color.b`, `Color.a` value from a `Color` and return the new object. Why do these extensions exist? Because you can't modify a struct (like color). You always need to replace the reference.

```csharp
using BaseTool.Generic.Extensions;
using UnityEngine;

public class Test
{
    public Color Color = Color.blue;
}

Test test = new();

// If I want to change the red value, I can't do:
test.Color.r = 1; // CS1612 error

// But I can:
test.Color = test.Color.ChangeRed(1);

// Which is basically the same as:
var tempColor = test.Color;
tempColor.r = 1;
test.Color = tempColor;
```

### `ToHex()`

Returns a hex code from a color object:

```csharp
using BaseTool.Generic.Extensions;

Color red = Color.red;
red.ToHex(); // #FF0000
Color.blue.ToHex(); // #0000FF
```

### `ToHexAlpha()`

Returns a hex code including alpha from a color object:

```csharp
using BaseTool.Generic.Extensions;
using UnityEngine;

Color red = Color.red;
red.ToHexAlpha(); // #FF0000FF
Color.blue.ToHexAlpha(); // #0000FFFF

Color green = new Color(0, 1, 0, 0.5f);
green.ToHexAlpha(); // #00FF007F
```

## List Extensions

### `GetRandom()`

Returns a random element from a `List<T>` using the `UnityEngine.Random` class.

```csharp
using BaseTool.Generic.Extensions;
using System.Collections.Generic;
using UnityEngine;

List<MonoBehaviour> behaviours = new();
// filling the list
MonoBehaviour random = behaviours.Random();
```

### `GetRandom(System.Random random)`

Same as `GetRandom()` but using the `System.Random` object passed by parameter instead of `UnityEngine.Random`.

### `ExtractRandom()`

Returns a random element from a `List<T>` using the `UnityEngine.Random` class and removes it from the list.

```csharp
using BaseTool.Generic.Extensions;
using System.Collections.Generic;
using UnityEngine;

List<MonoBehaviour> behaviours = new();
// filling 5 elements in the list
Debug.Log(behaviours.Length); // 5
MonoBehaviour random = behaviours.ExtractRandom();
Debug.Log(behaviours.Length); // 4
```

### `ExtractRandom(System.Random random)`

Same as `ExtractRandom()` but using the `System.Random` object passed by parameter instead of `UnityEngine.Random`.

## Number Extensions

### `IsIn(params int[] comp)`

Determines if an `int` or a `float` is one of the numbers passed by parameter.

```csharp
using BaseTool.Generic.Extensions;

int element = 4;
element.IsIn(1, 2, 3, 4, 5); // true
element.IsIn(6, 7, 8); // false

float floatElement = 4.5f; // will be rounded by IsIn()
floatElement.IsIn(1, 2, 3, 4); // false
floatElement.IsIn(5, 6, 7, 8); // true
```

### `IsBetween(int a, int b)`, `IsBetween(float a, float b)`

Determines if an `int` or a `float` is between a range of two numbers.

```csharp
using BaseTool.Generic.Extensions;

int element = 4;
element.IsBetween(3, 5); // true
element.IsBetween(1, 3); // false
element.IsBetween(2f, 4f); // true

float floatElement = 4.5f;
floatElement.IsBetween(4, 5); // true
floatElement.IsBetween(1.3f, 4.5f); // true
```

### `IsBetweenExclusive(int a, int b)`, `IsBetweenExclusive(float a, float b)`

Same as `IsBetween()` but excluding both min and max.

```csharp
using BaseTool.Generic.Extensions;

int element = 4;
element.IsBetween(3, 5); // true
element.IsBetween(1, 3); // false
element.IsBetween(2f, 4f); // false

float floatElement = 4.5f;
floatElement.IsBetween(4, 5); // true
floatElement.IsBetween(1.3f, 4.5f); // false
```

## Random Extensions

### `Next(float min, float max)`

Works the same as the [`UnityEngine.Random.Range()`](https://docs.unity3d.com/ScriptReference/Random.Range.html) but for `System.Random`.

```csharp
using BaseTool.Generic.Extensions;
using System;

Random rand = new();
rand.Next(1.2f, 55.5f);
```

## String Extensions

[WIP]

## Vector Extensions

### `ChangeX(float x)`, `ChangeY(float y)`, `ChangeZ(float z)`

hanges the `Vector3.x`, `Vector3.y` or `Vector3.z` value from a `Vector3` and return the new object. Why do these extensions exist? Because you can't modify a struct. You always need to replace the reference.

```csharp
using BaseTool.Generic.Extensions;

Transform player; // let's admit it's initialized

// If I want to change the x value, I can't do:
player.position.x = 1f; // CS1612 error

// But I can:
player.position = player.position.ChangeX(1f);

// Which is basically the same as:
var tempPos = player.position;
tempPos.x = 1;
player.position = tempPos;
```

### `Clamp(Vector3 min, Vector3 max)`

Works like the [`Mathf.Clamp()`](https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html) for `float`, but using two vectors as min and max.

```csharp
using BaseTool.Generic.Extensions;
using UnityEngine;

Vector3 pos = new(1, 2, 3);
pos = pos.Clamp(new(2, -1, -2), new (4, 1, 0));
Debug.Log(pos); // (2.00, 1.00, 0.00)
```

### `Lerp(Vector3 begin, Vector3 end)`

Works like the [`Vector3.Lerp()`](https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html) but using a vector as the ratio.

```csharp
using BaseTool.Generic.Extensions;
using UnityEngine;

Vector3 ratio = new(0, 0.5f, 1);
Vector3 newPos = ratio.Lerp(
    new(-5, -14, -30), 
    new(4, 20, 29)
);
Debug.Log(newPos); // (-5.00, 3.00, 29.00)
```

### `InverseLerp(Vector3 begin, Vector3 end)`

Works like the [`Vector3.InverseLerp()`](https://docs.unity3d.com/ScriptReference/Vector3.InverseLerp.html) but using a vector as the position to ratio.

```csharp
using BaseTool.Generic.Extensions;
using UnityEngine;

Vector3 pos = new(-5f, 3f, 29f);
Vector3 ratio = pos.InverseLerp(
    new(-5, -14, -30), 
    new(4, 20, 29)
);
Debug.Log(newPos); // (0.00, 0.50, 1.00)
```

### `GetClosestPointOnVector(Vector3 begin, Vector3 end)`

[WIP]