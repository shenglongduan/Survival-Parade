using System;
using QAssetBundle;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Player : ViewController
    {
        public float MovementSpeed = 5;

        public static Player Default;

        private AudioPlayer mWalkSfx;
        
        private void Awake()
        {
            Default = this;
        }

        private void OnDestroy()
        {
            Default = null;
        }

        void Start()
        {
            HurtBox.OnTriggerEnter2DEvent(collider2D =>
            {
                var hitBox = collider2D.GetComponent<HitHurtBox>();
                if (hitBox)
                {
                    if (hitBox.Owner.CompareTag("Enemy"))
                    {
                        Global.HP.Value--;

                        if (Global.HP.Value <= 0)
                        {
                            AudioKit.PlaySound("Die");
                            this.DestroyGameObjGracefully();

                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
                        else
                        {
                            AudioKit.PlaySound("Hurt");
                        }
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            void UpdateHP()
            {
                HPValue.fillAmount = Global.HP.Value / (float)Global.MaxHP.Value;
            }

            Global.HP.RegisterWithInitValue(hp => { UpdateHP(); }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.MaxHP.RegisterWithInitValue(maxHp => { UpdateHP(); }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private bool mFaceRight = true;

        private void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal"); // 1
            var vertical = Input.GetAxisRaw("Vertical"); // 1
            var targetVelocity = new Vector2(horizontal, vertical).normalized *
                                 (MovementSpeed * Global.MovementSpeedRate.Value);

            if (horizontal == 0 && vertical == 0)
            {
                if (mFaceRight)
                {
                    Sprite.Play("PlayerIdleRight");
                }
                else
                {
                    Sprite.Play("PlayerIdleLeft");
                }

                if (mWalkSfx != null)
                {
                    mWalkSfx.Stop();
                    mWalkSfx = null;
                }
            }
            else
            {
                if (mWalkSfx == null)
                {
                    mWalkSfx = AudioKit.PlaySound(Sfx.WALK, true);
                }
                
                if (horizontal > 0)
                {
                    mFaceRight = true;
                } 
                else if (horizontal < 0)
                {
                    mFaceRight = false;
                }
                
                if (mFaceRight)
                {
                    Sprite.Play("PlayerWalkRight");
                }
                else
                {
                    Sprite.Play("PlayerWalkLeft");
                }
            }

            SelfRigidbody2D.linearVelocity =
                Vector2.Lerp(SelfRigidbody2D.linearVelocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime * 5));
        }
    }
}