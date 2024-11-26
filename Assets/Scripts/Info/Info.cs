using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    public static Info Instance { get; private set; }

    [Header("Text Fields")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text lengthText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_InputField tagsInputField;
    [SerializeField] private TMP_InputField notesInputField;
    [SerializeField] private Transform commentsParent;
    [SerializeField] private GameObject commentPrefab;

    [Header("Saving Animation")]
    [SerializeField] private GameObject savingAnimation;

    private VideoDataSO currentVideoData;

    private void Awake()
    {
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
        // Hide the saving animation initially
        savingAnimation.SetActive(false);
    }

    private void OnDestroy()
    {
        // Remove listeners to avoid memory leaks
        tagsInputField.onValueChanged.RemoveListener(OnTagsChanged);
        notesInputField.onValueChanged.RemoveListener(OnNotesChanged);
    }

    public void SetVideoData(VideoDataSO videoData)
    {
        currentVideoData = videoData;

        titleText.text = videoData.title;
        lengthText.text = videoData.length;
        descriptionText.text = videoData.description;
        tagsInputField.text = string.Join(", ", videoData.tags);
        notesInputField.text = videoData.notes;

        // Clear existing comments
        foreach (Transform child in commentsParent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate comment prefabs
        foreach (var comment in videoData.comments)
        {
            GameObject commentObject = Instantiate(commentPrefab, commentsParent);
            commentObject.GetComponent<VideoCommentUI>().Setup(comment);
        }
    }

    public void OnTagsChanged(string newTags)
    {
        if (currentVideoData != null)
        {
            currentVideoData.tags = new List<string>(newTags.Split(','));
            Debug.Log("Tags changed: " + newTags);
            StartCoroutine(PlaySavingAnimation());
        }
    }

    public void OnNotesChanged(string newNotes)
    {
        if (currentVideoData != null)
        {
            currentVideoData.notes = newNotes;
            Debug.Log("Notes changed: " + newNotes);
            StartCoroutine(PlaySavingAnimation());
        }
    }

    private IEnumerator PlaySavingAnimation()
    {
        savingAnimation.SetActive(true);
        yield return new WaitForSeconds(2); // Simulate saving time
        savingAnimation.SetActive(false);
    }
}
