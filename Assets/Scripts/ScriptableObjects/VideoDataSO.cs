using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Video;
using System;

[CreateAssetMenu(fileName = "NewVideoDataSO", menuName = "Video Data SO", order = 1)]
public class VideoDataSO : ScriptableObject
{
    [SerializeField]
    private string id;
    public string Id => id; // Read-only property to access the ID

    public string title;
    public string description;
    public List<string> tags;
    public string date;
    public string length;
    public string notes;
    public List<VideoComment> comments;
    public VideoClip videoClip;
    public Sprite thumbnail;
    public bool isWatched;

    public void MarkAsWatched()
    {
        isWatched = true;
    }

    // This method is called when the script is loaded or a value is changed in the inspector
    private void OnValidate()
    {
        // Generate a new ID only if it is empty or null
        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
        }
    }
}
