using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("Quad References")]
    [Tooltip("Sadece görselleri tutan alt obje. (Görünmez yaparken bunu kapatacağız)")]
    public GameObject visualsContainer;

    [Tooltip("İçinde Fill Quad'ı bulunan ve sol kenarda duran boş obje.")]
    public Transform fillPivot;

    [Header("Auto Hide Settings (Player İçin)")]
    public bool autoHide = false;
    public float showDuration = 3f;
    private float hideTimer = 0f;

    private Vector3 initialScale;

    // Start yerine Awake kullanıyoruz. Awake, oyundaki tüm Start'lardan ÖNCE çalışır.
    void Awake()
    {
        if (fillPivot != null)
        {
            // Barın orijinal boyutunu en baştan güvene alıyoruz
            initialScale = fillPivot.localScale;
        }
    }

    void Start()
    {
        // Eğer gizlenme açıksa, oyun başladığında can barını gizle
        if (autoHide && visualsContainer != null)
        {
            visualsContainer.SetActive(false);
        }
    }

    void Update()
    {
        // Eğer bar görünürse ve otomatik gizlenme açıksa, sayacı geriye doğru say
        if (autoHide && visualsContainer != null && visualsContainer.activeSelf)
        {
            hideTimer -= Time.deltaTime;

            if (hideTimer <= 0f)
            {
                visualsContainer.SetActive(false); // Süre bitince gizle
            }
        }
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (fillPivot != null)
        {
            // Canı 0 ile 1 arasında sınırla ki eksi değerlere düşüp barı ters yönde büyütmesin
            float healthPercentage = Mathf.Clamp01(currentHealth / maxHealth);

            // X ölçeğini yüzdelik can değerine göre çarparak küçült
            fillPivot.localScale = new Vector3(initialScale.x * healthPercentage, initialScale.y, initialScale.z);
        }

        // Hasar alındığında barı tekrar görünür yap ve sayacı sıfırla
        if (autoHide && visualsContainer != null)
        {
            visualsContainer.SetActive(true);
            hideTimer = showDuration;
        }
    }
}