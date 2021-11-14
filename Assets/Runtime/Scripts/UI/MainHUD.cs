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
    [SerializeField] private GameMode gameMode;
    [SerializeField] private GameObject resumeHUD;
    [SerializeField] private GameObject pauseHUD;
    [SerializeField] private GameObject startHUD;
    [SerializeField] private UiAudioController audioController;
    

    private void Awake()
    { 
        ShowStartOverlay();
    }
    private void LateUpdate()
    {
        scoreText.text = $"Score: {gameMode.Score}";
        distanceText.text = $"{Mathf.RoundToInt(player.TotalDistanceZ)}m";
        cherriesText.text = $"{gameMode.CherriesCount}";
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
        startHUD.SetActive(false);
        resumeHUD.SetActive(true);
        pauseHUD.SetActive(false);
        
    }
    public void ShowPauseOverlay()
    {
        resumeHUD.SetActive(false);
        pauseHUD.SetActive(true);
    }

    public void ShowStartOverlay()
    {
        startHUD.SetActive(true);

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
