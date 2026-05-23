using UnityEngine;
using TMPro;
using System.Collections;

public class SkillPopupUI : MonoBehaviour
{
    public static SkillPopupUI Instance { get; private set; }

    [Header("UI Components")]
    public GameObject popupContainer;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    [Header("Settings")]
    public float displayDuration = 3f;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        Instance = this;
        canvasGroup = popupContainer.GetComponent<CanvasGroup>();
        popupContainer.SetActive(false);
    }

    public void ShowPopup(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;

        StopAllCoroutines(); // Eğer üst üste binerse eskisini durdur
        StartCoroutine(PopupRoutine());
    }

    private IEnumerator PopupRoutine()
    {
        popupContainer.SetActive(true);

        // Fade In (Şeffaflıktan görünürlüğe)
        float timer = 0f;
        while (timer < 0.5f)
        {
            timer += Time.unscaledDeltaTime; // Oyun durmuş olsa bile çalışsın
            canvasGroup.alpha = timer / 0.5f;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(displayDuration);

        // Fade Out (Görünürlükten şeffaflığa)
        while (timer > 0f)
        {
            timer -= Time.unscaledDeltaTime;
            canvasGroup.alpha = timer / 0.5f;
            yield return null;
        }

        popupContainer.SetActive(false);
    }
}