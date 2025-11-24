using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PostInfo : MonoBehaviour
{
    public List<string> allComments = new List<string>();
    public string postText;
    public string posterName;



    public void BUTTONOpenPostRelay()
    {
        PostManager.instance.OpenPost(this);
    }
}
