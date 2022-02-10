/// This file contains actual features for the mod.
using HarmonyLib;
using UnityEngine;

namespace BandageOverhaul
{
    internal class Features
    {
        [HarmonyPatch(typeof(GameManager), "Awake")]
        internal class GameManager_Awake
        {
            private static void Postfix()
            {
                RemoveBandageBlueprints();
                ChangeClothHarvest();
            }

            /// <summary>
            /// Remove all blueprints creating HeavyBandages.
            /// </summary>
            private static void RemoveBandageBlueprints()
            {
                foreach (BlueprintItem bpi in Utils.FindBlueprintsForGear("GEAR_HeavyBandage"))
                {
                    bpi.enabled = false;
                }
            }

            /// <summary>
            /// Change cloth harvest to give DirtyBandages instead of HeavyBandages.
            /// </summary>
            private static void ChangeClothHarvest()
            {
                Harvest clothHarvestPrefab = Resources.Load<GameObject>("GEAR_Cloth")?.GetComponent<Harvest>();
                GearItem dirtyBandageGearItem = Resources.Load("assets/prefabs/gear_dirtybandage.prefab")?.TryCast<GameObject>()?.GetComponent<GearItem>();
                if (clothHarvestPrefab == null || dirtyBandageGearItem == null)
                {
                    return;
                }
                clothHarvestPrefab.m_YieldGear = new GearItem[] { dirtyBandageGearItem };
                clothHarvestPrefab.m_YieldGearUnits = new int[] { 2 };
            }
        }

        [HarmonyPatch(typeof(BloodLoss), "BloodLossEnd")]
        internal class BloodLoss_BloodLossEnd
        {
            /// <summary>
            /// Increase infection risk when treating BloodLoss with a DirtyBandage item.
            /// </summary>
            private static void Postfix(BloodLoss __instance, int index)
            {
                FirstAidItem firstAidItem = GameManager.GetPlayerManagerComponent()?.m_FirstAidItemUsed;
                if (firstAidItem == null)
                {
                    return;
                }
                InfectionRisk infectionRisk = GameManager.GetInfectionRiskComponent();
                AfflictionBodyArea afflictionBodyArea = __instance.GetLocation(index);
                int infectionRiskIndex = infectionRisk.GetIndexFromLocation(afflictionBodyArea);
                GearItem gi = firstAidItem.GetComponent<GearItem>();
                if (infectionRiskIndex >= 0 && gi?.m_GearName == "GEAR_DirtyBandage")
                {
                    infectionRisk.m_CurrentInfectionChanceList[index] += Settings.instance.infectionRiskIncrease;
                }
            }
        }
    }
}
