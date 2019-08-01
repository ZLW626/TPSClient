using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

// 其他玩家的管理
public class OtherPlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;    // 玩家预设体
    private OtherPlayerManagerPre otherPlayerManagerPre; // 登录场景的大厅保存的玩家信息
    public Transform[] bornPoint;  // 玩家出生点
    private GameObject playerMain; // 玩家登录号, 用以控制玩家出生点

    // Start is called before the first frame update
    void Start()
    {
        otherPlayerManagerPre = 
            GameObject.Find("OtherPlayerManagerPre").
            GetComponent<OtherPlayerManagerPre>();
        InitializeOtherPlayers();

        playerMain = GameObject.Find("Player");
        int loginIDMain = otherPlayerManagerPre.loginIDMain;
        playerMain.transform.position = bornPoint[loginIDMain].position;
        Debug.LogAssertion(loginIDMain);

    }

    // 从预设体初始化其他玩家
    public void InitializeOtherPlayers()
    {
        int otherPlayerNum = otherPlayerManagerPre.otherPlayerNameList.Count; // 获取其他玩家的数量
        for (int i = 0;i < otherPlayerNum; ++i)
        {
            string currName = otherPlayerManagerPre.otherPlayerNameList[i];
            int currHp = otherPlayerManagerPre.otherPlayerHpList[i];
            int currLoginID = otherPlayerManagerPre.otherPlayerLoginIDList[i];

            GameObject currPlayer =
                Instantiate(playerPrefab,
                bornPoint[currLoginID].position,
                bornPoint[currLoginID].rotation);

            GameObject currPlayerStatusBar = currPlayer.GetComponent<PlayerHealth>().otherPlayerStatusBar;

            currPlayerStatusBar.SetActive(true);
            currPlayerStatusBar.GetComponent<HealthBarController>().otherPlayerName.text = currName;
            currPlayerStatusBar.GetComponent<HealthBarController>().SetHpValue(currHp);
            //设置相机不可用
            currPlayer.GetComponent<PlayerController>().mainCameraObj.SetActive(false);
            //设置玩家姓名
            currPlayer.GetComponent<PlayerController>().playerName = currName;
        }
    }
}
