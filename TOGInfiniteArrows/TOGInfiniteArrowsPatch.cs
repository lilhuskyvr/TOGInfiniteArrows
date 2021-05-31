using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace TOGInfiniteArrows
{
    public class TOGInfiniteArrowsPatch : MonoBehaviour
    {
        private Harmony _harmony;

        public void Inject()
        {
            try
            {
                _harmony = new Harmony("InfiniteArrows");
                _harmony.PatchAll(Assembly.GetExecutingAssembly());
                Debug.Log("Infinite Arrows Loaded");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }


        [HarmonyPatch(typeof(QuiverArrow))]
        [HarmonyPatch("OnParenting")]
        // ReSharper disable once UnusedType.Local
        private static class QuiverArrowOnParentingPatch
        {
            [HarmonyPostfix]
            private static void Postfix(QuiverArrow __instance, Stats s)
            {
                __instance.count = Int32.MaxValue;
            }
        }

        [HarmonyPatch(typeof(QuiverBolt))]
        [HarmonyPatch("OnParenting")]
        // ReSharper disable once UnusedType.Local
        private static class QuiverBoltOnParentingPatch
        {
            [HarmonyPostfix]
            private static void Postfix(QuiverBolt __instance, Stats s)
            {
                BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                         | BindingFlags.Static;
                __instance.GetType().GetField("count", bindFlags).SetValue(__instance, Int32.MaxValue);
            }
        }
    }
}