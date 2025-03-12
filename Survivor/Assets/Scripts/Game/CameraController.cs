using System;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
    public partial class CameraController : ViewController
    {
        private Vector2 mTargetPosition = Vector2.zero;

        private static CameraController mDefault = null;

        public static Transform LBTrans => mDefault.LB;
        public static Transform RTTrans => mDefault.RT;
        private void Awake()
        {
            mDefault = this;
        }

        private void OnDestroy()
        {
            mDefault = null;
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
        }


        private Vector3 mCurrentCameraPos;
        private bool mShake = false;
        private int mShakeFrame = 0;
        private float mShakeA = 2.0f; // 振幅

        public static void Shake()
        {
            mDefault.mShake = true;
            mDefault.mShakeFrame = 30;
            mDefault.mShakeA = 0.2f;
        }

        private void Update()
        {
            if (Player.Default)
            {
                mTargetPosition = Player.Default.transform.position;
                mCurrentCameraPos.x = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.x, mTargetPosition.x);
                mCurrentCameraPos.y = (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.y, mTargetPosition.y);

                mCurrentCameraPos.z = transform.position.z;
                
                if (mShake)
                {
                    mShakeFrame--;
                    var shakeA = Mathf.Lerp(mShakeA, 0.0f, (mShakeFrame / 30.0f));
                    transform.position = new Vector3(mCurrentCameraPos.x + Random.Range(-shakeA, shakeA),
                        mCurrentCameraPos.y + Random.Range(-shakeA, shakeA), mCurrentCameraPos.z);

                    if (mShakeFrame <= 0)
                    {
                        mShake = false;
                    }
                }
                else
                {
                    transform.PositionX(
                        (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                        .Lerp(transform.position.x, mTargetPosition.x));

                    transform.PositionY(
                        (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                        .Lerp(transform.position.y, mTargetPosition.y));
                }
            }
        }
    }
}