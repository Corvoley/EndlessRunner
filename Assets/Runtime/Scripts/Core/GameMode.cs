using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameSaver gameSaver;

    [Header("Player")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimationController playerAnimationController;

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


    public SaveGameData CurrentSave => gameSaver.CurrentSave;
    public AudioPreferences AudioPreferences => gameSaver.AudioPreferences;

    public int TemporaryScoreMultiplier
    { get => temporaryScoreMultiplier;
        set => temporaryScoreMultiplier = Mathf.Max(1, value);      
    }
    public int temporaryScoreMultiplier = 1;
    public float baseScoreMultiplier = 1;
    private float score;
    public int Score => Mathf.RoundToInt(score + cherriesTotalScore);

    private int cherriesCount = 0;
    private int peanutCount = 0;
    private float cherriesScoreValue = 100;
    private float cherriesTotalScore = 0;
    private float highestScore = 0;
    private float lastScore = 0;
    private int totalCherriesCount = 0;
    private int totalPeanutCount = 0;
    public int CherriesCount => cherriesCount;
    public int PeanutCount => peanutCount;
    public float HighestScore => Mathf.RoundToInt(highestScore);
    public float LastScore => lastScore;
    public int TotalCherriesCount => totalCherriesCount;
    public int TotalPeanutCount => totalPeanutCount;

    private bool isGameRunning = false;

    private void Awake()
    {
        player.PlayerDeathEvent += OnPlayerDeath;
        gameSaver.LoadGame();
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
            score += ( baseScoreMultiplier * temporaryScoreMultiplier * extraScoreMultiplier * player.ForwardSpeed * Time.deltaTime);
            
        }
    }
    

    private void SetWwaitForStartGameState()
    {
        player.enabled = false;
        mainHUD.ShowStartOverlay();
        musicPlayer.PlayStartMenuMusic();
    }

    private void OnPlayerDeath()
    {
        OnGameOver();
    }
    public void OnGameOver()
    {
        isGameRunning = false;
        player.ForwardSpeed = 0;
        gameSaver.SaveGame(new SaveGameData
        {
            HighestScore = Score > gameSaver.CurrentSave.HighestScore ? Score : gameSaver.CurrentSave.HighestScore,
            LastScore = Score,
            TotalCherriesCollected = gameSaver.CurrentSave.TotalCherriesCollected + cherriesCount,
            TotalPeanutCollected = gameSaver.CurrentSave.TotalPeanutCollected + peanutCount
        });
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

    public void IncreasePeanutCount()
    {
        peanutCount++;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnDestroy()
    {
        player.PlayerDeathEvent -= OnPlayerDeath;
    }

}
