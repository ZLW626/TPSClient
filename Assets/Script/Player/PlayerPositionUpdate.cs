using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

public class PlayerPositionUpdate : MonoBehaviour
{
    //更新玩家位置到服务器/请求其他玩家位置
    private float interval = 0.1f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
