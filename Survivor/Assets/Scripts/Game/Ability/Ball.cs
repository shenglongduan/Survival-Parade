using QAssetBundle;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
	public partial class Ball : ViewController
	{
		void Start()
		{
			SelfRigidbody2D.linearVelocity = 
				new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) *
				Random.Range(Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2);;

			Global.SuperBasketBall.RegisterWithInitValue(unlocked =>
			{
				if (unlocked)
				{
					this.LocalScale(3);
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			HurtBox.OnTriggerEnter2DEvent(collider =>
			{
				var hurtBox = collider.GetComponent<HitHurtBox>();
				if (hurtBox)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						var enemy = hurtBox.Owner.GetComponent<IEnemy>();
						var damageTimes = Global.SuperBasketBall.Value ? Random.Range(2, 3 + 1) : 1;
						DamageSystem.CalculateDamage(Global.BasketBallDamage.Value * damageTimes,enemy);
						
						if (Random.Range(0, 1f) < 0.5f && collider && collider.attachedRigidbody &&
						    Player.Default)
						{
							collider.attachedRigidbody.linearVelocity =
								collider.NormalizedDirection2DFrom(this) * 5 +
								collider.NormalizedDirection2DFrom(Player.Default) * 10;
						}
					}
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			var normal = other.GetContact(0).normal;

			if (normal.x > normal.y)
			{
				var rb = SelfRigidbody2D;
				rb.linearVelocity = new Vector2(rb.linearVelocity.x,
					Mathf.Sign(rb.linearVelocity.y) * Random.Range(0.5f, 1.5f) *
					Random.Range(Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2));

				rb.angularVelocity = Random.Range(-360f, 360f);
			}
			else
			{
				var rb = SelfRigidbody2D;
                rb.linearVelocity =
                    new Vector2(
                        Mathf.Sign(rb.linearVelocity.x) * Random.Range(0.5f, 1.5f) * Random.Range(
                            Global.BasketBallSpeed.Value - 2, Global.BasketBallSpeed.Value + 2),
                        rb.linearVelocity.y);

                rb.angularVelocity = Random.Range(-360f, 360f);
			}

			AudioKit.PlaySound(Sfx.BALL);
		}
	}
}
