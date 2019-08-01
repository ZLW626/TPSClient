using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 玩家生命值管理
public class PlayerHealth : MonoBehaviour
{
    public int health;
    public PlayerStatusBarController selfPlayerStatusBarController;
    public GameObject otherPlayerStatusBar;

    // Start is called before the first frame update
    void Start()
    {
        selfPlayerStatusBarController = GameObject.Find("SelfPlayerStatus").
            GetComponent<PlayerStatusBarController>();
        otherPlayerStatusBar.SetActive(false);
    }

    void TakeDamageSingleShoot(int damage)
    {
        health -= damage;
        PlayerPrefs.SetInt("hp", health);
        selfPlayerStatusBarController.SetHpBar(health);
    }
}
