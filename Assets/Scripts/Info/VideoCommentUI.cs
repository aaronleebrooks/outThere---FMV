using UnityEngine;
using TMPro;

[System.Serializable]
public class VideoComment
{
    public string username;
    public string commentText;
}

public class VideoCommentUI : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text commentText;

    public void Setup(VideoComment videoComment)
    {
        usernameText.text = videoComment.username;
        commentText.text = videoComment.commentText;
    }
}
