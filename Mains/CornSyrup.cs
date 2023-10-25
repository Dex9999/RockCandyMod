/*using KitchenData;
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

namespace MethMod.Mains
{
    internal class CornSyrup : CustomItemGroup
    {
            public override string UniqueNameID => "CornSyrup";
            public override GameObject Prefab => Mod.Bundle.LoadAsset<GameObject>("flask");
            public override Item DisposesTo => Mod.Pot;


            public override List<ItemGroup.ItemSet> Sets => new List<ItemGroup.ItemSet>
            {
                new ItemGroup.ItemSet
                {
                    Min = 2,
                    Max = 2,
                    Items = new List<Item>
                    {
                        Mod.Water,
                        Refs.Water
                    }
                }
            };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Min = 2,
                Max = 2,
                Items = new List<Item>
                {
                    Refs.UncookedBoba,
                    Refs.Water
                }
            },
            new()
            {
                Min = 1,
                Max = 1,
                IsMandatory = true,
                Items = new List<Item>
                {
                    Refs.Pot
                }
            }
        };

            public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = Refs.Cook,
                Result = Refs.CookedBobaPot,
                Duration = 4
            }
        };

            public override void SetupPrefab(GameObject prefab)
            {
                prefab.SetupMaterialsLikePot();
                prefab.GetChild("BobaBalls").ApplyMaterialToChildren("Ball", "UncookedBoba");

                prefab.GetComponent<UncookedBobaPotView>()?.Setup(prefab);
            }
        
    }
}
*/