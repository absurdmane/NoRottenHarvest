# No Rotten Harvest — test plan

Build and install: `dotnet build -c Release -p:Deploy=true` in `NoRottenHarvest/`.
Log: `BepInEx\LogOutput.log` — look for `No Rotten Harvest 1.0.0 loaded`. Speed up time or use DevMenu to get ripe fruit.

- [ ] The game starts, the log has the line, no errors; the config has `[Fruit]` and `[Mod]`.
- [ ] `FreshUntilTimer`: a fruit hanging ripe for a while (its bar half down) is picked at 100 % freshness.
- [ ] `FreshUntilTimer`: a fruit left hanging still falls off at the end of its timer and regrows later.
- [ ] `FixRegrownFruit`: the fruit that regrows after one fell off gives normal produce (vanilla: rot or nothing until reload).
- [ ] `FreshUntilPlantDies`: a ripe fruit doesn't shrink or change colour and never falls off; picked fresh.
- [ ] `FreshUntilPlantDies`: when the plant dies, its fruit can still be picked (fresh) before uprooting.
- [ ] `Vanilla`: freshness drops on the vine as without the mod.
- [ ] Harvest-all on the plant root ("pick all") gives fresh fruit too.
- [ ] `[Mod] Enabled = false` while playing: vanilla behaviour at once.
- [ ] Save and load: fruit keep their state; removing the mod leaves the save working.
