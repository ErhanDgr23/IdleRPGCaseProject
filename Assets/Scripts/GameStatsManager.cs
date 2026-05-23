using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için
using UnityEngine.UI;
using System.Collections;

public class GameStatsManager : MonoBehaviour
{
    public static GameStatsManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject losePanel;
    public Button restartButton;

    void Awake()
    {
        Instance = this;
        if (losePanel != null) losePanel.SetActive(false);
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerHealth>().OnPlayerDeath += HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        StartCoroutine(ShowLosePanelAfterDelay());
    }

    private IEnumerator ShowLosePanelAfterDelay()
    {
        // Karakterin aşağı düşüşünü izlemek için 2 saniye bekle
        yield return new WaitForSeconds(2f);

        if (losePanel != null)
        {
            losePanel.SetActive(true);
            // Fare imlecini görünür yap (Eğer gizliyse)
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void RestartGame()
    {
        // Zamanı normale döndür (Upgrade paneli açıkken ölme ihtimaline karşı)
        Time.timeScale = 1f;
        // Mevcut sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}