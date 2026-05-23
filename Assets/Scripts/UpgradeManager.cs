using UnityEngine;
using UnityEngine.UI; // Butonları koddan yönetmek için gerekli
using TMPro;
using System.Collections;

public class UpgradeManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject upgradePanel;

    [Header("UI Buttons (Atamalar Koddan Yapılacak)")]
    public Button openPanelButton;
    public Button closePanelButton;
    public Button damageButton;
    public Button hpButton;
    public Button attackSpeedButton;
    public Button moveSpeedButton;

    [Header("UI Texts (Buton İçerikleri)")]
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI moveSpeedText;

    [Header("Upgrade Base Costs")]
    public int baseCost = 50;

    private int damageLevel = 1;
    private int hpLevel = 1;
    private int attackSpeedLevel = 1;
    private int moveSpeedLevel = 1;

    private PlayerStats playerStats;

    void Awake()
    {
        // Buton tıklama olaylarını (Events) kod üzerinden fonksiyonlara bağlıyoruz
        if (openPanelButton != null) openPanelButton.onClick.AddListener(OpenUpgradePanel);
        if (closePanelButton != null) closePanelButton.onClick.AddListener(CloseUpgradePanel);

        if (damageButton != null) damageButton.onClick.AddListener(BuyDamageUpgrade);
        if (hpButton != null) hpButton.onClick.AddListener(BuyHPUpgrade);
        if (attackSpeedButton != null) attackSpeedButton.onClick.AddListener(BuyAttackSpeedUpgrade);
        if (moveSpeedButton != null) moveSpeedButton.onClick.AddListener(BuyMoveSpeedUpgrade);
    }

    void Start()
    {
        upgradePanel.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerStats = player.GetComponent<PlayerStats>();

        UpdateAllUI();
    }

    // Obje yok edildiğinde (sahne değişimi vs.) bellek sızıntısını önlemek için dinleyicileri kaldırıyoruz
    void OnDestroy()
    {
        if (openPanelButton != null) openPanelButton.onClick.RemoveAllListeners();
        if (closePanelButton != null) closePanelButton.onClick.RemoveAllListeners();
        if (damageButton != null) damageButton.onClick.RemoveAllListeners();
        if (hpButton != null) hpButton.onClick.RemoveAllListeners();
        if (attackSpeedButton != null) attackSpeedButton.onClick.RemoveAllListeners();
        if (moveSpeedButton != null) moveSpeedButton.onClick.RemoveAllListeners();
    }

    // --- ZAMAN VE PANEL YÖNETİMİ ---

    private void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
        openPanelButton.gameObject.SetActive(false); // Buton objesini gizle
        Time.timeScale = 0f;
    }

    private void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
        StartCoroutine(ResumeGameAfterDelay());
    }

    private IEnumerator ResumeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        openPanelButton.gameObject.SetActive(true);
    }

    // --- UPGRADE SATIN ALMA İŞLEMLERİ ---

    private void BuyDamageUpgrade()
    {
        int cost = baseCost * damageLevel;
        if (CurrencyManager.Instance.SpendGold(cost))
        {
            playerStats.UpgradeAttackDamage(5f);
            damageLevel++;
            UpdateAllUI();
        }
    }

    private void BuyHPUpgrade()
    {
        int cost = baseCost * hpLevel;
        if (CurrencyManager.Instance.SpendGold(cost))
        {
            playerStats.UpgradeHealth(20f);
            hpLevel++;
            UpdateAllUI();
        }
    }

    private void BuyAttackSpeedUpgrade()
    {
        int cost = baseCost * attackSpeedLevel;
        if (CurrencyManager.Instance.SpendGold(cost))
        {
            playerStats.UpgradeAttackSpeed(0.5f);
            attackSpeedLevel++;
            UpdateAllUI();
        }
    }

    private void BuyMoveSpeedUpgrade()
    {
        int cost = baseCost * moveSpeedLevel;
        if (CurrencyManager.Instance.SpendGold(cost))
        {
            playerStats.UpgradeMoveSpeed(0.5f);
            moveSpeedLevel++;
            UpdateAllUI();
        }
    }

    private void UpdateAllUI()
    {
        if (damageText != null) damageText.text = $"DAMAGE LVL {damageLevel}\nCost: {baseCost * damageLevel}";
        if (hpText != null) hpText.text = $"HP LVL {hpLevel}\nCost: {baseCost * hpLevel}";
        if (attackSpeedText != null) attackSpeedText.text = $"ATK SPD LVL {attackSpeedLevel}\nCost: {baseCost * attackSpeedLevel}";
        if (moveSpeedText != null) moveSpeedText.text = $"MOVE SPD LVL {moveSpeedLevel}\nCost: {baseCost * moveSpeedLevel}";
    }
}