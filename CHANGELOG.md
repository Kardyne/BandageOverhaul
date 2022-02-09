# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

## [2.2.0] - 2022-02-09

### Changed

- Build extensions are now in their own `Kardyne.TLDBuildExtensions` NuGet
  package ([repository](https://github.com/Kardyne/TLDBuildExtensions)).
  This will make it easier to update and share.
- Customization is now in `Directory.Build.props` instead of
  `ProjectParameters.props`.

### Removed

- All build extensions files, except `Directory.Build.props` at the solution
  root.

## [2.1.0] - 2022-02-08

### Changed

- Epic install detection now reads and parses the `UnrealEngineLauncher`
  library file (the location of which is obtained through the Windows
  registry). This should make it pretty reliable.
- Steam install detection now gets the default Steam library location from
  the registry. Other Steam libraries are not checked for TLD installs, but
  this should still make it more reliable.
- Set ModComponent project's `AssemblyName` property to `SolutionName` and
  change `ModComponentFilename` to reference `TargetName` (based on
  `AssemblyName`) instead of `SolutionName`.

### Fixed

- Visual Studio can now open the solution even if the `Epic.props`
  `ProjectParameters.props` or `Steam.props` files are missing.

## [2.0.0] - 2022-02-07

### Changed

- Split solution into two different projects: a C# project for custom code and
  a [Microsoft.Build.NoTargets](https://github.com/microsoft/MSBuildSdks/blob/f3125453cc5e2751bb6fb2a374b8935163d7a69a/src/NoTargets/README.md)
  project exclusively for ModComponent files. This should make it easier to
  only use the template for one or the other.
- Split `Directory.Build.targets` into two, one for each project. Only the
  `CopyDLL` target remains in `Code`, the other custom targets were all kept
  for the `ModComponent` project.
- `ReferencePath` property is no longer overridden by `Directory.Build.props`.
  New reference paths are now inserted at the beginning of the list.

### Fixed

- Visual Studio up-to-date check not building AssetBundle or ModComponent
  files. Using `Microsoft.Build.NoTargets` as the SDK and including all assets
  and Distributable files into the project lets Visual Studio keep track of
  changes.

## [1.1.0] - 2022-02-01

### Removed

- `UnhollowerRuntimeLib` from references

## [1.0.0] - 2022-02-01

### Added

- Auto-detection of the TLD install directory at
  `ProgramFiles(x86)\Steam\steamapps\TheLongDark` for Steam (default if found)
  or `ProgramFiles\Epic Games\TheLongDark` for Epic Games.
- Reference paths (`MelonLoader`, `MelonLoader\Managed` and `Mods`
  directories) and references for the project.
- Custom targets for the build process.
  - AssetBundle files building
  - ModComponent directory zipping
  - Mod DLL and ModComponent files copying to the TLD Mods folder.
- Custom run target for Visual Studio to start the game directly from the IDE
  (Steam or Epic only).
- Unity project template
  ([ModComponent_TemplateProject](https://github.com/ds5678/ModComponent_TemplateProject))
  by [@ds5678](https://github.com/ds5678/).
- Directory structure for ModComponent files in `Distributable`
- Starting implementation and example
  [ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings) file.
- `.gitignore` files for Unity and Visual Studio.
- Git LFS tracking:
  - Textures: `.jpg`, `.png`, `.bmp`
  - 3D Models: `.fbx`, `.blend`
  - Unity: `.unity3d`
  - ModComponent: `.zip`, `.modcomponent`
- Configurable settings in `ProjectParameters.props`:
  - TLD install root directory `TLDDir`
  - Unity editor path `UnityEditorPath`
  - Relative path to the unity project directory `UnityProjectRelativeDir`
  - Relative path to the ModComponent directory `ModComponentRelativeDir`

[2.2.0]: https://github.com/kardyne/TLDModTemplate/releases/tag/v2.2.0
[2.1.0]: https://github.com/kardyne/TLDModTemplate/releases/tag/v2.1.0
[2.0.0]: https://github.com/kardyne/TLDModTemplate/releases/tag/v2.0.0
[1.1.0]: https://github.com/kardyne/TLDModTemplate/releases/tag/v1.1.0
[1.0.0]: https://github.com/kardyne/TLDModTemplate/releases/tag/v1.0.0
