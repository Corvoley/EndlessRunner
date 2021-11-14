using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    
    [Header("Player")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private Animator playerAnimator;

    [Header("UI")]
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private MusicPlayer musicPlayer;
    [Header("Gameplay")]
    [SerializeField] private float reloadGameDelay = 3;
    [SerializeField] private int startGameCountdown = 3;

    [SerializeField] private float startPlayerSpeed = 10;
    [SerializeField] private float maxPlayerSpeed = 20;
    [SerializeField] private float timeToMaxSpeedSeconds = 300;
    private float startGameTime;


   [SerializeField] private float baseScoreMultiplier = 1;
    private float score;
    public int Score => Mathf.RoundToInt(score + cherriesTotalScore);

    private int cherriesCount = 0;
    private float cherriesScoreValue = 100;
    private float cherriesTotalScore = 0;
    public int CherriesCount => cherriesCount;
    private bool isGameRunning = false;

    private void Awake()
    {        
        SetWwaitForStartGameState();
    }
    private void Update()
    {
        DifficultScale();        
    }

    private void DifficultScale()
    {                 

        if (isGameRunning)
        {
            float timePercent = (Time.time - startGameTime) / timeToMaxSpeedSeconds;
             player.ForwardSpeed = Mathf.Lerp(startPlayerSpeed, maxPlayerSpeed, timePercent);

            float extraScoreMultiplier = 1 + timePercent;
            cherriesTotalScore = (CherriesCount * cherriesScoreValue);
            score += (baseScoreMultiplier * extraScoreMultiplier * player.ForwardSpeed * Time.deltaTime);
            
        }
    }

    private void SetWwaitForStartGameState()
    {
        player.enabled = false;
        mainHUD.ShowStartOverlay();
        musicPlayer.PlayStartMenuMusic();
    }

    public void OnGameOver()
    {
        isGameRunning = false;
        player.ForwardSpeed = 0;
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(1);
        musicPlayer.PlayDeathTrackMusic();
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }

    private IEnumerator StartGameCor()
    {
        musicPlayer.PlayMainTrackMusic();
        yield return StartCoroutine(mainHUD.PlayStartGameCountdown(startGameCountdown));
        yield return StartCoroutine(playerAnimationController.PlayStartGameAnimation());
        player.enabled = true;
        player.ForwardSpeed = startPlayerSpeed;
        startGameTime = Time.time;
        isGameRunning = true;
    }

    public void IncreaseCherriesCount()
    {
        cherriesCount++;
    }

    
}
