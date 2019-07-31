using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

public class PlayerPositionUpdate : MonoBehaviour
{
    //更新玩家位置到服务器/接收其他玩家位置
    private float interval = 0.1f;
    private float timer = 0f;

    [SerializeField] private OtherPlayerManager otherPlayerManager;
    private string selfPlayerName;
    private string currName;
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        selfPlayerName = PlayerPrefs.GetString("name");
        currName = playerController.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        if(selfPlayerName.Equals(currName))
        {
            UpdatePlayerPositionToServer();
        }
        else
        {
            ReceivePlayerPositionFromServer();
        }
    }

    private void UpdatePlayerPositionToServer()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            MsgCSPlayerPosition msgPos =
                new MsgCSPlayerPosition(
                    transform.position.x, transform.position.z
                    );
            byte[] msgPacked = msgPos.Marshal();
            SocketClient.netStream.Write(msgPacked, 0, msgPacked.Length);

        }
    }

    private void ReceivePlayerPositionFromServer()
    {
        byte[] dataReceivedNoHead = SocketClient.RemoveDataHead();
        MsgSCBase msgSCBase = new UnifromUnmarshal().Unmarshal(dataReceivedNoHead);

        MsgSCBroadcastPlayerPosition msgPos = 
            (MsgSCBroadcastPlayerPosition)msgSCBase;

        otherPlayerManager.
            otherPlayerDict[msgPos.playerName].
            PlayerMoveDrivenByServer(msgPos.x, msgPos.z);
    }
}
