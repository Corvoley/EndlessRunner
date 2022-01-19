using TMPro;
using UnityEngine;

public class StartGameOverlay : UIOverlay
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private MainHUD mainHud;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI totalCherriesText;
    [SerializeField] private TextMeshProUGUI totalPeanutsText;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        highestScoreText.text = $"Highest Score\n{gameMode.CurrentSave.HighestScore}";
        lastScoreText.text = $"Last Score\n{gameMode.CurrentSave.LastScore}";
        totalCherriesText.text = $"{gameMode.CurrentSave.TotalCherriesCollected}";
        totalPeanutsText.text = $"{gameMode.CurrentSave.TotalPeanutCollected}";
    }
    public void ShowSettings()
    {
        mainHud.ShowOverlay<SettingsWindow>();
    }
}
