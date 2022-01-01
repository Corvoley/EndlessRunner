using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveGameData
{
    public int LastScore = 0;
    public int HighestScore = 0;
    public int TotalCherriesCollected = 0;
}

public class AudioPreferences
{
    public float MasterVolume = 1;
    public float MusicVolume = 1;
    public float SFXVolume = 1;
}


public class GameSaver : MonoBehaviour
{

    private string SaveGameFilePath => $"{Application.persistentDataPath}/save.json";
    private string AudioPreferenceFilePath => $"{Application.persistentDataPath}/preferences.json";

    private const string LastScoreKey = "LastScore";
    private const string HighestScoreKey = "HighestScore";
    private const string TotalCherriesCollectedKey = "CherriesCollected";

    private const string MainVolumeKey = "MainVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    public SaveGameData CurrentSave { get; private set; }
    public AudioPreferences AudioPreferences { get; private set; }

    private bool IsLoaded => CurrentSave != null && AudioPreferences != null;
    private void SaveGameDataToFile(string filePath, SaveGameData data)
    {
        
        using(FileStream stream = new FileStream(filePath,FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(stream))
        using (JsonWriter jsonWriter = new JsonTextWriter(writer))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, data);
        }
    }
    private void SaveAudioPreferencesToFile(string filePath, AudioPreferences data)
    {

        using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(stream))
        using (JsonWriter jsonWriter = new JsonTextWriter(writer))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, data);
        }
    }

    private SaveGameData LoadGameDataFromFile(string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
        using (StreamReader reader = new StreamReader(stream))
        using (JsonReader jsonReader = new JsonTextReader(reader))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<SaveGameData>(jsonReader);
        }
    }
    private AudioPreferences LoadAudioPreferencesFromFile(string filePath)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read))
        using (StreamReader reader = new StreamReader(stream))
        using (JsonReader jsonReader = new JsonTextReader(reader))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<AudioPreferences>(jsonReader);
        }
    }

    public void SaveGame(SaveGameData saveData)
    {
        CurrentSave = saveData;
        SaveGameDataToFile(SaveGameFilePath, saveData);
    }

    public void LoadGame()
    {
        if (IsLoaded)
        {
            return;
        }

        CurrentSave = LoadGameDataFromFile(SaveGameFilePath) ?? new SaveGameData();


        AudioPreferences = LoadAudioPreferencesFromFile(AudioPreferenceFilePath) ?? new AudioPreferences();
       
    }

    public void SaveAudioPreferences(AudioPreferences preferences)
    {
        AudioPreferences = preferences;
        SaveAudioPreferencesToFile(AudioPreferenceFilePath, preferences);
    }

}