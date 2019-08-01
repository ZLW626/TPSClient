using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    //public RectTransform rect;
    public PlayerStatusBarController selfPlayerStatusBarController;
    public GameObject otherPlayerStatusBar;

    
    void Start()
    {
        selfPlayerStatusBarController = GameObject.Find("SelfPlayerStatus").
            GetComponent<PlayerStatusBarController>();
        otherPlayerStatusBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamageSingleShoot(int damage)
    {
        health -= damage;
        PlayerPrefs.SetInt("hp", health);
        selfPlayerStatusBarController.SetHpBar(health);
    }
}
