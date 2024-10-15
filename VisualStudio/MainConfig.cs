﻿using BepInEx;
using BepInEx.Configuration;
using LunarsOfExiguity.Content.Lunar.Reworks;

namespace LunarsOfExiguity
{
    public static class MainConfig
    {
        public static ConfigEntry<bool> EnableLogging;
        public static ConfigEntry<bool> GainItemOnFracture;
        public static ConfigEntry<float> FractureItemDelay;
        public static ConfigEntry<NameStyle> ItemNameStyle;
        public static ConfigEntry<int> RoundingLength;

        public static ConfigEntry<float> BaseDisableSkillsPercentage;
        public static ConfigEntry<float> MaxDisableSkillsPercentage;
        
        public static ConfigEntry<float> AdditionalChargeSpeedPercentage;
        public static ConfigEntry<float> MaxDamagePercentage;
        public static ConfigEntry<float> DamageLossOnHitPercentage;



        public static void SetUp(BaseUnityPlugin plugin)
        {
            GeneralConfig(plugin);
            LunarConfig(plugin);
        }

        public static void GeneralConfig(BaseUnityPlugin plugin)
        {
            var section = "! General !";
            
            EnableLogging = plugin.Config.Bind(section, "Enable Logging", true, "[ True = Keeps Logs | False = Turns Off Logs]\n If you disable this, I may not entirely be able to help you when an IL Hook fails.");
            RoundingLength = plugin.Config.Bind(section, "Item Stats Round", 0, "[ 0 = Whole | 1 = Tenths | 2 = Hundredths | 3 = ... ]\n Rounds item values to respective decimal place.");
            ItemNameStyle = plugin.Config.Bind(section, "Relic Names", NameStyle.Relic, "The naming conventions of lunar items.");
            GainItemOnFracture = plugin.Config.Bind(section, "Gain Item on Fracture", true, "Whether to gain the 'Fractured' item to count lunar item conversions.");
            FractureItemDelay = plugin.Config.Bind(section, "Fracture Delay", 0.5f, "PLEASE WRITE SOMETHING FOR THIS GEMO I AM LAZY.");
        }
        public static void LunarConfig(BaseUnityPlugin plugin)
        {
            BaseDisableSkillsPercentage = plugin.Config.Bind("AutoCastEquipment", "Rework - Percent Duration", 15f, "[ 15.0f = 15% Duration ]\n Duration per Equipment use");
            MaxDisableSkillsPercentage = plugin.Config.Bind("AutoCastEquipment", "Rework - Max Percent", 100f, "[ 100.0f = 100% Max Duration ]\n Max duration per Equipment use");

            // GEMO CHANGE THESE GEMO CHANGE THESE
            AdditionalChargeSpeedPercentage = plugin.Config.Bind("FocusConvergence", "Speed", 100f);
            MaxDamagePercentage = plugin.Config.Bind("FocusConvergence", "Max", 100f);
            DamageLossOnHitPercentage = plugin.Config.Bind("FocusConvergence", "Loss", 25f);
        }

        public enum NameStyle
        {
            Vanilla,
            Relic,
            Cursed
        }
    }
}
