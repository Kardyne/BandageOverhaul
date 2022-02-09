using HarmonyLib;
using UnityEngine;

namespace BandageOverhaul
{
    internal class Patches
    {
        [HarmonyPatch(typeof(GameManager), "Awake")]
        internal class GameManager_Awake
        {
            private static void Postfix()
            {
                RemoveBandageBlueprints();
                ChangeClothHarvest();
            }

            private static void RemoveBandageBlueprints()
            {
                foreach (BlueprintItem bpi in Utils.FindBlueprintsForGear("GEAR_HeavyBandage"))
                {
                    bpi.enabled = false;
                }
            }

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

        [HarmonyPatch(typeof(CookingPotItem), "SetCookedGearProperties")]
        internal class CookingPotItem_SetCookedGearProperties
        {
            private static void Prefix(CookingPotItem __instance, GearItem rawItem, GearItem cookedItem)
            {
                // Add temporary FoodItem component to avoid errors in the SetCookedGearProperties method
                if (rawItem?.m_GearName == "GEAR_DirtyBandage" && cookedItem != null)
                {
                    FoodItem dirtyBandageFoodItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<FoodItem>(rawItem);
                    rawItem.m_FoodItem = dirtyBandageFoodItem;
                    FoodItem bandageFoodItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<FoodItem>(cookedItem);
                    cookedItem.m_FoodItem = bandageFoodItem;

                    dirtyBandageFoodItem.m_CaloriesTotal = 1;
                    bandageFoodItem.m_CaloriesTotal = 1;
                }
            }

            private static void Postfix(CookingPotItem __instance, GearItem rawItem, GearItem cookedItem)
            {
                // Remove temporary FoodItem component
                if (rawItem?.m_GearName == "GEAR_DirtyBandage" && cookedItem != null)
                {
                    FoodItem dirtyBandageFoodItem = rawItem.GetComponent<FoodItem>();
                    if (dirtyBandageFoodItem != null)
                    {
                        UnityEngine.Object.Destroy(dirtyBandageFoodItem);
                        rawItem.m_FoodItem = null;
                    }
                    FoodItem bandageFoodItem = cookedItem.GetComponent<FoodItem>();
                    if (bandageFoodItem != null)
                    {
                        UnityEngine.Object.Destroy(bandageFoodItem);
                        cookedItem.m_FoodItem = null;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Panel_Cooking), "UpdateGearItem")]
        internal class Panel_Cooking_UpdateGearItem
        {
            private static void Prefix(Panel_Cooking __instance)
            {
                // Add temporary FoodWeight component to avoid errors in the UpdateGearItem method
                GearItem selectedFood = __instance?.GetSelectedFood();
                GearItem cookingResult = selectedFood?.m_Cookable?.m_CookedPrefab;
                if (selectedFood?.m_GearName == "GEAR_DirtyBandage" && cookingResult != null)
                {
                    FoodWeight dirtyBandageFoodWeight = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<FoodWeight>(selectedFood);
                    dirtyBandageFoodWeight.m_MinWeightKG = selectedFood.m_WeightKG;
                    dirtyBandageFoodWeight.m_MaxWeightKG = selectedFood.m_WeightKG;
                    dirtyBandageFoodWeight.m_CaloriesPerKG = 1;
                    FoodWeight bandageFoodWeight = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<FoodWeight>(cookingResult);
                    bandageFoodWeight.m_MinWeightKG = cookingResult.m_WeightKG;
                    bandageFoodWeight.m_MaxWeightKG = cookingResult.m_WeightKG;
                    bandageFoodWeight.m_CaloriesPerKG = 1;

                    selectedFood.m_FoodWeight = dirtyBandageFoodWeight;
                    cookingResult.m_FoodWeight = bandageFoodWeight;
                }
            }

            private static void Postfix(Panel_Cooking __instance)
            {
                // Remove temporary FoodWeight component
                GearItem selectedFood = __instance?.GetSelectedFood();
                GearItem cookingResult = selectedFood?.m_Cookable?.m_CookedPrefab;
                if (selectedFood?.m_GearName == "GEAR_DirtyBandage" && cookingResult != null)
                {
                    FoodWeight dirtyBandageFoodWeight = selectedFood.GetComponent<FoodWeight>();
                    if (dirtyBandageFoodWeight != null)
                    {
                        UnityEngine.Object.Destroy(dirtyBandageFoodWeight);
                        selectedFood.m_FoodWeight = null;
                    }
                    FoodWeight bandageFoodWeight = cookingResult.GetComponent<FoodWeight>();
                    if (bandageFoodWeight != null)
                    {
                        UnityEngine.Object.Destroy(bandageFoodWeight);
                        cookingResult.m_FoodWeight = null;
                    }
                }
            }
        }
    }
}
