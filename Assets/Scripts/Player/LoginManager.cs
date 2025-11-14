using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
//using System.Diagnostics;




[System.Serializable]
public class UserInfo
{
    const int MAX_MOVES_PER_TIME_PERIOD = 1;
    public UserInfo(string newUserName, string newPassword, int newAccountID)
    {
        userName = newUserName;
        passWord = newPassword;
        accountID = newAccountID;
        numOfMoves = MAX_MOVES_PER_TIME_PERIOD;
    }

    public string userName;
    public string passWord;
    public int accountID;
    public int numOfMoves;
}

class UserInfoJSONArray
{
    public UserInfoJSONArray(List<UserInfo> newArr)
    {
        userInfoArr = newArr;
    }
    public List<UserInfo> userInfoArr;
}

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameText;
    public TMP_InputField passwordText;


    [SerializeField] GameObject loginErrorMess;
    [SerializeField] GameObject bulletinBoard;
    [SerializeField] GameObject loginScreen;


    string jsonPath;
    [SerializeField] List<UserInfo> userInfoList = new List<UserInfo>();
    [SerializeField] TextMeshProUGUI currUserText;


    [Space(20)]
    const int MAX_MOVES_PER_TIME_PERIOD = 1;
    public UserInfo currAccount;

    public static LoginManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        jsonPath = Path.Combine(Application.persistentDataPath, "UserDataBase.json");
        Debug.Log(jsonPath);

        if (File.Exists(jsonPath))
        {
            ReadData();
        }
        else
        {
            FirstDataBaseCreation();
            WriteData();
        }

        RefreshNumOfMoves(); /////////////////////////////////////////////////////////REMOVE LATER, THIS IS FOR DEMONSTRATION
        ChangeAccount(userInfoList[0]);
    }

    void FirstDataBaseCreation()
    {
        userInfoList.Clear();

        //Create 5 Special users. Just in case
        userInfoList.Add(new UserInfo("GUEST", "", userInfoList.Count)); //First user is GUEST. This is default, Anyone can use this, but  you have no moves
        userInfoList.Add(new UserInfo("ADMIN", "ADMIN", userInfoList.Count)); //Second user is ADMIN. It's basically cheats
        userInfoList.Add(new UserInfo("Buffer1", "", userInfoList.Count)); //These
        userInfoList.Add(new UserInfo("Buffer2", "", userInfoList.Count)); //Three are
        userInfoList.Add(new UserInfo("Buffer3", "", userInfoList.Count)); //Buffers, just in case i need to add more
        WriteData();
    }
    

    [ContextMenu("Write Data")]
    private void WriteData()
    {
        UserInfoJSONArray userInfoFromJSON = new UserInfoJSONArray(userInfoList);

        string json = JsonUtility.ToJson(userInfoFromJSON, true);
        File.WriteAllText(jsonPath, json);
    }

    //This is where we add all accounts from the save json to the running game.
    [ContextMenu("Read Data")]
    private void ReadData()
    {
        string json = File.ReadAllText(jsonPath);
        UserInfoJSONArray userInfoFromJSON = JsonUtility.FromJson<UserInfoJSONArray>(json);

        if(userInfoFromJSON.userInfoArr != null)
        {
            foreach (UserInfo user in userInfoFromJSON.userInfoArr)
            {
                userInfoList.Add(user);
            }
        }

// #if UNITY_EDITOR
//         UnityEditor.EditorUtility.SetDirty(this);
// #endif
    }



    public void BUTTONLoginCheck()
    {
        foreach (UserInfo currUser in userInfoList)
        {
            //This is if you successfully logged in
            if (usernameText.text == currUser.userName && passwordText.text == currUser.passWord)// && currUser.passWord == newUser.passWord)
            {
                //UIManager.
                loginScreen.SetActive(false);
                bulletinBoard.SetActive(true);

                ChangeAccount(currUser);

                return;
            }
        }
        loginErrorMess.SetActive(true);
    }

    public void BUTTONRegister()
    {
        UserInfo newUser = new UserInfo(usernameText.text, passwordText.text, userInfoList.Count);

        bool ifUserExists = false;
        foreach (UserInfo currUser in userInfoList)
        {
            if (currUser.userName == newUser.userName)// && currUser.passWord == newUser.passWord)
                ifUserExists = true;
        }

        if (!ifUserExists)
        {
            userInfoList.Add(newUser);
            WriteData();
        }
    }

    public void BUTTONLogout()
    {
        ChangeAccount(userInfoList[0]);
    }

    void RefreshNumOfMoves()
    {
        foreach (UserInfo user in userInfoList)
        {
            user.numOfMoves = MAX_MOVES_PER_TIME_PERIOD;
            WriteData();
        }
    }

    public void PlayerHasMoved()
    {
        if (currAccount.numOfMoves > 0)
        {
            currAccount.numOfMoves--;
            UIManager.instance.DecrementMarkerCount();
            WriteData();
        }
    }
    
    void ChangeAccount(UserInfo currUser)
    {
        currAccount = currUser;
        currUserText.text = currUser.userName;

        UIManager.instance.ClearMarkers();
        for (int i = 0; i < currAccount.numOfMoves; i++)
            UIManager.instance.IncrementMarkerCount();
    }
}
