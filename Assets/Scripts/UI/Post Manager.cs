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

        TextMeshProUGUI[] postText = newPost.GetComponentsInChildren<TextMeshProUGUI>(); //First is username, second is body text
        
        postText[0].text = "Posted by: " + LoginManager.instance.currAccount.userName;
        postText[1].text = userPostDraft.text;

        userPostDraft.text = "";
    }
}
