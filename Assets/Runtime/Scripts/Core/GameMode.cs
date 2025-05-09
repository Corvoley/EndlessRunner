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
    private bool canRetry = true;


    public SaveGameData CurrentSave => gameSaver.CurrentSave;
    public AudioPreferences AudioPreferences => gameSaver.AudioPreferences;

    public int TemporaryScoreMultiplier
    {
        get => temporaryScoreMultiplier;
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
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        player.PlayerDeathEvent += OnPlayerDeath;
        gameSaver.LoadGame();
        SetWaitForStartGameState();
    }

    private void Start()
    {
        AdsManager.Instance.OnIntertistialAdClosed += RetryGame;
    }
    private void Update()
    {
        DifficultScale();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGameRunning)
            {
               
                if (Time.timeScale == 0)
                {
                    ExitGame();
                }
                else
                {
                    mainHUD.PauseGame();
                    PauseGame();
                }
            }
            else
            {
                ExitGame();
            }


        }
    }

    private void DifficultScale()
    {

        if (isGameRunning)
        {
            float timePercent = (Time.time - startGameTime) / timeToMaxSpeedSeconds;
            player.ForwardSpeed = Mathf.Lerp(startPlayerSpeed, maxPlayerSpeed, timePercent);

            float extraScoreMultiplier = 1 + timePercent;
            cherriesTotalScore = (CherriesCount * cherriesScoreValue);
            score += (baseScoreMultiplier * temporaryScoreMultiplier * extraScoreMultiplier * player.ForwardSpeed * Time.deltaTime);

        }
    }


    private void SetWaitForStartGameState()
    {
        player.enabled = false;
        mainHUD.ShowOverlay<StartGameOverlay>();
        musicPlayer.PlayStartMenuMusic();
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(EndGameCor());
        if (canRetry)
        {
            mainHUD.RetryGame();

        }
        else
        {
            GameOver();
        }
    }
    public void RetryGame()
    {
        canRetry = false;
        StartCoroutine(RetryGameCor());

    }
    private IEnumerator RetryGameCor()
    {
        playerAnimationController.PlayIdleAnimation();
        musicPlayer.PlayMainTrackMusic();
        yield return StartCoroutine(mainHUD.PlayStartGameCountdown(startGameCountdown));
        yield return StartCoroutine(playerAnimationController.PlayStartGameAnimation());
        player.enabled = true;
        player.ForwardSpeed = startPlayerSpeed;
        isGameRunning = true;
    }
    public void GameOver()
    {
        gameSaver.SaveGame(new SaveGameData
        {
            HighestScore = Score > gameSaver.CurrentSave.HighestScore ? Score : gameSaver.CurrentSave.HighestScore,
            LastScore = Score,
            TotalCherriesCollected = gameSaver.CurrentSave.TotalCherriesCollected + cherriesCount,
            TotalPeanutCollected = gameSaver.CurrentSave.TotalPeanutCollected + peanutCount
        });
        StartCoroutine(ReloadGameCoroutine());
    }
    private IEnumerator EndGameCor()
    {
        isGameRunning = false;
        player.ForwardSpeed = 0;
        player.enabled = false;
        yield return new WaitForSeconds(0.5f);
        musicPlayer.PlayDeathTrackMusic();
    }
    private IEnumerator ReloadGameCoroutine()
    {
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
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
    private void OnDestroy()
    {
        player.PlayerDeathEvent -= OnPlayerDeath;
    }

}
