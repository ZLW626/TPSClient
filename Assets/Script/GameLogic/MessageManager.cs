﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;
using UnityEngine.SceneManagement;

public class MessageManager : MonoBehaviour
{
    public HallPanelController hallPanelController;
    private EnemyManager enemyManager;
    private GameManager gameManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMessage();
    }

    void ProcessMessage()
    {
        if (!SocketClient.netStream.DataAvailable)
            return;
        byte[] dataReceivedNoHead = SocketClient.RemoveDataHead();

        MsgSCBase msgSCBase = new UnifromUnmarshal().Unmarshal(dataReceivedNoHead);
        //Debug.Log(msgSCBase.sid_cid);
        switch (msgSCBase.sid_cid)
        {
            case 0x1003://MSG_SC_LOGIN_CONFIRM
                //登录操作: 发送用户名和密码与接收服务器确认是连贯的流程,不应该分开
                break;

            case 0x1006: // MSG_SC_PLAYER_INFO_IN_HALL
                MsgSCPlayerInfoInHall msg1006 = (MsgSCPlayerInfoInHall)msgSCBase;
                hallPanelController.AddPlayerToHall(msg1006);
                break;
            case 0x2001://MSG_SC_CONFIRM
                MsgSCConfirm msg2001 = (MsgSCConfirm)msgSCBase;
                //Debug.Log(msg2001.confirm);
                switch (msg2001.confirm)
                {
                    case 1: // 场景跳转
                        if(hallPanelController != null)
                            hallPanelController.StartGame();
                        break;
                    case 2:

                        break;
                }
                //if (msg2001.confirm == 1 && hallPanelController != null)
                //    hallPanelController.StartGame();
                
                if(gameManager != null)
                {
                    gameManager.countdownText.text = "";
                    gameManager.roundNumText.text = "Round " + msg2001.confirm;
                }
                    
                break;
            case 0x3002://MSG_SC_ENEMY_INITIALIZE
                
                MsgSCEnemyInitialize msg3002 = (MsgSCEnemyInitialize)msgSCBase;
                Debug.Log(msg3002.sid_cid);
                if (enemyManager == null)
                {
                    enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
                }
                if (gameManager == null)
                    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                enemyManager.InitializeEnemies(msg3002);
                break;
            case 0x3004://MSG_SC_ENEMY_POSITION
                
                MsgSCEnemyPosition msg3004 = (MsgSCEnemyPosition)msgSCBase;
                if (enemyManager == null)
                {
                    enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
                }
                enemyManager.enemyDict[msg3004.enemy_id].GetComponent<EnemyController>().UpdateTargetPos(msg3004.x,msg3004.z);
                break;
            case 0x4002://MSG_SC_BROADCAST_PLAYER_POSITION
                break;
            case 0x4004://MSG_SC_OTHER_PLAYER
                MsgSCOtherPlayer msg4004 = (MsgSCOtherPlayer)msgSCBase;
                hallPanelController.AddPlayerToHall(msg4004.otherPlayerName);
                break;
            case 0x5001://MSG_SC_ROUND_END
                MsgSCRoundEnd msg5001 = (MsgSCRoundEnd)msgSCBase;
                if (gameManager == null)
                    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                if (msg5001.round == 2)
                {
                    gameManager.SavePlayer();
                    SceneManager.LoadScene("GameOverScene");//结束游戏
                }
                else
                    gameManager.Countdown();
                break;
            case 0x3007: //MSG_SC_ENEMY_TAKE_DAMAGE
                MsgSCEnemyTakeDamage msg3007 = (MsgSCEnemyTakeDamage)msgSCBase;
                if (enemyManager == null)
                {
                    enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
                }
                enemyManager.enemyDict[msg3007.enemyID].GetComponent<EnemyHealth>().TakeDamage(10);
                break;
        }
    }
}
