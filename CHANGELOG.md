# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

## [1.0.0] - 2022-02-09

### Added

- `DirtyBandage` item in the game. It can be used in the same way as a regular
  bandage for sprains but if the player applies it to an open wound (ie. when
  bleeding), it increases the chance of getting an infection.
- Sterilizing a `DirtyBandage` in boiling water creates a `Sterilized Bandage`
  which is just a renamed regular bandage.
- Disabled regular bandage crafting
- Cloth harvesting provides two dirty bandages instead of two regular
  bandages.
- Craft recipe for dirty bandages using one cloth to craft two dirty bandages.
- Dirty bandages spawn in first aid kits, backpacks or human corpses.

[1.0.0]: https://github.com/Kardyne/BandageOverhaul/releases/tag/v1.0.0
