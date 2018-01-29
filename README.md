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

## Authors
* **Mathias Enggrob Boon** - [Boonoboo](https://github.com/Boonoboo)

## License
This project is licensed under the GNU General License v3.0.
