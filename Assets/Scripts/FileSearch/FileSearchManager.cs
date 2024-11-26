using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FileSearchManager : MonoBehaviour
{
    public Transform searchResultsParent;
    public GameObject searchResultPrefab;
    public List<VideoDataSO> videoDataList;
    [SerializeField] private TMP_InputField searchInputField;

    private SearchResult currentSelectedResult;

    private void Start()
    {
        if (searchResultsParent == null || searchResultPrefab == null)
        {
            Debug.LogError("One or more required components are not assigned.");
            return;
        }

        DisplayAllResults();
        searchInputField.onEndEdit.AddListener(OnSearchSubmitted);
    }

    private void DisplayAllResults()
    {
        foreach (Transform child in searchResultsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var videoData in videoDataList)
        {
            GameObject resultObj = Instantiate(searchResultPrefab, searchResultsParent);
            SearchResult result = resultObj.GetComponent<SearchResult>();
            result.Setup(videoData);
            result.onVideoSelected.AddListener(OnSearchResultSelected);
        }
    }

    private void OnSearchSubmitted(string searchTerm)
    {
        List<VideoDataSO> filteredResults = videoDataList.FindAll(video => video.name.Contains(searchTerm));

        foreach (Transform child in searchResultsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var videoData in filteredResults)
        {
            GameObject result = Instantiate(searchResultPrefab, searchResultsParent);
            result.GetComponent<SearchResult>().Setup(videoData);
        }
    }

    public void OnSearchResultSelected(SearchResult searchResult)
    {
        currentSelectedResult?.DoUnselect();
        currentSelectedResult = searchResult;
        currentSelectedResult.DoSelect();

        Info.Instance.SetVideoData(currentSelectedResult.GetVideoData());

        // Mark the video as watched
        currentSelectedResult.GetVideoData().isWatched = true;
    }
}