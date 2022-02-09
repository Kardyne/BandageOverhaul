# The Long Dark Mod Template

A generic project template for The Long Dark mods.

The main goal of this project is to make it as easy and smooth as possible to
get into modding for TLD by automatically configuring a number of settings and
parameters required to start. In addition, it provides tools for new and
existing modders to help development and testing.

See [CHANGELOG.md](./CHANGELOG.md) for notable changes between versions.

## Main features

- Build extensions from [TLDBuildExtensions](https://github.com/Kardyne/TLDBuildExtensions)
	- [Auto-detection of the TLD install directory](https://github.com/Kardyne/TLDBuildExtensions#TLDDir).
	- Automatic configuration of
	  [reference paths and references](https://github.com/Kardyne/TLDBuildExtensions#References)
	  for the project.
	- Custom targets for the [build process](https://github.com/Kardyne/TLDBuildExtensions#Build):
	  - Build AssetBundle files build
	  - Zip the ModComponent directory
	  - Copy mod DLL and ModComponent files to the TLD Mods for faster
		debugging and tests
	- [Custom run target](https://github.com/Kardyne/TLDBuildExtensions#Run) for Visual Studio to start the game directly from the IDE.
- Comes with the configured Unity project
  [ModComponent_TemplateProject](https://github.com/ds5678/ModComponent_TemplateProject)
  made by [@ds5678].
- Directory structure for ModComponent files in `Distributable`
- Comes with a starting implementation and an example
  [ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings) file.
- Preconfigured `.gitignore` files for Unity and Visual Studio.
- Git LFS tracking:
  - Textures: `.jpg`, `.png`, `.bmp`
  - 3D Models: `.fbx`, `.blend`
  - Unity: `.unity3d`
  - ModComponent: `.zip`, `.modcomponent`

## Prerequisites

The Long Dark must be installed (Epic or Steam) as well as
[MelonLoader](https://github.com/LavaGang/MelonLoader).

Depending on what your mod is about, you will also need:
- Visual Studio (or Jetbrains Rider) for mods which need to patch game functions
- Unity Editor for creating models and textures for custom items
- A simple text editor for tweaking loot tables or adding blueprints

## Quick start

### New mod

To start creating a new mod:

1. Download the latest release and unzip somewhere, fork the project or click on
   'Use this template' on GitHub.
1. Remove unnecessary files and folders for your mod
   - `ModComponent` folder if you do not need custom items. If you plan to use
     Visual Studio, remove the project from there _first_ before deleting
     the folder.
        - `ModComponent\Unity` folder if you do not plan on adding custom prefabs
	    - `ModComponent\Distributable` folder if you do not plan on adding items,
          blueprints, or tweaking loot tables.
   - `Code` folder if you do not need custom game code. If you plan to use
     Visual Studio, remove the project from there _first_ before deleting
     the folder.
1. Open `TLDModTemplate.sln` in Visual Studio. Reference paths and assemblies
   references should be found automatically. If not found, check
   [Customization](#customization).
1. Rename the solution and restart Visual Studio. Update the values in
   `BuildInfo.cs`.
1. Check [Usage](#usage) for more information on using and customizing this
   template.
1. [Optional but recommended] Initialize a git repository and publish on
   GitHub. See [GitHub - Get started](https://docs.github.com/en/get-started).

Additional resources:

- TLD Modding discord
- [ModComponent documentation](https://ds5678.github.io/ModComponent/) for
  creating custom items.
- [ModComponent tutorial](https://github.com/stmSantana/ModComponentDocs) by
  [@stmSantana].
- [MelonLoader wiki](https://melonwiki.xyz/) for help in patching game
  functions.

### Existing mod

Check out [TLDBuildExtensions](https://github.com/Kardyne/TLDBuildExtensions)
for build extensions.

## Usage

### Customization

To customize project parameters such as the path to the TLD directory or the
path to the Unity Editor, set values in the `Directory.Build.props` file.

See [TLDBuildExtensions#customization](https://github.com/Kardyne/TLDBuildExtensions#customization)
for more information.

### Unity 

See [Unity README](./Unity/README.md).

## Thanks

[@ds5678] for the [ModComponent_TemplateProject](https://github.com/ds5678/ModComponent_TemplateProject)
and the [Mod development tutorial](https://the-long-dark-modding.fandom.com/wiki/Making_Mods_for_1.81%2B).

All modders and members of the TLD Modding Discord.

Hinterland for a great game.

[@ds5678]: https://github.com/ds5678/
[@stmSantana]: https://github.com/stmSantana/
