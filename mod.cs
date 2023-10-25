using Kitchen;
using KitchenData;
using KitchenLib;
using KitchenLib.Event;
using KitchenLib.References;
using KitchenMods;
using System.IO;
using System.Linq;
using System.Reflection;
using static KitchenLib.Utils.GDOUtils;
using UnityEngine;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MethMod.Mains;
using KitchenLib.Logging.Exceptions;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using UnityEngine.Networking.Types;
using TMPro;
using MethMod.Processes;
using static KitchenData.Appliance;


// Namespace should have "Kitchen" in the beginning - no
namespace MethMod
{
    public class Mod : BaseMod, IModSystem
    {
        
        public const string MOD_GUID = "com.icecreamsandwch.breakingkitchen";
        public const string MOD_NAME = "Cook Meth";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "IceCreamSandwch";
        public const string MOD_GAMEVERSION = ">=1.1.7";

        // Boolean constant whose value depends on whether you built with DEBUG or RELEASE mode, useful for testing
#if DEBUG
        public const bool DEBUG_MODE = true;
#else
        public const bool DEBUG_MODE = false;
#endif

        public static AssetBundle Bundle;

        //Game Data Objects already in the game
        public static Item Cheese => (Item)GDOUtils.GetExistingGDO(ItemReferences.Cheese);
        public static Item Pot => (Item)GDOUtils.GetExistingGDO(ItemReferences.Pot);
        public static Item Water => (Item)GDOUtils.GetExistingGDO(ItemReferences.Water);
        public static Item Sugar => (Item)GDOUtils.GetExistingGDO(ItemReferences.Sugar);
        public static Appliance Counter => (Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.Countertop);
        
        //GDO from the helpful "IngredientLib"
        //public static Item Garlic => (Item)GDOUtils.GetExistingGDO(IngredientLib.References.GetIngredient("garlic"));

        //processes, like how the character interacts
        public static Process Cook => (Process)GDOUtils.GetExistingGDO(ProcessReferences.Cook);
        public static Process Chop => (Process)GDOUtils.GetExistingGDO(ProcessReferences.Chop);
        public static Process Knead => (Process)GDOUtils.GetExistingGDO(ProcessReferences.Knead);
        public static Process Wash => (Process)GDOUtils.GetExistingGDO(ProcessReferences.Clean);

        public static Process BreakProcess => (Process)GDOUtils.GetCustomGameDataObject<BreakProcess>().GameDataObject;

        //item, can be combined and carried around
        public static Item CookedMeth => (Item)GDOUtils.GetCustomGameDataObject<CookedMeth>().GameDataObject;
        public static Item CookedMethTray => (Item)GDOUtils.GetCustomGameDataObject<CookedMethTray>().GameDataObject;

        public static ItemGroup UnstirredSolution => (ItemGroup)GDOUtils.GetCustomGameDataObject<UnstirredSolution>().GameDataObject;
        //dish
        public static Dish MethDish => (Dish)GDOUtils.GetCustomGameDataObject<MethDish>().GameDataObject;

        public static Item Flask => (Item)GDOUtils.GetCustomGameDataObject<Flask>().GameDataObject;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, $"{MOD_GAMEVERSION}", Assembly.GetExecutingAssembly())
        {
            /*
            string bundlePath = Path.Combine(new string[] { Directory.GetParent(Application.dataPath).FullName, "Mods", ModID });

            Debug.Log($"{MOD_NAME} {MOD_VERSION} {MOD_AUTHOR}: Loaded");
            Debug.Log($"Assets Loaded From {bundlePath}");*/
        }

        protected override void OnInitialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");

            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args)
            {

                if (args.gamedata.TryGet(ApplianceReferences.Countertop, out Appliance counter))
                {
                    if (!counter.Processes.Select(x => x.GetType()).Contains(typeof(BreakProcess)))
                    {
                        Appliance.ApplianceProcesses newProcess = new Appliance.ApplianceProcesses()
                        {
                            Process = (Process)GDOUtils.GetCustomGameDataObject<BreakProcess>().GameDataObject,
                            IsAutomatic = false,
                            Speed = 1f,
                            Validity = ProcessValidity.Generic
                        };
                        counter.Processes.Add(newProcess);
                    }
                }
            };

        }

        private void AddGameData()
        {
            LogInfo("Attempting to register game data...");

            //add the GDOs you need 
            AddGameDataObject<CookedMeth>();
            AddGameDataObject<CookedMethTray>();
            AddGameDataObject<Flask>();
            AddGameDataObject<UnstirredSolution>();
            AddGameDataObject<MethDish>();
            AddGameDataObject<BreakProcess>();

            LogInfo("Done loading game data.");
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            // TODO: Uncomment the following if you have an asset bundle.
            // TODO: Also, make sure to set EnableAssetBundleDeploy to 'true' in your ModName.csproj
            LogInfo("Attempting to load asset bundle...");
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).FirstOrDefault() ?? throw new MissingAssetBundleException(MOD_GUID);
            //custom process
            LogInfo("1");
            Bundle.LoadAllAssets<Texture2D>();
            LogInfo("2");
            Bundle.LoadAllAssets<Sprite>();
            LogInfo("3");
            var spriteAsset = Bundle.LoadAsset<TMP_SpriteAsset>("breakTex"); // use the name of your sprite ASSET here
            LogInfo("4");
            TMP_Settings.defaultSpriteAsset.fallbackSpriteAssets.Add(spriteAsset);
            string[] assetNames = Bundle.GetAllAssetNames();

            // Use a foreach loop to print each asset name
            foreach (string assetName in assetNames)
            {
                LogInfo(assetName);
            }
            spriteAsset.material = Object.Instantiate(TMP_Settings.defaultSpriteAsset.material);
            LogInfo("6");
            spriteAsset.material.mainTexture = Bundle.LoadAsset<Texture2D>("break"); // use the name of your sprite TEXTURE here

            LogInfo("Done loading asset bundle.");

            // Register custom GDOs
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