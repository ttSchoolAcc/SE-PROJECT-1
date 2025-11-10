using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
//using System.Diagnostics;

[System.Serializable]
public class UserInfo
{
    public UserInfo(string newUserName, string newPassword, int newAccountID)
    {
        userName = newUserName;
        passWord = newPassword;
        accountID = newAccountID;
    }

    public string userName;
    public string passWord;

    public int accountID;
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
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI passwordText;

    [SerializeField] GameObject loginErrorMess;
    [SerializeField] GameObject bulletinBoard;
    [SerializeField] GameObject loginScreen;

    string jsonPath;
    List<UserInfo> userInfoList = new List<UserInfo>();
    [SerializeField] TextMeshProUGUI currUserText;

    [Space(20)]
    public UserInfo currAccount;

    public static LoginManager instance;

    void Awake()
    {
        if(instance == null)
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
            WriteData();
        }
    }

    [ContextMenu("Write Data")]
    private void WriteData()
    {
        UserInfoJSONArray userInfoFromJSON = new UserInfoJSONArray(userInfoList);

        string json = JsonUtility.ToJson(userInfoFromJSON, true);
        File.WriteAllText(jsonPath, json);
    }

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

                currAccount = currUser;
                currUserText.text = currUser.userName;

                return;
            }
        }
        loginErrorMess.SetActive(true);
    }

    public void BUTTONRegister()
    {
        UserInfo newUser = new UserInfo(usernameText.text, passwordText.text, userInfoList.Count);

        bool ifExists = false;
        foreach (UserInfo currUser in userInfoList)
        {
            if (currUser.userName == newUser.userName)// && currUser.passWord == newUser.passWord)
                ifExists = true;
        }

        if (!ifExists)
        {
            userInfoList.Add(newUser);
            WriteData();
        }
    }
}
