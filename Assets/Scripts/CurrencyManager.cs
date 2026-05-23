using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    // Singleton erişimi: Herhangi bir scriptten CurrencyManager.Instance yazarak ulaşabiliriz.
    public static CurrencyManager Instance { get; private set; }

    public int CurrentGold { get; private set; }

    // Altın miktarı değiştiğinde UI'ı haberdar etmek için Event
    public event Action<int> OnGoldChanged;

    void Awake()
    {
        // Singleton Kurulumu
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        CurrentGold += amount;
        OnGoldChanged?.Invoke(CurrentGold); // UI'a "altın değişti, kendini güncelle" diyoruz
    }

    public bool SpendGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            OnGoldChanged?.Invoke(CurrentGold);
            return true; // Satın alma başarılı
        }
        return false; // Yeterli altın yok
    }
}