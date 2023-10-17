using System.Collections.Generic;
using System.Reflection;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using UnityEngine;

namespace CoolMod.Customs
{
    public class MethDish : CustomDish
    {
        public override string UniqueNameID => "Meth";
        public override bool IsAvailableAsLobbyOption => true;
        public override Dictionary<Locale, string> Recipe => new Dictionary<Locale, string>
        {
            { Locale.English, "Walter, get in the kitchen!" }
        };
        public override HashSet<Process> RequiredProcesses => new HashSet<Process>
        {
            (Process)GDOUtils.GetExistingGDO(ProcessReferences.Chop)
        };
        public override GameObject IconPrefab => ((ItemGroup)GDOUtils.GetExistingGDO(ItemGroupReferences.Mayonnaise)).Prefab;
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

        public override HashSet<Item> MinimumIngredients => new HashSet<Item>()
        {
            (Item)GDOUtils.GetExistingGDO(ItemReferences.Oil),
            (Item)GDOUtils.GetExistingGDO(ItemReferences.Egg)
        };

        public override List<Dish.MenuItem> ResultingMenuItems => new List<Dish.MenuItem>
        {
            new Dish.MenuItem
            {
                Item = (ItemGroup)GDOUtils.GetExistingGDO(ItemGroupReferences.Mayonnaise),
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuType = DynamicMenuType.Static,
                DynamicMenuIngredient = null
            }
        };

        public override List<string> StartingNameSet => new List<string> {
            "Mr White's Delights",
            "Blue Sky",
            "Los Pollos Hermanos"
        };
        public override DishType Type => DishType.Base;
        public override int BaseGameDataObjectID => DishReferences.BurgerBase;

        //CustomUnlock
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new List<Unlock> { };
        public override List<Unlock> HardcodedBlockers => new List<Unlock> { };

        public override void OnRegister(GameDataObject gameDataObject)
        {
            Dish dish = (Dish)gameDataObject;
            LocalisationObject<UnlockInfo> info = new LocalisationObject<UnlockInfo>();
            FieldInfo dictionary = ReflectionUtils.GetField<LocalisationObject<UnlockInfo>>("Dictionary");
            Dictionary<Locale, UnlockInfo> dict = new Dictionary<Locale, UnlockInfo>();
            dict.Add(Locale.English, new UnlockInfo
            {
                Name = "Meth",
                Description = "Adds Meth as a Main Dish"
            });
            dictionary.SetValue(info, dict);
            dish.Info = info;

            ((ItemGroup)GDOUtils.GetExistingGDO(ItemGroupReferences.Mayonnaise)).SatisfiedBy.Add((ItemGroup)GDOUtils.GetExistingGDO(ItemGroupReferences.Mayonnaise));
        }
    }
}