using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();
        public static bool AllUnlockedFinish = false;
        
        public static void CheckAllUnlockedFinish()
        {
            AllUnlockedFinish = Global.Interface.GetSystem<ExpUpgradeSystem>().Items
                .All(i => i.UpgradeFinish);
        }

        public Dictionary<string, ExpUpgradeItem> Dictionary = new();
        
        public Dictionary<string, string> Pairs = new Dictionary<string, string>()
        {
            { "simple_sword", "simple_critical" },
            { "simple_bomb", "simple_fly_count" },
            { "simple_knife", "damage_rate" },
            { "basket_ball", "movement_speed_rate" },
            { "rotate_sword", "simple_exp" },
            
            { "simple_critical", "simple_sword" },
            { "simple_fly_count", "simple_bomb" },
            { "damage_rate", "simple_knife" },
            { "movement_speed_rate", "basket_ball" },
            { "simple_exp", "rotate_sword" },
        };
        
        public Dictionary<string, BindableProperty<bool>> PairedProperties =
            new()
            {
                { "simple_sword", Global.SuperSword },
                { "simple_bomb", Global.SuperBomb },
                { "simple_knife", Global.SuperKnife },
                { "basket_ball", Global.SuperBasketBall },
                { "rotate_sword", Global.SuperRotateSword },

                // simple_exp
                // simple_collectable_area
            };
        

        public ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            Debug.Log("OnInit");
            ResetData();

            Global.Level.Register(_ =>
            {
                Debug.Log("Level Up");
                Roll();
            });
        }

        public void ResetData()
        {
            Items.Clear();

            Add(new ExpUpgradeItem(true)
                .WithKey("simple_sword")
                .WithName("Sword.")
                .WithIconName("simple_sword_icon")
                .WithPairedName("Crafted Sword.")
                .WithPairedIconName("paired_simple_sword_icon")
                .WithPairedDescription("Attack power doubled, attack range doubled.")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Sword Lv{lv}: Attacks nearby enemies",
                        2 => $"Sword Lv{lv}:\n+3 Attack, +2 Quantity",
                        3 => $"Sword Lv{lv}:\n+2 Attack, -0.25s Interval",
                        4 => $"Sword Lv{lv}:\n+2 Attack, -0.25s Interval",
                        5 => $"Sword Lv{lv}:\n+3 Attack, +2 Quantity",
                        6 => $"Sword Lv{lv}:\n+1 Range, -0.25s Interval",
                        7 => $"Sword Lv{lv}:\n+3 Attack, +2 Quantity",
                        8 => $"Sword Lv{lv}:\n+2 Attack, +1 Range",
                        9 => $"Sword Lv{lv}:\n+3 Attack, -0.25s Interval",
                        10 => $"Sword Lv{lv}:\n+3 Attack, +2 Quantity",
                        _ => null
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.SimpleSwordUnlocked.Value = true;
                            break;
                        case 2:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 3:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 4:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 5:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 6:
                            Global.SimpleSwordRange.Value++;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 7:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 8:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimpleSwordRange.Value++;
                            break;
                        case 9:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleAbilityDuration.Value -= 0.25f;
                            break;
                        case 10:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                    }
                })
            );

            Add(new ExpUpgradeItem(true)
                .WithKey("rotate_sword")
                .WithName("Guardian Sword.")
                .WithIconName("rotate_sword_icon")
                .WithPairedName("Crafted Guardian Sword.")
                .WithPairedIconName("paired_rotate_sword_icon")
                .WithPairedDescription("Attack power doubled, rotation speed doubled.")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Guardian Sword Lv{lv}:\nA sword that circles around the character",
                        2 => $"Guardian Sword Lv{lv}:\n+1 Quantity, +1 Attack",
                        3 => $"Guardian Sword Lv{lv}:\n+2 Attack, +25% Speed",
                        4 => $"Guardian Sword Lv{lv}:\n+50% Speed",
                        5 => $"Guardian Sword Lv{lv}:\n+1 Quantity, +1 Attack",
                        6 => $"Guardian Sword Lv{lv}:\n+2 Attack, +25% Speed",
                        7 => $"Guardian Sword Lv{lv}:\n+1 Quantity, +1 Attack",
                        8 => $"Guardian Sword Lv{lv}:\n+2 Attack, +25% Speed",
                        9 => $"Guardian Sword Lv{lv}:\n+1 Quantity, +1 Attack",
                        10 => $"Guardian Sword Lv{lv}:\n+2 Attack, +25% Speed",
                        _ => null
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.RotateSwordUnlocked.Value = true;
                            break;
                        case 2:
                            Global.RotateSwordCount.Value++;
                            Global.RotateSwordDamage.Value++;
                            break;
                        case 3:
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordSpeed.Value *= 1.25f;
                            break;
                        case 4:
                            Global.RotateSwordSpeed.Value *= 1.5f;
                            break;
                        case 5:
                            Global.RotateSwordCount.Value++;
                            Global.RotateSwordDamage.Value++;
                            break;
                        case 6:
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordSpeed.Value *= 1.25f;
                            break;
                        case 7:
                            Global.RotateSwordCount.Value++;
                            Global.RotateSwordDamage.Value++;
                            break;
                        case 8:
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordSpeed.Value *= 1.25f;
                            break;
                        case 9:
                            Global.RotateSwordCount.Value++;
                            Global.RotateSwordDamage.Value++;
                            break;
                        case 10:
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordDamage.Value++;
                            Global.RotateSwordSpeed.Value *= 1.25f;
                            break;
                    }
                })
            );

            Add(new ExpUpgradeItem(false)
                .WithKey("simple_bomb")
                .WithName("Bomb.")
                .WithIconName("bomb_icon")
                .WithPairedName("Crafted Bomb.")
                .WithPairedIconName("paired_bomb_icon")
                .WithPairedDescription("Explodes every 15 seconds.")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Bomb Lv{lv}:\nAttacks all enemies (Dropped by monsters)",
                        2 => $"Bomb Lv{lv}:\n+5% Drop Rate, +5 Attack",
                        3 => $"Bomb Lv{lv}:\n+5% Drop Rate, +5 Attack",
                        4 => $"Bomb Lv{lv}:\n+5% Drop Rate, +5 Attack",
                        5 => $"Bomb Lv{lv}:\n+5% Drop Rate, +5 Attack",
                        6 => $"Bomb Lv{lv}:\n+5% Drop Rate, +5 Attack",
                        7 => $"Bomb Lv{lv}:\n+5% Drop Rate, +5 Attack",
                        8 => $"Bomb Lv{lv}:\n+5% Drop Rate, +5 Attack",
                        9 => $"Bomb Lv{lv}:\n+5% Drop Rate, +5 Attack",
                        10 => $"Bomb Lv{lv}:\n+10% Drop Rate, +5 Attack",
                        _ => null
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.BombUnlocked.Value = true;
                            break;
                        case 2:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 3:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 4:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 5:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 6:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 7:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 8:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 9:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.05f;
                            break;
                        case 10:
                            Global.BombDamage.Value += 5;
                            Global.BombPercent.Value += 0.1f;
                            break;
                    }
                })
            );

            Add(new ExpUpgradeItem(true)
                .WithKey("simple_knife")
                .WithName("Throwing Knife.")
                .WithIconName("simple_knife_icon")
                .WithPairedName("Crafted Throwing Knife.")
                .WithPairedIconName("paired_simple_knife_icon")
                .WithPairedDescription("Attack power doubled.")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Throwing Knife Lv{lv}:\nFires a knife at the nearest enemy",
                        2 => $"Throwing Knife Lv{lv}:\n+3 Attack, +2 Quantity",
                        3 => $"Throwing Knife Lv{lv}:\n-0.1s Interval, +1 Attack, +1 Quantity",
                        4 => $"Throwing Knife Lv{lv}:\n-0.1s Interval, +1 Pierce, +1 Quantity",
                        5 => $"Throwing Knife Lv{lv}:\n+3 Attack, +1 Quantity",
                        6 => $"Throwing Knife Lv{lv}:\n-0.1s Interval, +1 Quantity",
                        7 => $"Throwing Knife Lv{lv}:\n-0.1s Interval, +1 Pierce, +1 Quantity",
                        8 => $"Throwing Knife Lv{lv}:\n+3 Attack, +1 Quantity",
                        9 => $"Throwing Knife Lv{lv}:\n-0.1s Interval, +1 Quantity",
                        10 => $"Throwing Knife Lv{lv}:\n+3 Attack, +1 Quantity",
                        _ => null
                    };
                })
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.SimpleKnifeUnlocked.Value = true;
                            break;
                        case 2:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value += 2;
                            break;
                        case 3:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeDamage.Value += 2;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 4:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeAttackCount.Value++;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 5:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 6:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 7:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeAttackCount.Value++;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 8:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 9:
                            Global.SimpleKnifeDuration.Value -= 0.1f;
                            Global.SimpleKnifeCount.Value++;
                            break;
                        case 10:
                            Global.SimpleKnifeDamage.Value += 3;
                            Global.SimpleKnifeCount.Value++;
                            break;
                    }
                })
            );


            Add(new ExpUpgradeItem(true)
                .WithKey("basket_ball")
                .WithName("Basketball.")
                .WithIconName("ball_icon")
                .WithPairedName("Crafted Basketball.")
                .WithPairedIconName("paired_ball_icon")
                .WithPairedDescription("Attack power doubled, size increased.")
                .WithMaxLevel(10)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Basketball Lv{lv}:\nA basketball that bounces within the screen",
                        2 => $"Basketball Lv{lv}:\n+3 Attack",
                        3 => $"Basketball Lv{lv}:\n+1 Quantity",
                        4 => $"Basketball Lv{lv}:\n+3 Attack",
                        5 => $"Basketball Lv{lv}:\n+1 Quantity",
                        6 => $"Basketball Lv{lv}:\n+3 Attack",
                        7 => $"Basketball Lv{lv}:\n+20% Speed",
                        8 => $"Basketball Lv{lv}:\n+3 Attack",
                        9 => $"Basketball Lv{lv}:\n+20% Speed",
                        10 => $"Basketball Lv{lv}:\n+1 Quantity",
                        _ => null
                    };
                })
                .WithMaxLevel(10)
                .OnUpgrade((_, level) =>
                {
                    switch (level)
                    {
                        case 1:
                            Global.BasketBallUnlocked.Value = true;
                            break;
                        case 2:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 3:
                            Global.BasketBallCount.Value++;
                            break;
                        case 4:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 5:
                            Global.BasketBallCount.Value++;
                            break;
                        case 6:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 7:
                            Global.BasketBallSpeed.Value *= 1.2f;
                            break;
                        case 8:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 9:
                            Global.BasketBallSpeed.Value *= 1.2f;
                            break;
                        case 10:
                            Global.BasketBallCount.Value++;
                            break;
                    }
                })
            );


            Add(new ExpUpgradeItem(false)
                .WithKey("simple_critical")
                .WithName("Critical Hit.")
                .WithIconName("critical_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Critical Hit Lv{lv}:\n15% chance to deal critical damage",
                        2 => $"Critical Hit Lv{lv}:\n28% chance to deal critical damage",
                        3 => $"Critical Hit Lv{lv}:\n43% chance to deal critical damage",
                        4 => $"Critical Hit Lv{lv}:\n50% chance to deal critical damage",
                        5 => $"Critical Hit Lv{lv}:\n80% chance to deal critical damage",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.CriticalRate.Value = 0.15f;
                            break;
                        case 2:
                            Global.CriticalRate.Value = 0.28f;
                            break;
                        case 3:
                            Global.CriticalRate.Value = 0.43f;
                            break;
                        case 4:
                            Global.CriticalRate.Value = 0.5f;
                            break;
                        case 5:
                            Global.CriticalRate.Value = 0.8f;
                            break;
                    }
                }));
     
            Add(new ExpUpgradeItem(false)
                .WithKey("damage_rate")
                .WithName("Damage Rate.")
                .WithIconName("damage_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Damage Rate Lv{lv}:\nIncreases extra damage by 20%",
                        2 => $"Damage Rate Lv{lv}:\nIncreases extra damage by 40%",
                        3 => $"Damage Rate Lv{lv}:\nIncreases extra damage by 60%",
                        4 => $"Damage Rate Lv{lv}:\nIncreases extra damage by 80%",
                        5 => $"Damage Rate Lv{lv}:\nIncreases extra damage by 100%",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.DamageRate.Value = 1.2f;
                            break;
                        case 2:
                            Global.DamageRate.Value = 1.4f;
                            break;
                        case 3:
                            Global.DamageRate.Value = 1.6f;
                            break;
                        case 4:
                            Global.DamageRate.Value = 1.8f;
                            break;
                        case 5:
                            Global.DamageRate.Value = 2f;
                            break;
                    }
                }));
  
            Add(new ExpUpgradeItem(false)
                .WithKey("simple_fly_count")
                .WithIconName("fly_icon")
                .WithName("Projectile.")
                .WithMaxLevel(3)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Projectile Lv{lv}:\nAdds 1 extra projectile",
                        2 => $"Projectile Lv{lv}:\nAdds 2 extra projectiles",
                        3 => $"Projectile Lv{lv}:\nAdds 3 extra projectiles",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.AdditionalFlyThingCount.Value++;
                            break;
                        case 2:
                            Global.AdditionalFlyThingCount.Value++;
                            break;
                        case 3:
                            Global.AdditionalFlyThingCount.Value++;
                            break;
                    }
                }));

            Add(new ExpUpgradeItem(false)
                .WithKey("movement_speed_rate")
                .WithName("Movement Speed.")
                .WithIconName("movement_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Movement Speed Lv{lv}:\nIncreases movement speed by 25%",
                        2 => $"Movement Speed Lv{lv}:\nIncreases movement speed by 50%",
                        3 => $"Movement Speed Lv{lv}:\nIncreases movement speed by 75%",
                        4 => $"Movement Speed Lv{lv}:\nIncreases movement speed by 100%",
                        5 => $"Movement Speed Lv{lv}:\nIncreases movement speed by 150%",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.MovementSpeedRate.Value = 1.25f;
                            break;
                        case 2:
                            Global.MovementSpeedRate.Value = 1.5f;
                            break;
                        case 3:
                            Global.MovementSpeedRate.Value = 1.75f;
                            break;
                        case 4:
                            Global.MovementSpeedRate.Value = 2f;
                            break;
                        case 5:
                            Global.MovementSpeedRate.Value = 2.5f;
                            break;
                    }
                }));

            Add(new ExpUpgradeItem(false)
                .WithKey("simple_collectable_area")
                .WithName("Pickup Range.")
                .WithIconName("collectable_icon")
                .WithMaxLevel(3)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Pickup Range Lv{lv}:\nIncreases pickup range by 100%",
                        2 => $"Pickup Range Lv{lv}:\nIncreases pickup range by 200%",
                        3 => $"Pickup Range Lv{lv}:\nIncreases pickup range by 300%",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.CollectableArea.Value = 2f;
                            break;
                        case 2:
                            Global.CollectableArea.Value = 3f;
                            break;
                        case 3:
                            Global.CollectableArea.Value = 4f;
                            break;
                    }
                }));

            Add(new ExpUpgradeItem(false)
                .WithKey("simple_exp")
                .WithName("EXP")
                .WithIconName("exp_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"Experience Lv{lv}:\nIncreases drop rate by 5%",
                        2 => $"Experience Lv{lv}:\nIncreases drop rate by 8%",
                        3 => $"Experience Lv{lv}:\nIncreases drop rate by 12%",
                        4 => $"Experience Lv{lv}:\nIncreases drop rate by 17%",
                        5 => $"Experience Lv{lv}:\nIncreases drop rate by 25%",
                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.AdditionalExpPercent.Value = 0.05f;
                            break;
                        case 2:
                            Global.AdditionalExpPercent.Value = 0.08f;
                            break;
                        case 3:
                            Global.AdditionalExpPercent.Value = 0.12f;
                            break;
                        case 4:
                            Global.AdditionalExpPercent.Value = 0.17f;
                            break;
                        case 5:
                            Global.AdditionalExpPercent.Value = 0.25f;
                            break;
                    }
                }));

            Dictionary = Items.ToDictionary(i => i.Key);
        }

        public void Roll()
        {
            foreach (var expUpgradeItem in Items)
            {
                expUpgradeItem.Visible.Value = false;
            }

            var list = Items.Where(item => !item.UpgradeFinish).ToList();

            if (list.Count >= 4)
            {
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
            }
            else
            {
                foreach (var item in list)
                {
                    item.Visible.Value = true;
                }
            }
        }
    }
}