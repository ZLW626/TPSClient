using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private PlayerStatusBarController playerStatusBarController;

    // Start is called before the first frame update
    void Start()
    {
        playerStatusBarController = GameObject.Find("SelfPlayerStatus").GetComponent<PlayerStatusBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItems()
    {
        if(playerStatusBarController.money > 0)
        {
            //playerStatusBarController.money -= 10;
            playerStatusBarController.UpdateMoneyText(-10);
            playerStatusBarController.ammo += 40;
            playerStatusBarController.UpdateAmmoText();
            playerStatusBarController.grenade += 10;
            playerStatusBarController.UpdateGrenadeText();
            playerStatusBarController.shell += 5;
            playerStatusBarController.UpdateShellText();
        }
    }
}
