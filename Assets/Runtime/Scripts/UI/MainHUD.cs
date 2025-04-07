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
    [SerializeField] private UiAudioController audioController;
    [SerializeField] private UIOverlay[] overlays;
    

    private void Awake()
    {
        ShowOverlay<HUDOverlay>();
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
        ShowOverlay<PauseOverlay>();
        gameMode.PauseGame();
    }
    public void ResumeGame()
    {
        gameMode.ResumeGame();
        ShowOverlay<HUDOverlay>();
    }

    public void RetryGame()
    {
        ShowOverlay<RetryOverlay>();
    }

    public void ShowOverlay<T>() where T : UIOverlay
    {
        foreach (UIOverlay overlay in overlays)
        {
            bool isTypeT = overlay is T;
            overlay.gameObject.SetActive(isTypeT);
        }
    }
    
    public IEnumerator PlayStartGameCountdown(int countdownSeconds)
    {
        ShowOverlay<HUDOverlay>();
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
