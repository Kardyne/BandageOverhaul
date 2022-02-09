using ModSettings;

namespace BandageOverhaul
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();

        [Name("Infection risk increase")]
        [Description("Infection risk increase when applying a dirty bandage on blood loss")]
        [Slider(0, 90)]
        public int infectionRiskIncrease = 20;
    }
}
