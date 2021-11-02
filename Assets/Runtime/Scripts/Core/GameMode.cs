using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private float reloadGameDelay = 3;
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private int startGameCountdown = 5;
    public void OnGameOver()
    {
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        //esperar uma frame
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
        yield return StartCoroutine(mainHUD.PlayStartGameCountdown(startGameCountdown));
        playerAnimationController.PlayGameAnimationStart();
    }
}
