﻿using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public class CoinUpgradeSystem : AbstractSystem,ICanSave
    {
        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();
        
        public List<CoinUpgradeItem> Items { get; } = new List<CoinUpgradeItem>();


        public CoinUpgradeItem Add(CoinUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }
        
        protected override void OnInit()
        {
            Add(new CoinUpgradeItem()
                    .WithKey("coin_percent_lv1")
                    .WithDescription("Coin Drop Rate Increase Lv1")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.CoinPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    }))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("coin_percent_lv2")
                    .WithDescription("Coin Drop Rate Increase Lv2")
                    .WithPrice(1000)
                    .OnUpgrade((item) =>
                    {
                        Global.CoinPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("coin_percent_lv3")
                    .WithDescription("Coin Drop Rate Increase Lv3")
                    .WithPrice(2000)
                    .OnUpgrade((item) =>
                    {
                        Global.CoinPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("coin_percent_lv4")
                    .WithDescription("Coin Drop Rate Increase Lv4")
                    .WithPrice(5000)
                    .OnUpgrade((item) =>
                    {
                        Global.CoinPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                ;
            
            Items.Add(new CoinUpgradeItem()
                .WithKey("exp_percent")
                .WithDescription("EXP Drop Rate Increase")
                .WithPrice(5)
                .OnUpgrade((item) =>
                {
                    Global.ExpPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));

            Add(new CoinUpgradeItem()
                    .WithKey("max_hp")
                    .WithDescription("Max HP+1")
                    .WithPrice(1000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    }))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp1")
                    .WithDescription("Max HP+1")
                    .WithPrice(2000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp2")
                    .WithDescription("Max HP+1")
                    .WithPrice(3000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp3")
                    .WithDescription("Max HP+1")
                    .WithPrice(4000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp4")
                    .WithDescription("Max HP+1")
                    .WithPrice(5000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp5")
                    .WithDescription("Max HP+1")
                    .WithPrice(6000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp6")
                    .WithDescription("Max HP+1")
                    .WithPrice(7000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp7")
                    .WithDescription("Max HP+1")
                    .WithPrice(8000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp8")
                    .WithDescription("Max HP+1")
                    .WithPrice(9000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .WithKey("max_hp9")
                    .WithDescription("Max HP+1")
                    .WithPrice(10000)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value++;
                        Global.Coin.Value -= item.Price;
                    })));
            
            Load();

            OnCoinUpgradeSystemChanged.Register(() =>
            {
                Save();
            });
        }

        public void Say()
        {
            Debug.Log("Hello CoinUpgradeSystem");
        }

        public void Save()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var coinUpgradeItem in Items)
            {
                saveSystem.SaveBool(coinUpgradeItem.Key, coinUpgradeItem.UpgradeFinish);
            }
        }

        public void Load()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            foreach (var coinUpgradeItem in Items)
            {
                coinUpgradeItem.UpgradeFinish = saveSystem.LoadBool(coinUpgradeItem.Key, false);
            }
        }
    }
}