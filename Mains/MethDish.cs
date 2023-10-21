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
using System.Collections.Generic;

namespace MethMod.Mains
{
    public class MethDish : CustomDish
    {
        public override string UniqueNameID => "Meth Dish";
        public override DishType Type => DishType.Base;
        //change to actual icon later
        public override GameObject DisplayPrefab => Mod.Bundle.LoadAsset<GameObject>("Crystal");
        public override GameObject IconPrefab => DisplayPrefab;
        // should more or less people come for this dish, LargeIncrease, SmallDecrease etc
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallIncrease;
        // change info about this Unlock such as the Colour, and Icon.
        public override CardType CardType => CardType.Default;
        // ExpReward is used to decide how much XP the Player should get after the run.
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Small;
        // UnlockGroup is used to decide when this Unlock can appear as an option. I think this is by default
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        // decide if you need to be on a specific franchise tier for this Unlock to appear as an option. goes with MinimumFranchiseTier 
        public override bool IsSpecificFranchiseTier => false;
        // if this Dish should generate an entity to be saved.
        public override bool DestroyAfterModUninstall => true;
        // IsUnlockable is used to decide if this Unlock can appear as an option. Maybe make this false later, ppl dont want meth in reg kitchen
        public override bool IsUnlockable => true;

        //make it very common
        public override float SelectionBias => 1;

        // starting names for restaurants
        public override List<string> StartingNameSet => new List<string> {
            "Mr White's Delights",
            "Blue Sky",
            "Los Pollos Hermanos",
            "\"Meth\"odical Dining"
        };

        
        // what customers can order
        public override List<Dish.MenuItem> ResultingMenuItems => new List<Dish.MenuItem>
        {
            new Dish.MenuItem
            {
                Item = Mod.CookedMeth,
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuType = DynamicMenuType.Static,
                DynamicMenuIngredient = null
            }
        };
        // what you need for the dish, ingredients, hob etc,
        // ingredients will spawn said provider, for example if a min ingredient is flour, and there no flour bag, flour bag spawned!!!!
        public override HashSet<Item> MinimumIngredients => new HashSet<Item>
        {
            Mod.CookedMeth,
            Mod.Cheese
        };

        // needed interactive stuff
        public override HashSet<Process> RequiredProcesses => new HashSet<Process>
        {
            Mod.Cook,
            Mod.Knead
        };

        public override bool IsAvailableAsLobbyOption => true;

        // recipe for the dish
        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Serve the meth!" }
        };


        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new HashSet<Dish.IngredientUnlock>
        {
            new Dish.IngredientUnlock
            {
                MenuItem = (ItemGroup)GDOUtils.GetExistingGDO(ItemGroupReferences.Mayonnaise),
                Ingredient = (Item)GDOUtils.GetExistingGDO(ItemReferences.Egg)
            },
            new Dish.IngredientUnlock
            {
                MenuItem = (ItemGroup)GDOUtils.GetExistingGDO(ItemGroupReferences.Mayonnaise),
                Ingredient = (Item)GDOUtils.GetExistingGDO(ItemReferences.Oil)
            }
        };

        // make the lobby card thing
        public override List<(Locale, UnlockInfo)> InfoList => new List<(Locale, UnlockInfo)> {
            { (Locale.English, LocalisationUtils.CreateUnlockInfo("Meth", "Jesse, we need to cook!", "Make sure it's up to Los Pollos Standards") )}
        };


        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial(DisplayPrefab, "mesh", new Material[]
            {
                MaterialUtils.GetCustomMaterial("ShinyBlue"),
            });
        }
    }
}