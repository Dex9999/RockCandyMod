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
using System.Collections.Generic; //for processes <<
using MethMod.Processes;

namespace MethMod.Mains
{
    internal class CookedMeth : CustomItem
    {
        //self explanatory
        public override string UniqueNameID => "CookedMeth";
        // This is the GameObject used for this Item's visual.
        // the name of my obj was Crystal
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("Crystal");
        // other catergories?
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        // this food is stackable
        // another is OutsideRubbish
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        // This is how much money is earned from serving this Item.
        public override ItemValue ItemValue => ItemValue.Medium;

        //game makes a provider for free :o
        public override Appliance DedicatedProvider => null;

        public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>
        {
            new Item.ItemProcess
            {
                Duration = 10, // how long to interact
                Process = Mod.BreakProcess, // using the cook process we defined/grabbed in mod.cs
                Result = Mod.Cheese // turns into what
            }
        };

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial(Prefab, "mesh", new Material[]
            {
                MaterialUtils.GetCustomMaterial("ShinyBlue"),
            });
            // you want to select the mesh, not the actual parent object
            // material name is ShinyBlue
            // below is simpler but starflux doesn't know so i did above for now
            // Prefab.ApplyMaterialToChild("mesh", "ShinyBlue");
        }
    }
}
