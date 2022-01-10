using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI cherriesText;
    [SerializeField] private TextMeshProUGUI peanutText;
    [SerializeField] private TextMeshProUGUI scoreMultiplierText;
    
    [SerializeField] private GameMode gameMode;
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject startGameOverlay;
    [SerializeField] private GameObject settingsOverlay;
    [SerializeField] private UiAudioController audioController;
    

    private void Awake()
    {
        ShowHudOverlay();


    }
    private void LateUpdate()
    {
        scoreText.text = $"Score: {gameMode.Score}";
        distanceText.text = $"{Mathf.RoundToInt(player.TotalDistanceZ)}m";
        cherriesText.text = $"{gameMode.CherriesCount}";
        peanutText.text = $"{gameMode.PeanutCount}";
        scoreMultiplierText.text = $"{gameMode.TemporaryScoreMultiplier}x";
        scoreMultiplierText.gameObject.SetActive(gameMode.TemporaryScoreMultiplier > 1);
    }


    public void PauseGame()
    {
        ShowPauseOverlay();
        gameMode.PauseGame();
    }
    public void ResumeGame()
    {
        gameMode.ResumeGame();
        ShowHudOverlay();
    }

    public void ShowHudOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(true);
        settingsOverlay.SetActive(false);


    }
    public void ShowPauseOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(true);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(false);
    }

    public void ShowStartOverlay()
    {

        startGameOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(false);

    }
    public void ShowSettingsOverlay()
    {
        startGameOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        hudOverlay.SetActive(false);
        settingsOverlay.SetActive(true);

    }
    public IEnumerator PlayStartGameCountdown(int countdownSeconds)
    {
        ShowHudOverlay();
        countdownText.gameObject.SetActive(false);
        if (countdownSeconds == 0)
        {
            yield break;
        }

        float timeToStart = Time.time + countdownSeconds;
        yield return null;
        countdownText.gameObject.SetActive(true);
        int lastRemainingTime = 0;
        bool alreadyEndSound = false;
        while (Time.time <= timeToStart)
        {
            float remainingTime = timeToStart - Time.time;
            int remainingTimeInt = Mathf.FloorToInt(remainingTime);
            countdownText.text = (remainingTimeInt + 1).ToString();

            float percent = remainingTime - remainingTimeInt;
            countdownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, percent);
            if (remainingTimeInt  != lastRemainingTime && remainingTimeInt > 0)
            {
                audioController.PlayCountdownSound();
                lastRemainingTime = remainingTimeInt;
            }
            else if (remainingTimeInt == 0 && !alreadyEndSound)
            {
                audioController.PlayCountdownEndSound();
                alreadyEndSound = true;
            }

            yield return null;
        }

        countdownText.gameObject.SetActive(false);
    }

}
