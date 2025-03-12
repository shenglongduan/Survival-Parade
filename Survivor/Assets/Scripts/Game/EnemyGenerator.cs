using System;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using Random = UnityEngine.Random;

namespace ProjectSurvivor
{
    public partial class EnemyGenerator : ViewController
    {
        [SerializeField] public LevelConfig Config;

        private float mCurrentGenerateSeconds = 0;
        private float mCurrentWaveSeconds = 0;

        public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);


        private Queue<EnemyWave> mEnemyWavesQueue = new Queue<EnemyWave>();

        public int WaveCount = 0;

        private int mTotalCount = 0;
        public bool LastWave => WaveCount == mTotalCount;
        public EnemyWave CurrentWave => mCurrentWave;

        private void Start()
        {
            foreach (var group in Config.EnemyWaveGroups)
            {
                foreach (var wave in group.Waves)
                {
                    mEnemyWavesQueue.Enqueue(wave);
                    mTotalCount++;
                }
            }
        }

        private EnemyWave mCurrentWave = null;

        private void Update()
        {
            if (mCurrentWave == null)
            {
                if (mEnemyWavesQueue.Count > 0)
                {
                    WaveCount++;
                    mCurrentWave = mEnemyWavesQueue.Dequeue();
                    mCurrentGenerateSeconds = 0;
                    mCurrentWaveSeconds = 0;
                }
            }

            if (mCurrentWave != null)
            {
                mCurrentGenerateSeconds += Time.deltaTime;
                mCurrentWaveSeconds += Time.deltaTime;

                if (mCurrentGenerateSeconds >= mCurrentWave.GenerateDuration)
                {
                    mCurrentGenerateSeconds = 0;

                    var player = Player.Default;
                    if (player)
                    {
                        var xOry = RandomUtility.Choose(-1, 1);
                        var pos = Vector2.zero;
                        if (xOry == -1)
                        {
                            pos.x = RandomUtility.Choose(CameraController.LBTrans.position.x,
                                CameraController.RTTrans.position.x);
                            pos.y = Random.Range(CameraController.LBTrans.position.y,
                                CameraController.RTTrans.position.y);
                        }
                        else
                        {
                            pos.x = Random.Range(CameraController.LBTrans.position.x,
                                CameraController.RTTrans.position.x);
                            pos.y = RandomUtility.Choose(CameraController.LBTrans.position.y,
                                CameraController.RTTrans.position.y);
                        }
                        

                        mCurrentWave.EnemyPrefab.Instantiate()
                            .Position(pos)
                            .Self(self =>
                            {
                                var enemy = self.GetComponent<IEnemy>();
                                enemy.SetSpeedScale(mCurrentWave.SpeedScale);
                                enemy.SetHPScale(mCurrentWave.HPScale);
                            })
                            .Show();
                    }
                }

                if (mCurrentWaveSeconds >= mCurrentWave.Seconds)
                {
                    mCurrentWave = null;
                }
            }
        }
    }
}