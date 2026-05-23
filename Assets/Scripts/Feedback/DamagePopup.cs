using UnityEngine;
using TMPro; // TextMeshPro için gerekli

public class DamagePopup : MonoBehaviour
{
    public TextMeshPro textMesh; // 3D TextMeshPro bileşeni
    public float moveYSpeed = 2f; // Yukarı uçma hızı
    public float fadeSpeed = 3f; // Şeffaflaşma hızı

    private Color textColor;
    private float disappearTimer = 0.3f; // Kaç saniye sonra şeffaflaşmaya başlasın?

    public void Setup(float damageAmount)
    {
        // Hasarı tam sayı olarak yazdır ("0" formatı küsuratları gizler)
        textMesh.SetText(damageAmount.ToString("0"));
        textColor = textMesh.color;

        // Ekranda sonsuza kadar kalmaması için her ihtimale karşı 2 saniye sonra yok et
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        // Her karede Y ekseninde yukarı doğru hareket et
        transform.position += Vector3.up * moveYSpeed * Time.deltaTime;

        // Sayacı azalt
        disappearTimer -= Time.deltaTime;

        // Süre dolduğunda şeffaflaşmaya (Fade) başla
        if (disappearTimer < 0)
        {
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;
        }
    }
}