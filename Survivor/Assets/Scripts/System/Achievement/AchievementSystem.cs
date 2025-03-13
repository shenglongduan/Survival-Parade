using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public class AchievementSystem : AbstractSystem
    {
        public AchievementItem Add(AchievementItem item)
        {
            Items.Add(item);
            return item;
        }
        
        protected override void OnInit()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            
            Add(new AchievementItem()
                    .WithKey("3_minutes")
                    .WithName("Hold on for three minutes.")
                    .WithDescription("Hold on for three minutes.\nReward: 1000 coins.")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 3)
                    //.Condition(() => Global.CurrentSeconds.Value >= 10)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("5_minutes")
                    .WithName("Hold on for five minutes.")
                    .WithDescription("Hold on for five minutes.\nReward: 1000 coins.")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 5)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("10_minutes")
                    .WithName("Hold on for ten minutes.")
                    .WithDescription("Hold on for ten minutes.\nReward: 1000 coins.")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 10)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("15_minutes")
                    .WithName("Hold on for 15 minutes.")
                    .WithDescription("Hold on for 15 minutes.\nReward: 1000 coins.")
                    .WithIconName("achievement_time_icon")
                    .Condition(() => Global.CurrentSeconds.Value >= 60 * 15)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("lv30")
                    .WithName("lv30")
                    .WithDescription("First time reaching level 30.\nReward: 1000 coins.")
                    .WithIconName("achievement_level_icon")
                    .Condition(() => Global.Level.Value >= 30)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("lv50")
                    .WithName("lv50")
                    .WithDescription("First time reaching level 50.\nReward: 1000 coins.")
                    .WithIconName("achievement_level_icon")
                    .Condition(() => Global.Level.Value >= 50)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_ball")
                    .WithName("Unlock basketball.")
                    .WithDescription("First time unlocking a basketball.\nReward: 1000 coins.")
                    .WithIconName("paired_ball_icon")
                    .Condition(() => Global.SuperBasketBall.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_bomb")
                    .WithName("Unlocked bomb.")
                    .WithDescription("First time unlocking a bomb.\nReward: 1000 coins.")
                    .WithIconName("paired_bomb_icon")
                    .Condition(() => Global.SuperBomb.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_sword")
                    .WithName("Unlock sword.")
                    .WithDescription("First time unlocking a sword.\nReward: 1000 coins.")
                    .WithIconName("paired_simple_sword_icon")
                    .Condition(() => Global.SuperSword.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_knife")
                    .WithName("Unlock throwing knife.")
                    .WithDescription("First time unlocking a throwing knife.\nReward: 1000 coins.")
                    .WithIconName("paired_simple_knife_icon")
                    .Condition(() => Global.SuperKnife.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_circle")
                    .WithName("Unlock Guardian Sword.")
                    .WithDescription("First time unlocking the Guardian Sword.\nReward: 1000 coins.")
                    .WithIconName("paired_rotate_sword_icon")
                    .Condition(() => Global.SuperRotateSword.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_circle")
                    .WithName("All abilities upgraded.")
                    .WithDescription("All abilities fully upgraded.\nReward: 1000 coins.")
                    .WithIconName("achievement_all_icon")
                    .Condition(() => ExpUpgradeSystem.AllUnlockedFinish)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            ActionKit.OnUpdate.Register(() =>
            {
                if (Time.frameCount % 10 == 0)
                {
                    foreach (var achievementItem in Items.Where(achievementItem =>
                                 !achievementItem.Unlocked && achievementItem.ConditionCheck()))
                    {
                        achievementItem.Unlock(saveSystem);
                    }
                }
            });
        }
        public List<AchievementItem> Items = new List<AchievementItem>();

        public static EasyEvent<AchievementItem> OnAchievementUnlocked = new EasyEvent<AchievementItem>();
    }
}