using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    [Header("Skill Thresholds")]
    public int multiShotThreshold = 10;
    public int poisonArrowThreshold = 25;
    public int arrowRainThreshold = 50;

    [Header("Skill References")]
    public GameObject arrowPrefab; // Inspector'dan oku sürükle!

    public bool IsMultiShotActive { get; private set; }
    public bool IsPoisonActive { get; private set; }
    public bool IsArrowRainActive { get; private set; }
    public int CurrentKillCount { get; private set; }

    private float arrowRainTimer = 0f;

    void Awake() { Instance = this; }

    void Update()
    {
        if (IsArrowRainActive)
        {
            arrowRainTimer += Time.deltaTime;
            if (arrowRainTimer >= 5f)
            {
                ExecuteArrowRain();
                arrowRainTimer = 0f;
            }
        }
    }

    private void ExecuteArrowRain()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        int count = Mathf.Min(enemies.Length, 5);
        for (int i = 0; i < count; i++)
        {
            Transform targetEnemy = enemies[Random.Range(0, enemies.Length)].transform;

            // Düşmanın tam tepesinde gökyüzünde oluştur
            Vector3 spawnPos = targetEnemy.position + Vector3.up * 12f;
            GameObject rainArrow = Instantiate(arrowPrefab, spawnPos, Quaternion.LookRotation(Vector3.down));

            // Oku aşağı doğru fırlat (ProjectileArrow scripti hızı Rigidbody'den almalı)
            ProjectileArrow arrowScript = rainArrow.GetComponent<ProjectileArrow>();
            if (arrowScript != null)
            {
                // Hasarı PlayerStats'tan çekebilirsin, şimdilik sabit 15 verelim
                arrowScript.Initialize(Vector3.down, 15f, IsPoisonActive);
            }
        }
    }

    public void AddKill()
    {
        CurrentKillCount++;
        CheckSkillUnlocks();
    }

    private void CheckSkillUnlocks()
    {
        if (!IsMultiShotActive && CurrentKillCount >= multiShotThreshold)
        {
            IsMultiShotActive = true;
            SkillPopupUI.Instance.ShowPopup("MULTI-SHOT!", "3 Arrows at once!");
        }
        if (!IsPoisonActive && CurrentKillCount >= poisonArrowThreshold)
        {
            IsPoisonActive = true;
            SkillPopupUI.Instance.ShowPopup("POISON!", "Damage over time!");
        }
        if (!IsArrowRainActive && CurrentKillCount >= arrowRainThreshold)
        {
            IsArrowRainActive = true;
            SkillPopupUI.Instance.ShowPopup("ARROW RAIN!", "Death from above!");
        }
    }
}