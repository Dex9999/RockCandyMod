using KitchenLib;
using CoolMod.Customs;
using KitchenMods;
using System.Reflection;
using UnityEngine;

namespace Meth
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.icecreamsandwch.breakingkitchen";
        public const string MOD_NAME = "Cook Meth";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "IceCreamSandwch";
        public const string MOD_GAMEVERSION = ">=1.1.7";

        public static AssetBundle Bundle;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) { }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            AddGameDataObject<MethDish>();

            LogInfo("Done loading game data.");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            AddGameData();
        }
        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}