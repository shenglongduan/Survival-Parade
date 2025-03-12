using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    [CreateAssetMenu]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        public List<EnemyWaveGroup> EnemyWaveGroups = new List<EnemyWaveGroup>();
    }

    [Serializable]
    public class EnemyWaveGroup
    {
        public string Name;
        [TextArea] public string Description = string.Empty;

        [SerializeField]
        public List<EnemyWave> Waves = new List<EnemyWave>();
    }
    
    [Serializable]
    public class EnemyWave
    {
        public string Name;
        public bool Active = true;
        public float GenerateDuration = 1;
        public GameObject EnemyPrefab;
        public int Seconds = 10;
        public float HPScale = 1.0f;
        public float SpeedScale = 1.0f;
    }
}
