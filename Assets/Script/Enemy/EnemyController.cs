using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

public class EnemyController : MonoBehaviour
{
    public int enemyID; // 敌人编号
    private Vector3 targetPos; // 敌人下一步的目标位置, 由服务器给出
    private float speed = 1.5f; // 敌人移动速度
    private float disThreshold = 0.1f; // 用以判断敌人是否到达下一个位置
    private EnemyHealth enemyHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // 敌人死亡后不再移动
        if(enemyHealth.health > 0)
            EnemeyMove();
    }

    void EnemeyMove()
    {
        // 如果到达目标点, 向服务器请求新的位置
        if (IsSamePoint(transform.position, targetPos))
        {
            MsgCSAskForEnemyPosition msgAsk = new MsgCSAskForEnemyPosition(enemyID);
            byte[] msgPacked = msgAsk.Marshal();
            SocketClient.netStream.Write(msgPacked, 0, msgPacked.Length);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.forward = (targetPos - transform.position).normalized;
    }


    bool IsSamePoint(Vector3 p1, Vector3 p2)
    {
        return Vector3.Distance(p1, p2) < disThreshold;
    }
    
    // 设置敌人下一步的位置
    public void UpdateTargetPos(float x, float z)
    {
        targetPos.x = x;
        targetPos.z = z;
    }
}
