using HarmonyLib;
using OS.Items;

namespace NoRottenHarvest
{
    /// <summary>
    /// A ripe <see cref="GrowItem"/> (fruit on a multi-harvest plant) starts its spoil timer at once, and <c>UpdateSpoilTime</c> writes
    /// <c>integrity = 1 − timer</c> into the meta that <c>GetItem</c> hands out. At the end of the timer the fruit hides and regrows.
    /// Nothing here touches saved fields: the timer runs as in vanilla (or is paused), only the picked item's freshness changes.
    /// </summary>
    [HarmonyPatch(typeof(GrowItem))]
    internal static class FruitPatches
    {
        /// <summary>FreshUntilPlantDies: the timer doesn't run, so the fruit never falls off and never looks rotten.</summary>
        [HarmonyPrefix]
        [HarmonyPatch("UpdateSpoilTime")]
        private static bool UpdateSpoilTimePrefix() => !(Plugin.On && Plugin.Mode.Value == VineFruitSpoilMode.FreshUntilPlantDies);

        /// <summary>
        /// Vanilla sets <c>spoiled = true</c> right after hiding and resetting the fruit, and only <c>ResetGrow</c> clears it again,
        /// which it already ran. So the fruit that regrows counts as rotten. Clear the flag when the fruit was just removed.
        /// </summary>
        [HarmonyPostfix]
        [HarmonyPatch("UpdateSpoilTime")]
        private static void UpdateSpoilTimePostfix(GrowItem __instance)
        {
            if (Plugin.On && Plugin.FixRegrownFruit.Value && __instance.spoiled && !__instance.dontRemoveWhenSpoiled)
                __instance.spoiled = false;
        }

        /// <summary>A fruit that isn't rotten is picked at full freshness.</summary>
        [HarmonyPrefix]
        [HarmonyPatch(nameof(GrowItem.GetItem))]
        private static void GetItemPrefix(GrowItem __instance)
        {
            if (!Plugin.On || Plugin.Mode.Value == VineFruitSpoilMode.Vanilla || !__instance.readyToPick || __instance.Spoiled())
                return;
            object[] meta = __instance.meta;
            if (meta == null)
                return;
            foreach (object m in meta)
                if (m is ItemPerishabledata perishable)
                    perishable.integrity = 1f;
        }
    }
}
