using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Data References")]
    public EnemyDatabase enemyDB;
    private Camera mainCamera;

    [Header("Spawn Area (Dikdörtgen Bölge)")]
    public Transform spawnPointA;
    public Transform spawnPointB;

    [Header("Spawn Settings")]
    public float initialSpawnRate = 3f;
    public float minSpawnRate = 0.5f;
    public float difficultyIncreaseRate = 0.02f;

    [Tooltip("Nokta kamera içindeyse kaç kez yeni nokta denensin?")]
    public int maxSpawnAttempts = 10;

    private float currentSpawnRate;
    private float nextSpawnTime;
    private int totalWeight;

    void Start()
    {
        mainCamera = Camera.main;
        currentSpawnRate = initialSpawnRate;
        CalculateTotalWeight();
    }

    void Update()
    {
        // 1. Zorluk Ölçeklendirme
        if (currentSpawnRate > minSpawnRate)
            currentSpawnRate -= difficultyIncreaseRate * Time.deltaTime;

        // 2. Spawn Zamanlaması
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemyInValidRegion();
            nextSpawnTime = Time.time + currentSpawnRate;
        }
    }

    void CalculateTotalWeight()
    {
        totalWeight = 0;
        foreach (var enemy in enemyDB.enemies)
            totalWeight += enemy.weight;
    }

    void SpawnEnemyInValidRegion()
    {
        if (enemyDB == null || enemyDB.enemies.Count == 0 || spawnPointA == null || spawnPointB == null) return;

        Vector3 spawnPos = Vector3.zero;
        bool foundValidPoint = false;

        // Sınırları hesapla
        float minX = Mathf.Min(spawnPointA.position.x, spawnPointB.position.x);
        float maxX = Mathf.Max(spawnPointA.position.x, spawnPointB.position.x);
        float minZ = Mathf.Min(spawnPointA.position.z, spawnPointB.position.z);
        float maxZ = Mathf.Max(spawnPointA.position.z, spawnPointB.position.z);

        // Belirlenen alan içinde, kamera dışı nokta bulana kadar dene
        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 testPos = new Vector3(randomX, 0f, randomZ);

            // Kameranın görüş alanını kontrol et
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(testPos);
            bool isInsideCamera = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

            if (!isInsideCamera)
            {
                spawnPos = testPos;
                foundValidPoint = true;
                break;
            }
        }

        // Eğer geçerli (kamera dışı) bir nokta bulunduysa düşmanı oluştur
        if (foundValidPoint)
        {
            int randomNumber = Random.Range(0, totalWeight);
            int currentWeightSum = 0;

            foreach (var enemy in enemyDB.enemies)
            {
                currentWeightSum += enemy.weight;
                if (randomNumber < currentWeightSum)
                {
                    Instantiate(enemy.prefab, spawnPos, Quaternion.identity);
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (spawnPointA == null || spawnPointB == null) return;
        Gizmos.color = Color.cyan;
        Vector3 center = (spawnPointA.position + spawnPointB.position) / 2f;
        Vector3 size = new Vector3(Mathf.Abs(spawnPointA.position.x - spawnPointB.position.x), 1f, Mathf.Abs(spawnPointA.position.z - spawnPointB.position.z));
        Gizmos.DrawWireCube(center, size);
    }
}