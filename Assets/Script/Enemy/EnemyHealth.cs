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
    private Slider slider;
    private Image hpImage;
    private Text hpText;
    //private PlayerMoney playerMoney;
    private int enemyVal = 10;
    [SerializeField] private HealthBarController healthBarController;
    private EnemyAnimation enemyAnimation;
    private PlayerStatusBarController playerStatusBarController;//GameObject Find
    private EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        playerStatusBarController = GameObject.Find("SelfPlayerStatus").GetComponent<PlayerStatusBarController>();
        //playerMoney = GameObject.Find("Player").GetComponent<PlayerMoney>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        enemyController = GetComponent<EnemyController>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

            //Debug.Log(playerStatusBarController.money);
            
            //playerStatusBarController.money += enemyVal;
            //Debug.Log(playerStatusBarController.money);
            //playerStatusBarController.UpdateMoneyText();

            //playerMoney.AddMoney(enemyVal);
        }
        
            

    }
}
