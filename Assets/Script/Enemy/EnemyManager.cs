using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;


// 敌人管理器, 负责敌人的生成
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform[] bornPoints;     // 敌人出生点
    [SerializeField] private GameObject longRangePrefab; // 两种敌人预设体
    [SerializeField] private GameObject explodePrefab;

    public Dictionary<int, GameObject> enemyDict;
    // Start is called before the first frame update
    void Start()
    {
        enemyDict = new Dictionary<int, GameObject>();
    }

    // 根据服务器发来的数据初始化敌人
    public void InitializeEnemies(MsgSCEnemyInitialize msgEnemyData)
    {
        enemyDict.Clear();
        int enemyNum = msgEnemyData.enemyNum;
        for (int i = 0; i < enemyNum; ++i)
        {
            int enemyID = msgEnemyData.enemies[i].enemyID;
            int enemyType = msgEnemyData.enemies[i].enemyType;
            GameObject currEnemy;
            Debug.Log("enemy num: " + enemyNum);
            Debug.Log("enemy id: " + enemyID);
            if (enemyType == 0)//实例化一个远程敌人
                currEnemy = GameObject.Instantiate(
                    longRangePrefab,
                    bornPoints[enemyID].position,
                    bornPoints[enemyID].rotation);
            else//实例化一个爆炸敌人
                currEnemy = GameObject.Instantiate(
                        explodePrefab,
                        bornPoints[enemyID].position,
                        bornPoints[enemyID].rotation);

            currEnemy.GetComponent<EnemyController>().enemyID = enemyID;
            currEnemy.GetComponent<EnemyHealth>().enemyID = enemyID;
            enemyDict.Add(enemyID, currEnemy);
        }
    }
}
