using KitchenData;
using KitchenDrinksMod.Boba;
using KitchenLib.Customs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ApplianceLib.Api.References.ApplianceLibGDOs;
using Unity.Entities;
using UnityEngine;
using Kitchen;
using static Kitchen.ItemGroupView;
using KitchenLib.Utils;

namespace MethMod.Mains
{
    // fix no chopping sound
    // same colour for flask + water and unstirred final
    // make it actually look nice, contents etc
    internal class UnstirredSolution : CustomItemGroup
    {
        public override string UniqueNameID => "UnstirredSolution";
        public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("waterflask");
        public override Item DisposesTo => Mod.Flask;


        public override List<ItemGroup.ItemSet> Sets => new List<ItemGroup.ItemSet>
            {
                new ItemGroup.ItemSet
                {
                    Min = 2,
                    Max = 2,
                    Items = new List<Item>
                    {
                        Mod.Water,
                        Mod.Sugar
                    }
                },
                new ItemGroup.ItemSet
                {
                    Min = 1,
                    Max = 1,
                    IsMandatory = true,
                    Items = new List<Item>
                    {
                        Mod.Flask
                    }
                }
            };

        public override List<Item.ItemProcess> Processes => new List<Item.ItemProcess>
        {
            new Item.ItemProcess
            {
                Duration = 2, // how long to interact
                Process = Mod.Cook, // using the cook process we defined/grabbed in mod.cs
                Result = Mod.CookedMethTray // turns into what
            }
        };

        public override void OnRegister(GameDataObject gameDataObject)
        {
            Prefab.ApplyMaterialToChild("glass", "Metal");
        }
    }
}
