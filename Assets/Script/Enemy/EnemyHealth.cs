using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    private Slider slider;
    private Image hpImage;
    private Text hpText;
    private PlayerMoney playerMoney;
    private int enemyVal = 10;
    [SerializeField] private HealthBarController healthBarController;
    private EnemyAnimation enemyAnimation;
    [SerializeField] private PlayerStatusBarController playerStatusBarController;
    // Start is called before the first frame update
    void Start()
    {
        playerMoney = GameObject.Find("Player").GetComponent<PlayerMoney>();
        enemyAnimation = GetComponent<EnemyAnimation>();
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
        if (health <= 0)
        {
            //敌人死亡
            Destroy(gameObject, 2f);

            //增加玩家金钱
            playerStatusBarController.money += enemyVal;
            playerStatusBarController.UpdateMoneyText();

            //playerMoney.AddMoney(enemyVal);
        }
        
            

    }
}
