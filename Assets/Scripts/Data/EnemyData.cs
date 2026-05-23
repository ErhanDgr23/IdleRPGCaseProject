using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string enemyName;
    public GameObject prefab;
    [Range(0, 100)]
    public int spawnWeight; // Çıkma olasılığı
}