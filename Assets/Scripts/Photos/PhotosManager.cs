using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotosManager : MonoBehaviour
{
    public Transform photosGrid;  // Parent object for thumbnails
    public GameObject photoThumbnailPrefab;  // Thumbnail prefab to represent each photo
    public Image fullPhotoDisplay;  // Full view for selected photo
    public GameObject fullPhotoPanel;  // Panel to display the full image
    private string screenshotDirectory;

    private Sprite selectedSprite;
    private string selectedFilePath;

    private void Start()
    {
        screenshotDirectory = Path.Combine(Application.persistentDataPath, "Screenshots");

        // Ensure the screenshot directory exists
        if (!Directory.Exists(screenshotDirectory))
        {
            Directory.CreateDirectory(screenshotDirectory);
        }

        LoadAllPhotos();
    }

    // Load all photos in the directory
    public void LoadAllPhotos()
    {
        // Clear any existing thumbnails
        foreach (Transform child in photosGrid)
        {
            Destroy(child.gameObject);
        }

        // Load each screenshot as a thumbnail
        string[] files = Directory.GetFiles(screenshotDirectory, "*.png");
        foreach (string file in files)
        {
            GameObject thumbnail = Instantiate(photoThumbnailPrefab, photosGrid);
            Image thumbnailImage = thumbnail.GetComponent<Image>();
            Button thumbnailButton = thumbnail.GetComponentInChildren<Button>();

            // Load the image data
            byte[] fileData = File.ReadAllBytes(file);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);

            // Convert the Texture2D to a Sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Set the Image component to use this sprite
            thumbnailImage.sprite = sprite;

            ViewPhoto(file);  // View the first photo by default

            // Set the button click event to view the full photo
            thumbnailButton.onClick.AddListener(() => ViewPhoto(file));
        }
    }

    // View a full-size photo
    public void ViewPhoto(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Convert the Texture2D to a Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // Set the Image component to use this sprite
        fullPhotoDisplay.sprite = sprite;
        fullPhotoPanel.SetActive(true);

        selectedSprite = sprite;
        selectedFilePath = filePath;
    }

    // Set photo as wallpaper
    public void SetAsWallpaper()
    {
        // Save the selected photo as wallpaper
        DesktopManager.SetWallpaper(selectedSprite);
    }

    // Delete a photo
    public void DeletePhoto()
    {
        if (File.Exists(selectedFilePath))
        {
            File.Delete(selectedFilePath);
            LoadAllPhotos();  // Reload the photos after deletion
            fullPhotoPanel.SetActive(false);  // Close the full photo view
        }
    }

    // Close the full photo view
    public void CloseFullPhotoView()
    {
        fullPhotoPanel.SetActive(false);
    }

    public void OnPhotoThumbnailClicked(string filePath)
    {
        ViewPhoto(filePath);
    }
}