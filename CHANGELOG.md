# Changelog 

## 0.5.0

### Improvements

- Add a `MessageAttribute` to display message in the inspector [[#91](https://github.com/DarkRewar/BaseTool/issues/91)]
- Add a `ButtonAttribute` to display a button for methods in the inspector [[#18](https://github.com/DarkRewar/BaseTool/issues/18)]
- Add DynamicExpresso library to use code evaluation for conditional attributes [[#74](https://github.com/DarkRewar/BaseTool/issues/74)]
- Add `EnableIf` and `DisableIf` attributes to mark field as readonly is condition is valid or not [[#95](https://github.com/DarkRewar/BaseTool/issues/95)]
- Add `Suffix` and `Prefix` attributes to add labels before or after a field in inspector [[#98](https://github.com/DarkRewar/BaseTool/issues/98)]
- Add `ToSerializableDictionary` extension to `Dictionary` class [[#100](https://github.com/DarkRewar/BaseTool/issues/100)]
- Add `Stop()`, `Pause()` and `Resume()` methods to `Cooldown` class [[#103](https://github.com/DarkRewar/BaseTool/issues/103)]
- Add `Vector2` extensions based on `Vector3` extensions [[#105](https://github.com/DarkRewar/BaseTool/issues/105)]

## 0.4.1

### Improvements

- Add the `MathUtils.Approximately()` method as a tolerant equivalent of `Mathf.Approximately` [[#86](https://github.com/DarkRewar/BaseTool/issues/86)]

### Changes

- Fix the creation of two `EventSystem` on `Console` initialization [[#75](https://github.com/DarkRewar/BaseTool/issues/75)]
- Fix the bug that blocks users to use `IfAttribute` with property accessor condition [[#78](https://github.com/DarkRewar/BaseTool/issues/78)]
- Add unit tests to avoid regression [[#87](https://github.com/DarkRewar/BaseTool/issues/87)]

## 0.4.0

### Improvements

- Add a custom drawer for `ValueListener` [[#41](https://github.com/DarkRewar/BaseTool/issues/41)]
- Add array and range extensions [[#39](https://github.com/DarkRewar/BaseTool/issues/39)]
- Add string extensions documentation [[#49](https://github.com/DarkRewar/BaseTool/issues/49)]
- Add `CooldownManager` to update every `Cooldown` automatically [[#63](https://github.com/DarkRewar/BaseTool/pull/63)]
- Add `Array.GetRandom()` extension methods [[#64](https://github.com/DarkRewar/BaseTool/issues/64)]
- Add a `TickManager` component [[#43](https://github.com/DarkRewar/BaseTool/issues/43)]
- Add a `Clear()` transform extensions [[#40](https://github.com/DarkRewar/BaseTool/issues/40)]
- Add a `PonderateRandom` class to alter weight of random elements [[#46](https://github.com/DarkRewar/BaseTool/issues/46)]
- Add a top-down character controller in the Movement module [[#15](https://github.com/DarkRewar/BaseTool/issues/15)]
- Add documentation and sample for the UI module [[#45](https://github.com/DarkRewar/BaseTool/issues/45)]

### Changes

- Fix a bug where using `ReadOnly` attribute on hidden field still applying its height [[#51](https://github.com/DarkRewar/BaseTool/issues/51)]
- Fix a bug that blocks inputs on the dev console on Unity 6 [[#50](https://github.com/DarkRewar/BaseTool/issues/50)]
- Update the `Console` to work with a settings file [[#57](https://github.com/DarkRewar/BaseTool/issues/57)]

## 0.3.0

### Improvements

- Add `ReadOnly` attribute [[#19](https://github.com/DarkRewar/BaseTool/issues/19)]
- Add `Todo List` editor feature [[#26](https://github.com/DarkRewar/BaseTool/issues/26)]
- Add `ValueListener` feature [[#30](https://github.com/DarkRewar/BaseTool/issues/30)]
- Add support links to the documentation and issues pages [[#32](https://github.com/DarkRewar/BaseTool/issues/32)]
- Add buttons to open data and persistent data paths [[#36](https://github.com/DarkRewar/BaseTool/issues/36)]
- Add `SerializableDictionary` feature [[#35](https://github.com/DarkRewar/BaseTool/issues/35)]

### Changes

- Fix [issue 21](https://github.com/DarkRewar/BaseTool/issues/21) that throws errors if not using old input system [[#24](https://github.com/DarkRewar/BaseTool/pull/24)]
- Fix [issue 22](https://github.com/DarkRewar/BaseTool/issues/22) that throws error on multiple Unity runtimes [[#25](https://github.com/DarkRewar/BaseTool/pull/25)]
- Fix [issue 29](https://github.com/DarkRewar/BaseTool/issues/29) which removes many logs and nullref exceptions in the `TodoWindow.cs` [[#33](https://github.com/DarkRewar/BaseTool/pull/33)]

## 0.2.0

### Improvements

- Package documentation enhanced
- Add the `SetupWizard` to enable or disable modules [[#7](https://github.com/DarkRewar/BaseTool/pull/7)]
- Add the weapon system [[#8](https://github.com/DarkRewar/BaseTool/pull/8)]
- Add scriptable object `GameEvent` system [[#11](https://github.com/DarkRewar/BaseTool/pull/11)]
- Add `If` and `IfNot` attributes [[#8](https://github.com/DarkRewar/BaseTool/pull/8)]
- Add documentation for the `Movement` module [[#14](https://github.com/DarkRewar/BaseTool/pull/14)]

### Changes

- Replace labels by float field in the `[MinMaxAttribute]` property drawer
- Replace `BaseTool.Generic.*` namespaces to `BaseTool` [[#10](https://github.com/DarkRewar/BaseTool/pull/10)]
- Injector does not retrieve if property is not null [[#8](https://github.com/DarkRewar/BaseTool/pull/8)]
- Change component icons to follow their module icon [[#8](https://github.com/DarkRewar/BaseTool/pull/8)]
- Fix [issue 12](https://github.com/DarkRewar/BaseTool/issues/12) that blocks the console to be interactable in Unity 2023+ [[#13](https://github.com/DarkRewar/BaseTool/pull/13)]

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