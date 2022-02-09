using MelonLoader;

namespace TLDModTemplate
{
    internal class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Settings.instance.AddToModSettings(BuildInfo.Name);
            LoggerInstance.Msg($"Version {BuildInfo.Version}");
        }
    }
}
