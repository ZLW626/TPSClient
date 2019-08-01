using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.Network;
using Assets.Script.Common;

public class EnemyHealth : MonoBehaviour
{
    public int enemyID; 
    private bool isDead = false;
    public int health;

    // 敌人血条UI
    private Slider slider;
    private Image hpImage;
    private Text hpText;
    [SerializeField] private HealthBarController healthBarController;

    // 敌人所值金币
    private int enemyVal = 10;

    private EnemyAnimation enemyAnimation;
    private EnemyController enemyController;

    private PlayerStatusBarController playerStatusBarController;

    // Start is called before the first frame update
    void Start()
    {
        playerStatusBarController = GameObject.Find("SelfPlayerStatus").GetComponent<PlayerStatusBarController>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyController = GetComponent<EnemyController>();
        isDead = false;
    }

    // 敌人承受伤害
    public void TakeDamage(int damage)
    {
        health -= damage;
        enemyAnimation.TakeDamage(health);
        healthBarController.UpdateValue(damage);
        if (health <= 0 && !isDead)
        {
            //敌人死亡
            isDead = true;

            //增加玩家金钱
            playerStatusBarController.UpdateMoneyText(enemyVal);
            
            //发送敌人死亡信息到服务器
            Debug.Log("enemy die");
            MsgCSEnemyDeath msg = new MsgCSEnemyDeath(enemyController.enemyID);
            byte[] msgPacked = msg.Marshal();
            SocketClient.netStream.Write(msgPacked, 0, msgPacked.Length);

            Destroy(gameObject, 2f);
        }
    }
}
