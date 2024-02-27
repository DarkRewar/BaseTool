# Changelog 

## 0.2.0

### Improvements

- Add the `SetupWizard` to enable or disable modules [[#7](https://github.com/DarkRewar/BaseTool/pull/7)]

### Changes

- Replace labels by float field in the `[MinMaxAttribute]` property drawer
- Replace `BaseTool.Generic.*` namespaces to `BaseTool` [[#10](https://github.com/DarkRewar/BaseTool/pull/10)]

## 0.1.0

### Improvements

- Add `GetComponent`, `GetComponents`, `GetComponentInChildren`, `GetComponentsInChildren` and `GetComponentInParent` attributes [[#5](https://github.com/DarkRewar/BaseTool/pull/5)]
- Add `FirstPersonController`, `SideViewController`, `JumpController`, `IMovable` and `IJumpable` [[#6](https://github.com/DarkRewar/BaseTool/pull/6)]
- Add `Console` to allow using commands inside the game runtime
- Add `Navigation` system to allow navigation between differents UI views

### Changes

- Rewriting architecture to get a better seperation of concerns [[#4](https://github.com/DarkRewar/BaseTool/pull/4)]
- Removing `PlayerMovement` from `BaseTool.Shooter`, it is now replaced by `FirstPersonController`

## 0.0.3

- Add `MonoSingleton` [[#2](https://github.com/DarkRewar/BaseTool/pull/2)]
- Add `Cooldown` class [[#1](https://github.com/DarkRewar/BaseTool/pull/1)]
- Add `CameraExtensions` : `GetOrthographicBounds()`
- Add `ColorExtensions` : `ChangeRed()`, `ChangeBlue()`, `ChangeGreen()`, `ChangeAlpha()`, `ToHex()` and `ToHexAlpha()`
- Add `ListExtensions` : `GetRandom()` and `ExtractRandom()`
- Add `NumberExtensions` : `IsIn()`, `IsBetween()` and `IsBetweenExclusive()`
- Add `RandomExtension` : `Next()` for floats
- Add `StringExtensions` :
    - Parsing strings : `ToByte()`, `ToUshort()`, `ToUint()`, `ToUlong()`, `ToSbyte()`, `ToShort()`, `ToInt()` and `ToLong()`
    - Matching : `AfterLast()`, `BeforeLast()`, `AfterFirst()`, `BeforeFirst()` and `PrefixMatch()`
    - Generation : `Repeat()`
- Add `Vector3Extensions` : `ChangeX()`, `ChangeY()`, `ChangeZ()`, `Clamp()`, `Lerp()`, `InverseLerp()`, `GetClosestPointOnVector()`, `RatioOnVector3()`
- Add `MinMax` attribute to put upon int and float for inspector
- Add `Tree<T>` system for generic objects