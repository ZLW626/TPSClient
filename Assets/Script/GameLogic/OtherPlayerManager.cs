using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

public class OtherPlayerManager : MonoBehaviour
{
    //public Dictionary<string, PlayerController> otherPlayerDict;
    [SerializeField] private GameObject playerPrefab;
    private OtherPlayerManagerPre otherPlayerManagerPre;
    public Transform[] bornPoint;
    private GameObject playerMain;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeOtherPlayers()
    {
        //for(int i = 0; ;++i)
        //{
        //    string name;
        //    if (PlayerPrefs.HasKey("playerName" + i))
        //        name = PlayerPrefs.GetString("playerName" + i);
        //    else
        //        break;
        //}
        int otherPlayerNum = otherPlayerManagerPre.otherPlayerNameList.Count;
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
            Debug.Log("other player");
        }

        //foreach(var item in otherPlayerManagerPre.otherPlayerDict)
        //{
        //    string name = item.Key;
        //    int hp = item.Value;
        //    GameObject currPlayer = 
        //        Instantiate(playerPrefab, 
        //        bornPoint.position, 
        //        bornPoint.rotation);
            
        //}

        //MsgCSAskForOtherPlayer msgAsk = new MsgCSAskForOtherPlayer();
        //byte[] msgPacked = msgAsk.Marshal();
        //SocketClient.netStream.Write(msgPacked, 0, msgPacked.Length);

        //byte[] dataReceivedNoHead = SocketClient.RemoveDataHead();
        //MsgSCBase msgSCBase = new UnifromUnmarshal().Unmarshal(dataReceivedNoHead);
        //MsgSCOtherPlayer msgOtherPlayer = (MsgSCOtherPlayer)msgSCBase;

        //int otherPlayerNum = msgOtherPlayer.otherPlayerNum;
        //for(int i = 0;i < otherPlayerNum;++i)
        //{
        //    GameObject currPlayer = Instantiate(
        //        playerPrefab);
        //    otherPlayerDict.Add(
        //        msgOtherPlayer.otherPlayerName[i],
        //        currPlayer.GetComponent<PlayerController>());

        //}
    }

    public void InitializeOnePlayer()
    {

    }
}
