using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Network;
using Assets.Script.Common;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.SceneManagement;

public class LoginOrRegister : MonoBehaviour
{
    // 登录游戏UI控件
    [SerializeField] private Button loginButton;
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private InputField loginNameIF;
    [SerializeField] private InputField loginPasswordIF;

    // 创建角色UI控件
    [SerializeField] private Button registerButton;
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private InputField registerNameIF;
    [SerializeField] private InputField registerPasswordIF;
    [SerializeField] private InputField registerPasswordIF2;

    // 大厅UI控件
    [SerializeField] private GameObject hallPanel;
    private OtherPlayerManagerPre otherPlayerManager;

    // Start is called before the first frame update
    void Start()
    {
        otherPlayerManager =
            GameObject.Find("OtherPlayerManagerPre").
            GetComponent<OtherPlayerManagerPre>();
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    public void OnLoginBtnClicked()
    {
        loginPanel.SetActive(true);
    }

    public void OnLoginOKBtnClicked()
    {
        // 获取输入框的用户名和密码, 并使用MD5加密密码
        string username = loginNameIF.text;
        string password = MD5Encryption(loginPasswordIF.text);

        // 发送登录信息
        MsgCSLogin msg = new MsgCSLogin(username, password);
        byte[] dataToSend = msg.Marshal();
        SocketClient.netStream.Write(dataToSend, 0, dataToSend.Length);

        // 接收登录确认信息
        byte[] dataReceivedNoHead = SocketClient.RemoveDataHead();
        MsgSCBase msgSCBase = new UnifromUnmarshal().Unmarshal(dataReceivedNoHead);
        MsgSCLoginConfirm msgConfirm = (MsgSCLoginConfirm)msgSCBase;
        int confirmCode = msgConfirm.confirm;
        if (confirmCode >= 0) // 登录成功
        {
            // 暂存玩家之前的信息
            otherPlayerManager.loginIDMain = confirmCode;
            loginPanel.SetActive(false);
            PlayerPrefs.SetInt("loginID", confirmCode);
            PlayerPrefs.SetString("name", username);
            PlayerPrefs.SetInt("hp", msgConfirm.hp);
            PlayerPrefs.SetInt("money", msgConfirm.money);
            PlayerPrefs.SetInt("ammo", msgConfirm.ammo);
            PlayerPrefs.SetInt("grenade", msgConfirm.grenade);
            PlayerPrefs.SetInt("shell", msgConfirm.shell);

            hallPanel.SetActive(true);
            hallPanel.GetComponent<HallPanelController>().AddPlayerToHall(username);
        }
        else // 登录失败
        {
            loginNameIF.text = "";
            loginPasswordIF.text = "";
            loginPanel.SetActive(false);
        }
            


    }
    public void OnLoginCancelBtnClicked()
    {
        loginNameIF.text = "";
        loginPasswordIF.text = "";
        loginPanel.SetActive(false);
    }

    public void OnRegisterBtnClicked()
    {
        registerPanel.SetActive(true);
    }
    public void OnRegisterOKBtnClicked()
    {
        //比较两次输入的密码是否一致
        if(registerPasswordIF.text.Equals(registerPasswordIF2.text))
        {
            //如果一致, 获取输入框的用户名和密码,并加密密码
            string username = registerNameIF.text;
            string password = MD5Encryption(registerPasswordIF.text);
            MsgCSRegister msg = new MsgCSRegister(username, password);
            byte[] msgPacked = msg.Marshal();
            SocketClient.netStream.Write(msgPacked, 0, msgPacked.Length);

            byte[] dataReceivedNoHead = SocketClient.RemoveDataHead();
            MsgSCBase msgComfirm = new UnifromUnmarshal().Unmarshal(dataReceivedNoHead);
            int comfirmCode = ((MsgSCConfirm)msgComfirm).confirm;
            if (comfirmCode == 1)
            {
                registerPanel.SetActive(false);
            }
            else
                Debug.Log("register failed!");

        }
        else
        {
            Debug.Log("passwords mismatch!");
        }
    }
    public void OnRegisterCancelBtnClicked()
    {
        registerNameIF.text = "";
        registerPasswordIF.text = "";
        registerPasswordIF2.text = "";
        registerPanel.SetActive(false);
    }

    private string MD5Encryption(string plainText)
    {
        MD5 md5 = MD5.Create();
        byte[] bytesPlainText = Encoding.Default.GetBytes(plainText);
        byte[] bytesPlainTextEncrypted = md5.ComputeHash(bytesPlainText);

        StringBuilder stringBuilder = new StringBuilder();
        for(int i = 0;i < bytesPlainTextEncrypted.Length;++i)
        {
            stringBuilder.Append(bytesPlainTextEncrypted[i].ToString("x2"));
        }
        return stringBuilder.ToString();
    }
}
