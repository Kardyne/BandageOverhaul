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

        /// <summary>
        /// Add FirstAidItem component to the DirtyBandage item.
        /// </summary>
        /// <param name="dirtyBandagePrefab">Non-null prefab for the DirtyBandage item.</param>
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

        /// <summary>
        /// Add default water meshes to the DirtyBandage Cookable component.
        /// </summary>
        /// <param name="dirtyBandagePrefab">Non-null prefab for the DirtyBandage item.</param>
        /// <exception cref="System.NullReferenceException">
        /// Thrown when <paramref name="dirtyBandagePrefab"/>has no Cookable component or
        /// if the m_WaterMesh property from the CookingPotItem component of GEAR_RecycledCan
        /// and GEAR_CookingPot is null or cannot be accessed.
        /// </exception>
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
