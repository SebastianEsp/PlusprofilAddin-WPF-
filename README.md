# PlusprofilAddin (WPF)
Sparx Systems Enterprise Architect add-in that facilitates simple displaying and editing of tagged values covered by the Plusprofil MDG. 

## Overview


## Getting Started
### Prerequisites
* Sparx Systems Enterprise Architect
* WiX 3.11

### Deploying
1. Build the project `PlusprofilAddin` to build `PlusprofilAddin.dll`.
2. Build the project `PlusprofilAddinInstaller` to build `PlusprofilAddinInstaller.msi`.
3. Run the installer to install the add-in for the current user.

For development and testing purposes, the build output of PlusprofilAddin (`PlusprofilAddin.dll`) can be used without running the installer through the following steps:

1. Open the Registry Editor (`regedit.exe`).
2. Go to `Computer\HKEY_CURRENT_USER\Software\Sparx Systems\EAAddins`. If the key `EAAddins` does not exist, add it. 
3. Add a new key named `PlusprofilAddin` through the context menu, i.e. right click -> New -> Key.
4. Select the default value in the key, and change the value to `PlusprofilAddin.MainClass`.
5. Build the project `PlusprofilAddin` to build `PlusprofilAddin.dll`.

To use the above steps, ensure that no versions of `PlusprofilAddin.dll` exist other than the version used for testing.

## Quickstart guide to adjusting the add-in
### Modifying the behaviour of a `PlusprofilTaggedValue`
Go to `PlusprofilAddin.PlusprofilTaggedValueDefinitions`, found in `PlusprofilAddin\PlusprofilTaggedValue.cs`.

#### Defining a new `PlusprofilTaggedValue`
In the `PlusprofilTaggedValueDefinitions.Definitions` initialization, add an additional `PlusprofilTaggedValue` initialization at the end of the list.
The constructor should takes following parameters:
* `key`: The key used to identify the PlusprofilTaggedValue. Should be identical to the key used in the ViewModels and string resource files.
* `name`: The name used to identify the tagged value in Sparx Systems Enterprise Architect.
* `hasMemoField`: Defines certain behaviour in the add-in, e.g. if line breaks are allowed, as well as how tagged value information is stored in Sparx Systems Enterprise Architect.
* `manyMultiplicity`: Defines certain behaviour in the add-in, e.g. if more than one of the tagged value should be allowed to be created.

Note that it is possible to define multiple `PlusprofilTaggedValue` with a similar `Name` but different `Key`, allowing tagged value behaviour to vary depending on object type by using a different `Key`.

#### Modifying an existing `PlusprofilTaggedValue`
In the `PlusprofilTaggedValueDefinitions.Definitions` initialization, find the `PlusprofilTaggedValue` to modify, either by `Name`, their expected name in Sparx Systems Enterprise Architect, or `Key`, the key used to identify them in the add-in. The behaviour that can be modified in the constructor is:
* `key`: The key used to identify the PlusprofilTaggedValue. Should be identical to the key used in the ViewModels and string resource files.
* `name`: The name used to identify the tagged value in Sparx Systems Enterprise Architect.
* `hasMemoField`: Defines certain behaviour in the add-in, e.g. if line breaks are allowed, as well as how tagged value information is stored in Sparx Systems Enterprise Architect.
* `manyMultiplicity`: Defines certain behaviour in the add-in, e.g. if more than one of the tagged value should be allowed to be created.

### Modifying the displayed name of a `PlusprofilTaggedValue` or other dynamic string resource
1. Open the file `PlusprofilAddin\Resources\StringResources.dk-DK.xaml` or `PlusprofilAddin\Resources\StringResources.dk-DK.xaml`.
2. Find the string value with the key of the PlusprofilTaggedValue to modify.
3. Modify the value of the string.

### Adding support for a new language
1. Create a new file `StringResources.xx-XX.xaml`, where `xx-XX` is the .NET Language Culture Name, e.g. `nl-NL` for Dutch - The Netherlands.
2. In the new file, add a new `ResourceDictionary`, i.e.:
    ````<ResourceDictionary
		xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
		xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
		xmlns:system="clr-namespace:System;assembly=mscorlib"/>````
3. Create a new `<system:String/>` in the `ResourceDictionary` for each string to translate. Ensure that the `x:Key` corresponds to the key used in the relevant UI element or `PlusprofilTaggedValue`.
4. Open `PlusprofilAddin.MainClass`, found in `PlusprofilAddin\MainClass.cs`.
    1. Add a field `private const string XMenuOption = "&Open X Editing Window"`, where X is the language to add.
    Example:
    ````private const string DutchMenuOption = "&Open Dutch Editing Window;````
    To add a menu option for dutch
    2. In `MainClass.EA_GetMenuitems(..)`, add the newly added field to `string[] subMenus`.
    Example:
    ````string[] subMenus = {DanishMenuOption, EnglishMenuOption, DutchMenuOption};````
    To add the Dutch option to the menu.
    3. In the `itemName` switch statement of `MainClass.EA_MenuClick(..)`, add a new case for the newly added menu option.
    Example:
    ````case DutchMenuOption:
    	dict.Source = new Uri("pack://application:,,,/PlusprofilAddin;component/Resources/StringResources.nl-NL.xaml",
    	UriKind.Absolute);
    	break;````
    To add an option for the menu option `DutchMenuOption`, using the file `StringResources.nl-NL.xaml`.
5. Consider adding a hotkey for the new language

### Changing / adding hotkeys
1. Open the code-behind for `PlusprofilAddin.HotkeyForm`, found in `PlusprofilAddin\HotkeyForm.cs`.
2. In the try-catch statement of the constructor, add a statement `keyboardHook.RegisterHotKey(parameters)`, using the desired combination of keys and modifier keys as parameters.
Example:
````keyboardHook.RegisterHotKey(PlusprofilAddin.ModifierKeys.Control | PlusprofilAddin.ModifierKeys.Shift, Keys.Q)````
Registers the hotkey CTRL + SHIFT + Q.
3. In the switch case of the event handler `KeyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)`, add a case for the previously used combination of keys.
Example:
````case Keys.Q when e.Modifier == PlusprofilAdd.ModifierKeys.Control | PlusprofilAdd.ModifierKeys.Shift:
	foo();
	break;````
Calls the function `foo()` when CTRL + SHIFT + Q is pressed.

## Authors
* **Mathias Enggrob Boon** - [Boonoboo](https://github.com/Boonoboo)

## License
This project is licensed under the GNU General License v3.0.