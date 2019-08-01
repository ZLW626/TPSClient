using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Script.Network;
using Assets.Script.Common;

// 游戏场景管理
public class GameManager : MonoBehaviour
{
    //private int currRound = 1;
    private int countdown = 10;                   // 每回合结束后倒计时10s
    private EnemyManager enemyManager;            // 敌人管理
    private OtherPlayerManager otherPlayerManager;// 其他在线玩家管理

    [SerializeField] public Text roundNumText;         // 显示回合数
    [SerializeField] public Text countdownText;        // 显示倒计时
    //[SerializeField] public InputManager inputManager; // 

    //private bool tempFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        otherPlayerManager = GameObject.Find("OtherPlayerManager").GetComponent<OtherPlayerManager>();

        roundNumText.text = "Round 1";
        countdownText.text = "";
    }

    //private IEnumerator PreRound()
    //{
    //    //UI
    //    roundNumText.text = "Round " + currRound;
    //    countdownText.text = "";

    //    //初始化敌人
    //    //enemyManager.InitializeEnemies(currRound);

    //    //使玩家待机
    //    inputManager.enablePlayer = false;

    //    yield return new WaitForSeconds(3f);
    //}

    //private IEnumerator InRound()
    //{
    //    //UI
    //    roundNumText.text = "";

    //    //激活玩家
    //    inputManager.enablePlayer = true;


    //    //当场景还有敌人存在就一直进行下去
    //    while (!Input.GetKeyDown(KeyCode.E))
    //    {
    //        yield return null;
    //    }
    //}

    //private IEnumerator AfterRound()
    //{
    //    //使玩家待机
    //    inputManager.enablePlayer = false;

    //    //检查是否是第三波

    //    currRound++;
    //    //倒计时
    //    int leftSeconds = countdown;
    //    while(leftSeconds >= 0)
    //    {
    //        countdownText.text = leftSeconds + " s!";
    //        yield return new WaitForSeconds(1f);
    //        leftSeconds--;
    //    }

    //}

    //private IEnumerator RoundLoop()
    //{
    //    yield return StartCoroutine(PreRound());
    //    yield return StartCoroutine(InRound());
    //    yield return StartCoroutine(AfterRound());

    //    if(tempFlag || currRound == 3)
    //    {
    //        SceneManager.LoadScene("GameOverScene");
    //    }
    //    {
    //        StartCoroutine(RoundLoop());
    //    }
    //}

    private IEnumerator CountdownCore()
    {
        int leftSeconds = countdown;
        while (leftSeconds >= 0)
        {
            countdownText.text = leftSeconds + " s!";
            yield return new WaitForSeconds(1f);
            leftSeconds--;
        }

        //倒计时结束后, 告知服务器, 本客户端想要开始下一回合
        MsgCSStartGame msg = new MsgCSStartGame();
        byte[] dataToSend = msg.Marshal();
        SocketClient.netStream.Write(dataToSend, 0, dataToSend.Length);
    }

    public void Countdown()
    {
        StartCoroutine(CountdownCore());
    }

    // 所有回合结束后保存玩家信息
    public void SavePlayer()
    {
        MsgCSSavePlayer msg =
                        new MsgCSSavePlayer(
                            PlayerPrefs.GetString("name"),
                            PlayerPrefs.GetInt("hp"),
                            PlayerPrefs.GetInt("money"),
                            PlayerPrefs.GetInt("ammo"),
                            PlayerPrefs.GetInt("grenade"),
                            PlayerPrefs.GetInt("shell"));
        byte[] dataToSend = msg.Marshal();
        SocketClient.netStream.Write(dataToSend, 0, dataToSend.Length);
    }
}
