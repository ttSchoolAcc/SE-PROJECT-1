using TMPro;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    [SerializeField] GameObject postObj;
    [SerializeField] Transform postParent;
    [SerializeField] TextMeshProUGUI userPostDraft;

    public void BUTTONCreatePost()
    {
        GameObject newPost = Instantiate(postObj, postParent);
        TextMeshProUGUI postText = newPost.GetComponentInChildren<TextMeshProUGUI>();

        postText.text = userPostDraft.text;
        userPostDraft.text = "";
    }
}
