using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileArrow : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 15f;
    public float lifetime = 3f; // 3 saniye hiçbir şeye çarpmazsa yok olsun

    // Çarpışma efektleri için (opsiyonel, daha sonra eklenebilir)
    // public GameObject hitVFX; 

    private float damage;
    private Rigidbody rb;

    private bool isPoisonous;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Oku PlayerCombat içinden oluşturduğumuzda bu fonksiyonu çağırıp ona yön ve hasar bilgisini vereceğiz
    public void Initialize(Vector3 direction, float damageAmount, bool poison)
    {
        damage = damageAmount;
        isPoisonous = poison;
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifetime);
    }

    // Ok bir şeye çarptığında (Tetiklendiğinde) çalışır
    private void OnTriggerEnter(Collider other)
    {
        // Çarptığımız obje "Enemy" etiketine (Tag) sahip mi?
        if (other.CompareTag("Enemy"))
        {
            BaseEnemy enemy = other.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                // Eğer zehirliyse düşmana zehir etkisi başlat
                if (isPoisonous) enemy.ApplyPoison();
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Environment"))
        {
            // Duvara veya engele çarparsa da yok olsun
            Destroy(gameObject);
        }
    }
}