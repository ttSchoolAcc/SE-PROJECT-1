using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
class UserInfo
{
    public UserInfo(string newUserName, string newPassword)
    {
        userName = newUserName;
        passWord = newPassword;
    }

    public string userName;
    public string passWord;
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
    string jsonPath;

    List<UserInfo> userInfoList = new List<UserInfo>();

    void Awake()
    {
        jsonPath = Path.Combine(Application.persistentDataPath, "UserDataBase.json");

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

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }



    public void BUTTONLoginCheck()
    {
        Debug.Log(Application.persistentDataPath);
    }

    public void BUTTONRegister()
    {
        UserInfo newUser = new UserInfo(usernameText.text, passwordText.text);

        bool ifExists = false;
        foreach (UserInfo currUser in userInfoList)
        {
            if (currUser.userName == newUser.userName)// && currUser.passWord == newUser.passWord)
                ifExists = true;
        }
        
        if(!ifExists)
        {
            userInfoList.Add(newUser);
            WriteData();
        }
    }
}
