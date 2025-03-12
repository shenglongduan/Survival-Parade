using System.Linq;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class AbilityController : ViewController,IController
	{
		void Start()
		{
			// Code Here

			Global.SimpleSwordUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					SimpleSword.Show();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.SimpleKnifeUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					SimpleKnife.Show();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.RotateSwordUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					RotateSword.Show();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.BasketBallUnlocked.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					BasketBallAbility.Show();
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			

			// 随机升级一个
			this.GetSystem<ExpUpgradeSystem>().Items.Where(item=>item.IsWeapon)
				.ToList()
				.GetRandomItem().Upgrade();

			Global.SuperBomb.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					SuperBomb.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
