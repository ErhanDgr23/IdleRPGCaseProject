using UnityEngine;
using System;

[RequireComponent(typeof(PlayerStats))]
public class PlayerHealth : MonoBehaviour
{
    private PlayerStats stats;
    private float currentHealth;

    public event Action OnPlayerDeath;
    public event Action<float, float> OnHealthChanged;

    [Header("UI")]
    public HealthBar floatingHealthBar; // Oyuncunun kafasındaki can barı (Opsiyonel)

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        currentHealth = stats.maxHealth;

        UpdateHealthUI();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            GetComponent<GlobalAnimator>().TriggerHit();
        }
    }

    private void UpdateHealthUI()
    {
        OnHealthChanged?.Invoke(currentHealth, stats.maxHealth);

        // Eğer oyuncunun üzerinde yüzen bir can barı varsa onu da güncelle
        if (floatingHealthBar != null)
        {
            floatingHealthBar.UpdateHealth(currentHealth, stats.maxHealth);
        }
    }

    private void Die()
    {
        GetComponent<GlobalAnimator>().TriggerDeath();
        OnPlayerDeath?.Invoke(); // Event'i fırlat (Kamera ve Manager duyacak)

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;

        // Sadece Collider'ı kapatıyoruz. Rigidbody aktif kalacağı için karakter 
        // yerçekimiyle aşağı düşmeye başlayacak.
        GetComponent<Collider>().enabled = false;

        if (floatingHealthBar != null) floatingHealthBar.gameObject.SetActive(false);
    }
}