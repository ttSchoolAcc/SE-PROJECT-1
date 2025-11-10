using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [SerializeField] Transform mainChatBox;
    [SerializeField] TextMeshProUGUI playerMessage;
    [SerializeField] GameObject textEntryTemplate;


    public void BUTTONPostChat()
    {
        if(playerMessage.text != "")
        {
            string currUserName = "GUEST";
            if(LoginManager.instance.currAccount.userName != "")
            {
                currUserName = LoginManager.instance.currAccount.userName;
            }

            GameObject newChatEntry = Instantiate(textEntryTemplate, mainChatBox);
            newChatEntry.GetComponentInChildren<TextMeshProUGUI>().text = "<color=red>" + currUserName + "<color=black>" + ":" + playerMessage.text;            
        }
    }
}
