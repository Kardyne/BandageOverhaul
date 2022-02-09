using MelonLoader;
using UnityEngine;

namespace BandageOverhaul
{
    internal class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Settings.instance.AddToModSettings(BuildInfo.Name);
            LoggerInstance.Msg($"Version {BuildInfo.Version}");
        }

        public override void OnApplicationLateStart()
        {
            GameObject dirtyBandagePrefab = Resources.Load("assets/prefabs/gear_dirtybandage.prefab")?.TryCast<GameObject>();
            if (dirtyBandagePrefab == null)
            {
                throw new System.NullReferenceException("Could not load DirtyBandage prefab for modification.");
            }
            AddFirstAidItem(dirtyBandagePrefab);
            AddCookingMeshes(dirtyBandagePrefab);
        }

        private void AddFirstAidItem(GameObject dirtyBandagePrefab)
        {
            FirstAidItem firstAidItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<FirstAidItem>(dirtyBandagePrefab);
            firstAidItem.m_AppliesBandage = true;
            firstAidItem.m_HPIncrease = 0;
            firstAidItem.m_LocalizedProgressBarMessage = new LocalizedString() { m_LocalizationID = "GAMEPLAY_ApplyingBandage" };
            firstAidItem.m_LocalizedRemedyText = new LocalizedString() { m_LocalizationID = "GAMEPLAY_ApplyBandage" };
            firstAidItem.m_LocalizedInspectModeUseText = new LocalizedString() { m_LocalizationID = "GAMEPLAY_APPLY" };
            firstAidItem.m_StabalizesSprains = true;
            firstAidItem.m_TimeToUseSeconds = 3;
            firstAidItem.m_UnitsPerUse = 1;
            firstAidItem.m_UseAudio = "Play_FirstAidBandage";
        }

        private void AddCookingMeshes(GameObject dirtyBandagePrefab)
        {
            Cookable cookable = dirtyBandagePrefab.GetComponent<Cookable>();
            if (cookable == null)
            {
                throw new System.NullReferenceException("DirtyBandage has no Cookable component.");
            }
            cookable.m_MeshCanStyle = Resources.Load<CookingPotItem>("GEAR_RecycledCan")?.m_WaterMesh ?? throw new System.NullReferenceException("Cannot load Can_Liquid Mesh.");
            CookingPotItem cookingPot = Resources.Load<CookingPotItem>("GEAR_CookingPot");
            cookable.m_MeshPotStyle = cookingPot?.m_WaterMesh ?? throw new System.NullReferenceException("Cannot load Pot_Liquid Mesh.");
            cookable.m_CookingPotRawMaterialsList = cookingPot.m_BoilWaterPotMaterialsList;
            cookable.m_CookingPotMaterialsList = cookingPot.m_BoilWaterReadyMaterialsList;
        }
    }
}
