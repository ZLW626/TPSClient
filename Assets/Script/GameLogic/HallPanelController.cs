using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Script.Network;
using Assets.Script.Common;

// 进入游戏前的等待大厅
public class HallPanelController : MonoBehaviour
{
    public GameObject hallPanel;
    public Text[] playerInHall; // 用以显示玩家名称
    private int currIndex = 0;

    private OtherPlayerManagerPre otherPlayerManagerPre; // 保存大厅内的玩家信息, 以便后面一个场景利用
    // Start is called before the first frame update
    void Start()
    { 
        for (int i = 0; i < 3; ++i)
            playerInHall[i].gameObject.SetActive(false);
        hallPanel.SetActive(false);

        otherPlayerManagerPre = GameObject.Find("OtherPlayerManagerPre").GetComponent<OtherPlayerManagerPre>();
    }

    // 添加一个其他玩家到大厅
    public void AddPlayerToHall(MsgSCPlayerInfoInHall msg)
    {
        playerInHall[currIndex].text = msg.name;
        playerInHall[currIndex].gameObject.SetActive(true);
        currIndex++;

        // 保存大厅内的玩家信息, 以便后面一个场景利用
        otherPlayerManagerPre.otherPlayerNameList.Add(msg.name);
        otherPlayerManagerPre.otherPlayerHpList.Add(msg.hp);
        otherPlayerManagerPre.otherPlayerLoginIDList.Add(msg.loginID);
    }

    // 添加本客户端玩家到大厅
    public void AddPlayerToHall(string currName)
    {
        playerInHall[currIndex].text = currName;
        playerInHall[currIndex].gameObject.SetActive(true);
        currIndex++;
    }

    public void OnStartBtnClicked()
    {
        hallPanel.SetActive(false);

        // 告知服务器, 本客户端想要开始游戏
        MsgCSStartGame msg = new MsgCSStartGame();
        byte[] dataToSend = msg.Marshal();
        SocketClient.netStream.Write(dataToSend, 0, dataToSend.Length);
    }

    // 跳转游戏场景
    public void StartGame()
    {
        SceneManager.LoadScene("BattlefieldScene");
    }


}
