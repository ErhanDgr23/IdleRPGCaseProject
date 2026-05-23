using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject prefab;
    public int weight; // Çıkma olasılığı (Yüksek rakam = daha sık çıkar)
}

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "ScriptableObjects/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemySpawnData> enemies;
}