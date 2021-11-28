using System;
using BepInEx;
using Logger = BepInEx.Logging.Logger;
using PolyTechFramework;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using BepInEx.Configuration;


namespace TemplateMod
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    // Specify the mod as a dependency of PTF
    [BepInDependency(PolyTechMain.PluginGuid, BepInDependency.DependencyFlags.HardDependency)]
    // This Changes from BaseUnityPlugin to PolyTechMod.
    // This superclass is functionally identical to BaseUnityPlugin, so existing documentation for it will still work.
    public class TemplateMod : PolyTechMod
    {
        public new const string
            PluginGuid = "org.bepinex.plugins.TemplateMod",
            PluginName = "Template Mod",
            PluginVersion = "1.0.0";
        
        public static TemplateMod instance;
        public static ConfigEntry<bool> modEnabled;
        Harmony harmony;
        void Awake()
        {
            this.repositoryUrl = "https://github.com/Conqu3red/Template-Mod/"; // repo to check for updates from
			if (instance == null) instance = this;
            // Use this if you wish to make the mod trigger cheat mode ingame.
            // Set this true if your mod effects physics or allows mods that you can't normally do.
            isCheat = false;
           
            modEnabled = Config.Bind("Template Mod", "modEnabled", true, "Enable Mod");

            modEnabled.SettingChanged += onEnableDisable;
	    this.isEnabled = modEnabled.Value; // sync value initially

            harmony = new Harmony("org.bepinex.plugins.TemplateMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            this.authors = new string[] {"Conqu3red"};

            // just a log statement
            Logger.LogInfo("aaa");

            PolyTechMain.registerMod(this);
        }

        public void Start(){
            // do something idk
        }


        public void onEnableDisable(object sender, EventArgs e)
        {
            this.isEnabled = modEnabled.Value;

            if (modEnabled.Value)
            {
                enableMod();
            }
            else
            {
                disableMod();
            }
        }
        public override void enableMod() 
        {
            modEnabled.Value = true;
        }
        public override void disableMod() 
        {
            modEnabled.Value = false;
        }
    
    }
}
