using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Script.Network;
using Assets.Script.Common;

public class HallPanelController : MonoBehaviour
{
    public GameObject hallPanel;
    public Text[] playerInHall;
    private int currIndex = 0;

    private OtherPlayerManagerPre otherPlayerManagerPre;
    // Start is called before the first frame update
    void Start()
    {
        
        //playerInHall = new Text[3];
        for (int i = 0; i < 3; ++i)
            playerInHall[i].gameObject.SetActive(false);
        hallPanel.SetActive(false);

        otherPlayerManagerPre = GameObject.Find("OtherPlayerManagerPre").GetComponent<OtherPlayerManagerPre>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerToHall(MsgSCPlayerInfoInHall msg)
    {
        playerInHall[currIndex].text = msg.name;
        playerInHall[currIndex].gameObject.SetActive(true);

        //PlayerPrefs.SetString("playerName" + currIndex, name);
        currIndex++;

        otherPlayerManagerPre.otherPlayerNameList.Add(msg.name);
        otherPlayerManagerPre.otherPlayerHpList.Add(msg.hp);
        otherPlayerManagerPre.otherPlayerLoginIDList.Add(msg.loginID);
    }
    public void AddPlayerToHall(string currName)
    {
        playerInHall[currIndex].text = currName;
        playerInHall[currIndex].gameObject.SetActive(true);

        //PlayerPrefs.SetString("playerName" + currIndex, name);
        currIndex++;

        //otherPlayerManagerPre.otherPlayerNameList.Add(msg.name);
        //otherPlayerManagerPre.otherPlayerHpList.Add(msg.hp);
        //otherPlayerManagerPre.otherPlayerLoginIDList.Add(msg.loginID);
    }

    public void OnStartBtnClicked()
    {
        hallPanel.SetActive(false);

        //告知服务器, 本客户端想要开始游戏
        MsgCSStartGame msg = new MsgCSStartGame();
        byte[] dataToSend = msg.Marshal();
        SocketClient.netStream.Write(dataToSend, 0, dataToSend.Length);

    }

    public void StartGame()
    {
        SceneManager.LoadScene("BattlefieldScene");
    }


}
