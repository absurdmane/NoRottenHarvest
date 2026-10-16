# No Rotten Harvest for Obenseuer

A BepInEx 6 (Unity Mono) plugin for **Obenseuer**. Fruit of multi-harvest plants is no longer half rotten when you pick it.

In vanilla a ripe fruit starts rotting on the vine the moment it ripens: pick it halfway through and you get 50 % freshness.
With this mod it's picked fresh. Nothing is written to the game's saves, so the mod is safe to add and remove.

## What it does

- **Fresh fruit.** A ripe fruit that hasn't fallen off is picked at full freshness.
- **Choose how long it hangs** (`VineFruitSpoilMode`):
  - `FreshUntilTimer` (default): picked fresh, but it still falls off when the game's on-vine timer runs out, as in vanilla.
  - `FreshUntilPlantDies`: it never falls off; it hangs fresh until the plant dies.
  - `Vanilla`: the game's own behaviour.
- **Regrown fruit fix.** Game bug: after a fruit falls off rotten, the one that regrows in its place still counts as rotten
  (until the game is reloaded) and gives rot or nothing when picked. Fixed (`FixRegrownFruit`).

## Install

1. Install [BepInEx 6 (Unity Mono)](https://builds.bepinex.dev/projects/bepinex_be) into the game folder.
2. Put `NoRottenHarvest.dll` into `BepInEx/plugins/NoRottenHarvest/`.
3. Start the game once; settings appear in `BepInEx/config/obenseuer.absurdmane.norottenharvest.cfg`.

## Configuration

All settings work without a restart.

| Section | Key | Default | Meaning |
|---|---|---|---|
| Fruit | `VineFruitSpoilMode` | `FreshUntilTimer` | `Vanilla` / `FreshUntilTimer` / `FreshUntilPlantDies` |
| | `FixRegrownFruit` | `true` | a fruit regrown after a rotten one is a normal fruit |
| Mod | `Enabled` | `true` | off = everything as in vanilla |

When reporting a problem, please attach `BepInEx/LogOutput.log` and say which plant.

## Building

Requirements: .NET SDK 6+ and the game with BepInEx 6 installed.

1. Copy `local.props.example` to `local.props` and set `GameDir` to your Obenseuer folder.
2. `dotnet build NoRottenHarvest -c Release` (add `-p:Deploy=true` to copy the DLL into the game's plugins folder).

## License

MIT © absurdmane, see [LICENSE](LICENSE).
