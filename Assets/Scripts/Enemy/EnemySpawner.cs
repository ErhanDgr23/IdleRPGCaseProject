using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Data (Data)")]
    // ScriptableObject'ten arındırılmış, doğrudan Inspector'da doldurulacak liste
    public List<EnemyData> enemyList = new List<EnemyData>();

    [Header("Spawn Sınırları (Point A & B)")]
    // Sahnede oluşturduğun SpawnPoint_A ve SpawnPoint_B Empty Object'leri buraya ata
    public Transform spawnPointA;
    public Transform spawnPointB;

    [Header("Zorluk ve Hız Ayarları")]
    public float initialSpawnRate = 3f; // Başlangıç spawn hızı (saniyede)
    public float minSpawnRate = 0.5f;   // Maksimum spawn hızı (saniyede)
    public float difficultyIncreaseRate = 0.02f; // Saniyede hız artışı

    private float currentSpawnRate;
    private float nextSpawnTime;
    private int totalWeight;
    private Camera targetCamera; // Inspector'dan atanan ana kamera

    void Start()
    {
        // Eğer Inspector'da atanmamışsa, otomatik bulmaya çalış
        if (targetCamera == null) targetCamera = Camera.main;

        currentSpawnRate = initialSpawnRate;
        CalculateTotalWeight();

        // Kritik Kontrol: Sahnede mutlaka SpawnPoint'ler olmalı!
        if (spawnPointA == null || spawnPointB == null)
        {
            Debug.LogError("DİKKAT: EnemySpawner objesinde SpawnPointA veya SpawnPointB atanmamış! Düşmanlar spawn olmayacak.");
            return;
        }
    }

    void Update()
    {
        // Eğer sistem hazır değilse güncelleme yapma
        if (spawnPointA == null || spawnPointB == null) return;

        // 1. Zorluk Hızını Artır (Difficulty Scaling)
        if (currentSpawnRate > minSpawnRate)
        {
            currentSpawnRate -= difficultyIncreaseRate * Time.deltaTime;
        }

        // 2. Spawn Döngüsü
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + currentSpawnRate;
        }
    }

    // Ağırlıklı spawn için toplam ağırlığı hesapla
    void CalculateTotalWeight()
    {
        totalWeight = 0;
        foreach (var enemy in enemyList)
        {
            totalWeight += enemy.spawnWeight;
        }
    }

    // Düşmanı spawn et
    void SpawnEnemy()
    {
        if (enemyList.Count == 0) return;

        // 1. Koordinat Belirle: Sınırlar içinden rastgele bir nokta seç
        Vector3 spawnPos = GetRandomPointInRegion();

        // 2. Ağırlıklı Rastgele Düşman Seç
        GameObject selectedPrefab = GetWeightedRandomEnemy();

        if (selectedPrefab != null)
        {
            // Düşmanı oluştur
            Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        }
    }

    // Ağırlıklı rastgele seçim fonksiyonu
    GameObject GetWeightedRandomEnemy()
    {
        int randomNumber = Random.Range(0, totalWeight);
        int currentSum = 0;

        foreach (var enemy in enemyList)
        {
            currentSum += enemy.spawnWeight;
            if (randomNumber < currentSum)
            {
                return enemy.prefab;
            }
        }
        return null;
    }

    // İki nokta arasındaki sınırlar içinde rastgele bir nokta oluştur
    Vector3 GetRandomPointInRegion()
    {
        // İki nokta arasındaki X ve Z sınırlarını hesapla
        float minX = Mathf.Min(spawnPointA.position.x, spawnPointB.position.x);
        float maxX = Mathf.Max(spawnPointA.position.x, spawnPointB.position.x);
        float minZ = Mathf.Min(spawnPointA.position.z, spawnPointB.position.z);
        float maxZ = Mathf.Max(spawnPointA.position.z, spawnPointB.position.z);

        // Belirlenen sınırlar içinde rastgele bir pozisyon oluştur
        // Y yüksekliği için spawnPointA'nın yüksekliğini referans alıyoruz
        return new Vector3(Random.Range(minX, maxX), spawnPointA.position.y, Random.Range(minZ, maxZ));
    }
}