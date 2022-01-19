using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : UIOverlay
{
    [SerializeField] private MainHUD mainHud;
    [SerializeField] private GameSaver gameSaver;
    [SerializeField] private AudioController audioController;

    [Header("UI Elements")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button deleteAllDataButton;
    [SerializeField] private TextMeshProUGUI deleteAllDataButtonText;

    private void OnEnable()
    {
        UpdateUI();
        deleteAllDataButton.interactable = true;
        deleteAllDataButtonText.text = "DELETE DATA";
    }

    private void OnDisable()
    {
        audioController.SaveAudioPreferences();
    }

    private void UpdateUI()
    {
        masterSlider.value = audioController.MasterVolume;
        musicSlider.value = audioController.MusicVolume;
        sfxSlider.value = audioController.SFXVolume;
    }

    public void Close()
    {
        //TODO: Assuming we only open from StartGameOverlay. Need go back functionality
        mainHud.ShowOverlay<StartGameOverlay>();
    }

    public void OnMasterVolumeChange(float value)
    {
        audioController.MasterVolume = value;
    }

    public void OnMusicVolumeChange(float value)
    {
        audioController.MusicVolume = value;
    }

    public void OnSFXVolumeChange(float value)
    {
        audioController.SFXVolume = value;
    } 
    public void DeleteAllData()
    {
        gameSaver.DeleteAllData();
    }
}