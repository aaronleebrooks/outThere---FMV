using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DesktopManager : MonoBehaviour
{
    public static DesktopManager Instance { get; private set; }
    [Header("App Panels")]
    public GameObject fileSearchPanel;
    public GameObject infoPanel;
    public GameObject notesPanel;
    public GameObject subtitlesPanel;
    public GameObject cameraPanel;
    public GameObject settingsPanel;
    public GameObject helpPanel;
    public GameObject startMenuPanel;

    [SerializeField] public Image wallpaperImage;

    private Dictionary<string, GameObject> appPanels;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the dictionary
        appPanels = new Dictionary<string, GameObject>
        {
            { "fileSearch", fileSearchPanel },
            { "info", infoPanel },
            { "notes", notesPanel },
            { "subtitles", subtitlesPanel },
            { "camera", cameraPanel },
            { "settings", settingsPanel },
            { "help", helpPanel }
        };

        // Optionally, ensure all panels are initially hidden
        foreach (var panel in appPanels.Values)
        {
            panel.SetActive(false);
        }
    }

    // Example for the File Search button
    public void OnFileSearchButtonClicked()
    {
        OpenApp("fileSearch");
    }

    public void OnInfoButtonClicked()
    {
        OpenApp("info");
    }

    public void OnNotesButtonClicked()
    {
        OpenApp("notes");
    }

    public void OnSubtitlesButtonClicked()
    {
        OpenApp("subtitles");
    }

    public void OnCameraButtonClicked()
    {
        OpenApp("camera");
    }

    public void OnSettingsButtonClicked()
    {
        OpenApp("settings");
    }

    public void OnHelpButtonClicked()
    {
        OpenApp("help");
    }

    public void OpenApp(string appName)
    {
        if (appPanels.ContainsKey(appName))
        {
            // Bring the requested panel to the front
            GameObject appPanel = appPanels[appName];
            appPanel.SetActive(true);

            BringAppToFront(appName);
        }
        else
        {
            Debug.LogWarning($"App panel '{appName}' not found.");
        }
    }

    public void CloseApp(string appName)
    {
        if (appPanels.ContainsKey(appName))
        {
            appPanels[appName].SetActive(false);
        }
        else
        {
            Debug.LogWarning($"App panel '{appName}' not found.");
        }
    }

    public void BringAppToFront(string appName)
    {
        if (appPanels.ContainsKey(appName))
        {
            appPanels[appName].transform.SetAsLastSibling();
        }
        else
        {
            Debug.LogWarning($"App panel '{appName}' not found.");
        }
    }

    private void SetWallpaperImage(Sprite wallpaperSprite)
    {
        // Set the wallpaper image
        wallpaperImage.sprite = wallpaperSprite;
    }

    public static void SetWallpaper(Sprite wallpaperSprite)
    {
        // Set the wallpaper image
        Instance.SetWallpaperImage(wallpaperSprite);
    }

    public void ToggleStartMenu()
    {
        startMenuPanel.SetActive(!startMenuPanel.activeSelf);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
