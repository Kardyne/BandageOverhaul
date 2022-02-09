# Bandage Overhaul

This is a mod for the game The Long Dark by Hinterland.

This mod makes bandages increase infection risk on open wounds unless
sterilized beforehand.

Feel free to open an [issue](https://github.com/Kardyne/BandageOverhaul/issues)
if you encounter any problem or for suggestions. You can also ask for help on
the TLD Modding discord.

## Features

This mod adds a new `DirtyBandage` item in the game. It can be used in the
same way as a regular bandage for sprains but if you apply it to an open wound
(ie. when bleeding), it increases the chance of getting an infection.

This new item can be sterilized in boiling water to create a
`Sterilized Bandage` which is safe to use on open wounds.

Crafting regular bandage is disabled: only dirty bandages can be crafted or
harvested from clothes. You may still start with sterilized bandages or find
some while scavenging.

You can also find dirty bandages in first aid kits, backpacks or on corpses.

### Settings

`Infection risk increase` controls the infection chance increase. The default
is to increase infection risk by 20%.

## Installation

- Install [MelonLoader](https://github.com/LavaGang/MelonLoader) for The Long Dark.
- Install [ModComponent](https://github.com/ds5678/ModComponent).
- Install [ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings).
- Download `BandageOverhaul.dll` and `BandageOverhaul.modcomponent` from the
  [releases page](https://github.com/Kardyne/BandageOverhaul/releases) and move
  them into your `Mods` folder.

## Future features

These are rough ideas at this point in time. Feel free to discuss them or
suggest new ones!

- Need to change bandages regularly for the same blood loss, or risking an
  infection
  - After some time the wound heals completely
  - When changing a bandage, the player gets a bloodied bandage than can be
    cleaned and sterilized again
- A wound that is still healing would be easier to open again (after a fall or
  a struggle)
