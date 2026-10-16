using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using HarmonyLib;

namespace NoRottenHarvest
{
    /// <summary>What happens to a ripe fruit hanging on a multi-harvest plant.</summary>
    public enum VineFruitSpoilMode
    {
        /// <summary>The game's own: freshness drops on the vine, the fruit vanishes when its timer runs out.</summary>
        Vanilla,

        /// <summary>Always picked fresh, but still vanishes when the on-vine timer runs out.</summary>
        FreshUntilTimer,

        /// <summary>Never vanishes on its own: hangs fresh until the plant dies.</summary>
        FreshUntilPlantDies
    }

    [BepInPlugin(Guid, Name, Version)]
    public sealed class Plugin : BaseUnityPlugin
    {
        public const string Guid = "obenseuer.absurdmane.norottenharvest";
        public const string Name = "No Rotten Harvest";
        public const string Version = "1.0.0";

        internal static ManualLogSource Log;

        internal static ConfigEntry<VineFruitSpoilMode> Mode;
        internal static ConfigEntry<bool> FixRegrownFruit;
        internal static ConfigEntry<bool> Enabled;

        private void Awake()
        {
            Log = Logger;

            Mode = Config.Bind("Fruit", "VineFruitSpoilMode", VineFruitSpoilMode.FreshUntilTimer,
                "Ripe fruit on multi-harvest plants. Vanilla = it rots on the vine and is picked half rotten; " +
                "FreshUntilTimer = always picked fresh, but still falls off when its on-vine timer runs out; " +
                "FreshUntilPlantDies = never falls off, hangs fresh until the plant dies. Works without a restart.");
            FixRegrownFruit = Config.Bind("Fruit", "FixRegrownFruit", true,
                "Game bug: after a fruit falls off rotten, the one that regrows in its place counts as rotten too (until the game is reloaded) " +
                "and gives rot or nothing when picked. On = the regrown fruit is a normal fruit. Works without a restart.");
            Enabled = Config.Bind("Mod", "Enabled", true,
                "Off = the mod does nothing at all: everything as in vanilla. Works without a restart.");

            new Harmony(Guid).PatchAll(typeof(Plugin).Assembly);
            Log.LogInfo($"{Name} {Version} loaded");
        }

        internal static bool On => Enabled.Value;
    }
}
