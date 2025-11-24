using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    [SerializeField] GameObject postObj;
    [SerializeField] Transform postParent;
    [SerializeField] TextMeshProUGUI userPostDraft;

    [SerializeField] GameObject commentTemplate;
    [SerializeField] TMP_InputField commentBarText;

    [SerializeField] TextMeshProUGUI postedByText;
    [SerializeField] GameObject ExpandedPost;
    [SerializeField] TextMeshProUGUI expandedPostText;
    [SerializeField] Transform commentParent;

    public static PostManager instance;

    [SerializeField] PostInfo currPostInfo;



    public void Awake()
    {
        if(instance == null || instance != this)
        {
            instance = this;
        }
    }


    public void OpenPost(PostInfo postInfo)
    {
        UIManager.instance.EnableCanvas(ExpandedPost);

        currPostInfo = postInfo;
        postedByText.text = "Posted By: " + postInfo.posterName;

        expandedPostText.text = postInfo.postText;

        // foreach(Transform comment in commentParent)
        // {
        //     comment.SetParent(null);
        //     Destroy(comment.gameObject);
        // }

        while(commentParent.childCount > 0)
        {
            GameObject commentToDestroy = commentParent.GetChild(commentParent.childCount - 1).gameObject;
            commentToDestroy.transform.SetParent(null); //This "instantly removes" the child so the loop for clearmarkers works()
            Destroy(commentToDestroy);
        }

        for(int i = 0; i < postInfo.allComments.Count; i++)
        {
            GameObject newComment = Instantiate(commentTemplate, commentParent);
            //newComment.GetComponentInChildren<TextMeshProUGUI>().text = "<color=red>" + currUserName + "<color=black>" + ":" + playerMessage.text;
            newComment.GetComponentInChildren<TextMeshProUGUI>().text = postInfo.allComments[i];
        }
    }

    public void BUTTONAddComment()
    {
        if(currPostInfo == null) return;


        string textToAdd = "<color=red>" + LoginManager.instance.currAccount.userName + "<color=black>" + ":" + commentBarText.text;
        currPostInfo.allComments.Add(textToAdd);
        OpenPost(currPostInfo); //To Refresh
    }

    public void BUTTONCreatePost()
    {
        GameObject newPost = Instantiate(postObj, postParent);

        TextMeshProUGUI[] postText = newPost.GetComponentsInChildren<TextMeshProUGUI>(); //First is username, second is body text
        
        postText[0].text = "Posted by: " + LoginManager.instance.currAccount.userName;
        postText[1].text = userPostDraft.text;

        PostInfo postInfo = newPost.GetComponent<PostInfo>();
        postInfo.postText = userPostDraft.text;
        postInfo.posterName = LoginManager.instance.currAccount.userName;

        userPostDraft.text = "";
    }
}
