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

namespace MethMod.Mains
{
    internal class CookedMeth : CustomItem
    {
        //self explanatory
        public override string UniqueNameID => "CookedMeth";
        // This is the GameObject used for this Item's visual.
        // the name of my obj was tinker
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("tinker");
        // other catergories?
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        // this food is stackable
        // another is OutsideRubbish
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        // This is how much money is earned from serving this Item.
        public override ItemValue ItemValue => ItemValue.Medium;

        //game makes a provider for free :o
        public override Appliance DedicatedProvider => null;

        /*public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>
        {
            new Item.ItemProcess
            {
                Duration = 2, // how long to interact
                Process = Mod.Cook, // using the cook process we defined/grabbed in mod.cs
                Result = Refs.Pancake // turns into what
            }
        };*/

        public override void OnRegister(GameDataObject gameDataObject)
        {
            MaterialUtils.ApplyMaterial(Prefab, "group_0_-1040146473", new Material[]
            {
                MaterialUtils.GetCustomMaterial("Simple Flat"),
            });
            //i'm dumb and didnt rename the mesh so it's gibberish, you want to select the mesh, not the actual parent object
            //also i kept the material json name as Simple Flat :sob:
            //below is simpler but starflux doesn't know so i did above for now
            //Prefab.ApplyMaterialToChild("group_0_-1040146473", "Simple Flat");
        }
    }
}
