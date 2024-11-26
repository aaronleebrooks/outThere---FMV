using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private SaveData saveData;
    private string savePath;
    [SerializeField] private Button continueButton; // Reference to the Continue button

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the GameManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }

        savePath = Application.persistentDataPath + "/savefile.json";
        CheckForSaveData(); // Disable/Enable Continue button based on save existence
    }

    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("MainGameScene");
    }

    public void ShowStartMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("StartMenuScene");
    }

    public void QuitGame()
    {
        // Handle quitting the application
        Application.Quit();
    }

    // Other global functions like pausing, saving, loading, etc.
    public void StartNewGame()
    {
        saveData = new SaveData(); // Create a new empty save
        SaveGame(); // Immediately save the new game
        StartGame(); // Start the game
    }

    public void VideoWatched(int videoID)
    {
        if (!saveData.watchedVideoIDs.Contains(videoID))
        {
            saveData.watchedVideoIDs.Add(videoID);
            SaveGame(); // Save the game whenever a new video is watched
        }
    }

    private void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved.");
    }

    private void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game Loaded.");
        }
        else
        {
            StartNewGame(); // Start a new game if no save file exists
        }
    }

    private void CheckForSaveData()
    {
        if (File.Exists(savePath))
        {
            Debug.Log("Save file found.");
            continueButton.interactable = true; // Enable Continue button
        }
        else
        {
            Debug.Log("No save file found.");
            continueButton.interactable = false; // Disable Continue button
        }
    }
}
