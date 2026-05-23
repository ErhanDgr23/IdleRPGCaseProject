using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerCombat : MonoBehaviour
{
    private PlayerStats stats;
    private GlobalAnimator charAnimator;
    private float attackCooldownTimer = 0f;

    // Hareket scriptinin (PlayerMovement) bu hedefe ulaşıp dönüşü durdurabilmesi için public yapıyoruz
    public Transform CurrentTarget { get; private set; }

    [Header("References")]
    public GameObject arrowPrefab;
    public Transform firePoint;
    public LayerMask enemyLayer;

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        charAnimator = GetComponent<GlobalAnimator>();
    }

    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        // Her karede en yakın düşmanı bul
        FindNearestEnemy();

        if (CurrentTarget != null)
        {
            // Yakında düşman var, Animator Body Layer ağırlığını 1'e doğru artır
            charAnimator.SetCombatState(true);

            // Hedefe doğru pürüzsüz dön (Y eksenini kilitleyerek)
            Vector3 directionToTarget = CurrentTarget.position - transform.position;
            directionToTarget.y = 0f;

            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
            }

            // Süre dolduysa ateş et
            if (attackCooldownTimer <= 0f)
            {
                Shoot(CurrentTarget);
                attackCooldownTimer = 1f / stats.attackSpeed;
            }
        }
        else
        {
            // Yakında düşman yok, Body Layer ağırlığını 0'a doğru azalt
            charAnimator.SetCombatState(false);
        }
    }

    private void FindNearestEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, stats.attackRange, enemyLayer);
        CurrentTarget = null; // Önce hedefi sıfırla
        float minDistance = Mathf.Infinity;

        foreach (Collider enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                CurrentTarget = enemy.transform;
            }
        }
    }

    private void Shoot(Transform target)
    {
        charAnimator.TriggerAttack();

        if (SkillManager.Instance.IsMultiShotActive)
        {
            // MULTISHOT: 3 Ok fırlat (Merkez, Sol, Sağ)
            SpawnArrow(target, 0f);    // Merkez
            SpawnArrow(target, -15f);  // 15 derece sol
            SpawnArrow(target, 15f);   // 15 derece sağ
        }
        else
        {
            // Normal tekli atış
            SpawnArrow(target, 0f);
        }
    }

    private void SpawnArrow(Transform target, float angleOffset)
    {
        Vector3 directionToTarget = (target.position - firePoint.position).normalized;
        directionToTarget.y = 0f;

        // Yönü açısal olarak saptır (Quaternion çarpımı yönü döndürür)
        Quaternion rotationOffset = Quaternion.Euler(0, angleOffset, 0);
        Vector3 finalDirection = rotationOffset * directionToTarget;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.LookRotation(finalDirection));

        ProjectileArrow arrowScript = arrow.GetComponent<ProjectileArrow>();

        // POISON: Eğer zehir yeteneği açıksa okun hasar türünü zehirli yap
        bool isPoisoned = SkillManager.Instance.IsPoisonActive;
        arrowScript.Initialize(finalDirection, stats.attackDamage, isPoisoned);
    }
}