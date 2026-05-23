using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseEnemy : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 20f;
    public float moveSpeed = 3f;
    public float damage = 10f;
    public int goldReward = 5;

    [Tooltip("Düşmanın oyuncuya ne kadar yaklaşınca duracağı")]
    public float stoppingDistance = 1.5f;

    [Header("Attack Settings")]
    [Tooltip("Düşman durma mesafesindeyken kaç saniyede bir hasar versin?")]
    public float attackCooldown = 1f;
    protected float attackTimer = 0f;

    [Header("Hit Settings")]
    public float stunDuration = 0.3f;

    [Header("UI")]
    public HealthBar healthBar;

    [Header("Drops")]
    public GameObject goldPrefab;

    [Header("Feedback")]
    public GameObject damagePopupPrefab; // Hasar yazısı prefab'ı

    [Header("Material Swap")]
    public Renderer enemyRenderer;
    public Material poisonMaterial;
    private Material originalMaterial;

    protected float currentStunTimer = 0f;
    protected float currentHealth;

    protected Transform targetPlayer;
    protected PlayerHealth playerHealth; // Hasar vermek için oyuncunun can referansı

    protected Rigidbody rb;
    protected GlobalAnimator charAnimator;
    protected Collider enemyCollider;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        charAnimator = GetComponent<GlobalAnimator>();
        enemyCollider = GetComponent<Collider>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            targetPlayer = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>(); // Can scriptini önbelleğe al
        }

        if (enemyRenderer != null)
        {
            // Sahne başladığında düşmanın kendi materyalini orijinal olarak kaydet
            originalMaterial = enemyRenderer.material;
        }

        if (healthBar != null) healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    protected virtual void FixedUpdate()
    {
        if (currentHealth <= 0) return;

        // Sersemleme (Stun) kontrolü
        if (currentStunTimer > 0f)
        {
            currentStunTimer -= Time.fixedDeltaTime;
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            if (charAnimator != null) charAnimator.SetRunning(false);
            return;
        }

        if (targetPlayer != null)
        {
            MoveTowardsPlayer();
        }
    }

    protected virtual void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

        if (distanceToPlayer > stoppingDistance)
        {
            // --- YÜRÜME DURUMU ---
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            direction.y = 0f;

            rb.linearVelocity = new Vector3(direction.x * moveSpeed, rb.linearVelocity.y, direction.z * moveSpeed);

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            if (charAnimator != null) charAnimator.SetRunning(true);

            // Düşman yürürken saldırı süresini sıfırda tutalım ki, menzile girer girmez anında ilk vuruşunu yapsın.
            attackTimer = 0f;
        }
        else
        {
            // --- SALDIRI (DURMA) DURUMU ---
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);

            Vector3 lookDirection = (targetPlayer.position - transform.position).normalized;
            lookDirection.y = 0f;
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }

            if (charAnimator != null) charAnimator.SetRunning(false);

            // Geri Sayım ve Hasar Verme
            attackTimer -= Time.fixedDeltaTime;

            if (attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackCooldown; // Vurduktan sonra süreyi başa sar
            }
        }
    }

    protected virtual void AttackPlayer()
    {
        if (playerHealth != null)
        {
            // Oyuncuya tek seferlik net bir hasar ver
            playerHealth.TakeDamage(damage);

            // Eğer düşman modellerinin de bir "Attack" animasyonu varsa onu oynat
            if (charAnimator != null)
            {
                charAnimator.TriggerAttack();
            }
        }
    }

    public virtual void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;

        if (healthBar != null) healthBar.UpdateHealth(currentHealth, maxHealth);

        // --- HASAR YAZISI SPAWN İŞLEMİ ---
        if (damagePopupPrefab != null)
        {
            // Yazıların üst üste binmemesi için rastgele küçük bir ofset (sapma) ekliyoruz
            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), 1f, Random.Range(-0.5f, 0.5f));
            Vector3 spawnPos = transform.position + randomOffset;

            GameObject popup = Instantiate(damagePopupPrefab, spawnPos, Quaternion.identity);

            // DamagePopup betiğindeki Setup fonksiyonuna hasar miktarını gönder
            popup.GetComponent<DamagePopup>().Setup(amount);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (charAnimator != null) charAnimator.TriggerHit();
            currentStunTimer = stunDuration;
        }
    }

    protected virtual void Die()
    {
        SkillManager.Instance.AddKill();

        if (charAnimator != null) charAnimator.TriggerDeath();
        if (enemyCollider != null) enemyCollider.enabled = false;
        if (rb != null) rb.isKinematic = true;
        if (healthBar != null) healthBar.gameObject.SetActive(false);

        // ALTIN DÜŞÜRME
        if (goldPrefab != null)
        {
            // Altını düşmanın biraz yukarısında (Y: 0.5f) oluştur ki yerin içine girmesin
            Vector3 dropPosition = transform.position + new Vector3(0f, 0.5f, 0f);
            GameObject droppedGold = Instantiate(goldPrefab, dropPosition, Quaternion.identity);

            // Düşmanın kendi altın değerini (Örn: 5 veya 15) pickup scriptine aktar
            droppedGold.GetComponent<GoldPickup>().goldAmount = goldReward;
        }

        Destroy(gameObject, 2f);
    }

    public void ApplyPoison()
    {
        // Eğer zaten zehirliyse mevcut işlemi durdur ve tazele
        StopCoroutine(nameof(PoisonRoutine));
        StartCoroutine(nameof(PoisonRoutine));
    }

    private IEnumerator PoisonRoutine()
    {
        int tickCount = 5;

        // --- MATERYAL DEĞİŞTİR: ZEHRİ GÖSTER ---
        if (enemyRenderer != null && poisonMaterial != null)
        {
            enemyRenderer.material = poisonMaterial;
        }

        while (tickCount > 0)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(2f); // Saniye başı 2 zehir hasarı
            tickCount--;
        }

        // --- MATERYAL DEĞİŞTİR: ORİJİNAL HALİNE DÖN ---
        if (enemyRenderer != null)
        {
            enemyRenderer.material = originalMaterial;
        }
    }
}