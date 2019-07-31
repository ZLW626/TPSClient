using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

public class EnemyController : MonoBehaviour
{
    public int enemyID;
    public List<Vector2> path;
    //private float requestInterval = 0.1f;
    //private float timer = 0f;
    private Vector3 targetPos;
    private float speed = 1.5f;
    private float disThreshold = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //if(timer >= requestInterval)
        EnemeyMove();
    }

    void EnemeyMove()
    {
        if (IsSamePoint(transform.position, targetPos))
        {
            MsgCSAskForEnemyPosition msgAsk = new MsgCSAskForEnemyPosition(enemyID);
            byte[] msgPacked = msgAsk.Marshal();
            SocketClient.netStream.Write(msgPacked, 0, msgPacked.Length);

            byte[] dataReceivedNoHead = SocketClient.RemoveDataHead();
            MsgSCEnemyPosition msgEnemyPosition = (MsgSCEnemyPosition)
                (new UnifromUnmarshal().Unmarshal(dataReceivedNoHead));

            targetPos.x = msgEnemyPosition.x;
            targetPos.z = msgEnemyPosition.z;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.forward = (targetPos - transform.position).normalized;
    }

    void AskForPath()
    {

    }

    bool IsSamePoint(Vector3 p1, Vector3 p2)
    {
        return Vector3.Distance(p1, p2) < disThreshold;
    }
}
