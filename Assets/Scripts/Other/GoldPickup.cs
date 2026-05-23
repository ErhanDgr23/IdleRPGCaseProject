using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GoldPickup : MonoBehaviour
{
    [HideInInspector]
    public int goldAmount;

    [Header("Magnet Settings")]
    public float magnetSpeed = 15f;
    [Tooltip("Mıknatıs tetikleyici alanının büyüklüğü")]
    public float magnetRadius = 4f;
    [Tooltip("Oyuncuya ne kadar yaklaşınca toplansın?")]
    public float collectDistance = 0.5f;
    public float dropDelay = 0.5f;

    private Transform player;
    private bool isMagnetized = false;
    private float delayTimer = 0f;
    private SphereCollider magnetCollider;

    void Start()
    {
        // Collider ayarlarını koddan otomatik yapıyoruz
        magnetCollider = GetComponent<SphereCollider>();
        magnetCollider.isTrigger = true;
        magnetCollider.radius = magnetRadius; // Mıknatıs menzilini Trigger yarıçapı yapıyoruz
    }

    void Update()
    {
        // 1. Bekleme süresi dolmadan bekle
        if (delayTimer < dropDelay)
        {
            delayTimer += Time.deltaTime;
            return;
        }

        // 2. Mıknatıslandıysa ve oyuncu biliniyorsa ona uç
        if (isMagnetized && player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);

            // Altın oyuncuya yeterince yaklaştığında topla
            if (Vector3.Distance(transform.position, player.position) <= collectDistance)
            {
                CollectGold();
            }
        }
    }

    // Fiziksel temas (Oyuncu mıknatıs alanına girdiğinde)
    private void OnTriggerEnter(Collider other)
    {
        if (!isMagnetized && other.CompareTag("Player"))
        {
            // GameObject.Find yerine, bize çarpan objenin (Player) referansını alıyoruz!
            player = other.transform;
            isMagnetized = true;
        }
    }

    // Eğer altın yere düştüğünde oyuncu ZATEN mıknatıs alanının içindeyse
    // OnTriggerEnter çalışmayabilir, bu yüzden güvenliğe almak için Stay kullanıyoruz
    private void OnTriggerStay(Collider other)
    {
        if (!isMagnetized && delayTimer >= dropDelay && other.CompareTag("Player"))
        {
            player = other.transform;
            isMagnetized = true;
        }
    }

    private void CollectGold()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddGold(goldAmount);
        }

        Destroy(gameObject);
    }
}