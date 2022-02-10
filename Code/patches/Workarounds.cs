/// This file contains workarounds to avoid errors at runtime in TLD methods.
using HarmonyLib;

namespace BandageOverhaul
{
    internal class Workarounds
    {
        [HarmonyPatch(typeof(CookingPotItem), "SetCookedGearProperties")]
        internal class CookingPotItem_SetCookedGearProperties
        {
            private static void Prefix(CookingPotItem __instance, GearItem rawItem, GearItem cookedItem)
            {
                // Add temporary FoodItem component to avoid errors in the SetCookedGearProperties method
                if (rawItem?.m_GearName == "GEAR_DirtyBandage" && cookedItem != null)
                {
                    AddDummyFoodItem(rawItem);
                    AddDummyFoodItem(cookedItem);
                }
            }

            private static void Postfix(CookingPotItem __instance, GearItem rawItem, GearItem cookedItem)
            {
                // Remove temporary FoodItem component
                if (rawItem?.m_GearName == "GEAR_DirtyBandage" && cookedItem != null)
                {
                    RemoveFoodItem(rawItem);
                    RemoveFoodItem(cookedItem);
                }
            }

            private static void AddDummyFoodItem(GearItem gearItem)
            {
                if (gearItem == null)
                {
                    return;
                }
                FoodItem foodItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<FoodItem>(gearItem);
                gearItem.m_FoodItem = foodItem;
                foodItem.m_CaloriesTotal = 1;
            }

            private static void RemoveFoodItem(GearItem gearItem)
            {
                FoodItem foodItem = gearItem?.GetComponent<FoodItem>();
                if (foodItem != null)
                {
                    UnityEngine.Object.Destroy(foodItem);
                    gearItem.m_FoodItem = null;
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
