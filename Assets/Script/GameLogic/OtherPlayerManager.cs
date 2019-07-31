using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

public class OtherPlayerManager : MonoBehaviour
{
    public Dictionary<string, PlayerController> otherPlayerDict;
    [SerializeField] private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeOtherPlayers()
    {
        MsgCSAskForOtherPlayer msgAsk = new MsgCSAskForOtherPlayer();
        byte[] msgPacked = msgAsk.Marshal();
        SocketClient.netStream.Write(msgPacked, 0, msgPacked.Length);

        byte[] dataReceivedNoHead = SocketClient.RemoveDataHead();
        MsgSCBase msgSCBase = new UnifromUnmarshal().Unmarshal(dataReceivedNoHead);
        MsgSCOtherPlayer msgOtherPlayer = (MsgSCOtherPlayer)msgSCBase;

        int otherPlayerNum = msgOtherPlayer.otherPlayerNum;
        for(int i = 0;i < otherPlayerNum;++i)
        {
            GameObject currPlayer = Instantiate(
                playerPrefab);
            otherPlayerDict.Add(
                msgOtherPlayer.otherPlayerName[i],
                currPlayer.GetComponent<PlayerController>());

        }
    }

    public void InitializeOnePlayer()
    {

    }
}
