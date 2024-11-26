using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class SearchResultEvent : UnityEvent<SearchResult> { }

public class SearchResult : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Sprite selectedImage;
    [SerializeField] private Sprite unselectedImage;

    [SerializeField] private Image playIcon;
    [SerializeField] private Sprite selectedIcon;
    [SerializeField] private Sprite unselectedIcon;

    private VideoDataSO videoData;

    // UnityEvent to notify when a video is selected
    public SearchResultEvent onVideoSelected;
    public SearchResultEvent onVideoDeselected;

    private void Awake()
    {
        if (onVideoSelected == null)
        {
            onVideoSelected = new SearchResultEvent();
        }
    }

    public void Setup(VideoDataSO data)
    {
        videoData = data;
        DoUnselect(); // Initialize as unselected
    }

    public void DoClick()
    {
        onVideoSelected.Invoke(this);
    }

    public void DoSelect()
    {
        background.sprite = selectedImage;
        playIcon.sprite = selectedIcon;
    }


    public void DoUnselect()
    {
        background.sprite = unselectedImage;
        playIcon.sprite = unselectedIcon;
    }

    public VideoDataSO GetVideoData()
    {
        return videoData;
    }
}
