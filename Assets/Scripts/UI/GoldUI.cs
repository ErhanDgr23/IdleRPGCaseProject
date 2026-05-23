using UnityEngine;
using TMPro; // TextMeshPro kütüphanesi

public class GoldUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    void Start()
    {
        if (CurrencyManager.Instance != null)
        {
            // Event'e abone ol (Altın değiştiğinde UpdateGoldText otomatik çalışacak)
            CurrencyManager.Instance.OnGoldChanged += UpdateGoldText;

            // Başlangıçta yazıyı 0 olarak güncelle
            UpdateGoldText(CurrencyManager.Instance.CurrentGold);
        }
    }

    void OnDestroy()
    {
        // UI kapanır veya yok olursa memory leak olmaması için abonelikten çık
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.OnGoldChanged -= UpdateGoldText;
        }
    }

    private void UpdateGoldText(int newAmount) => goldText.text = "Current Gold: " + newAmount.ToString();
}