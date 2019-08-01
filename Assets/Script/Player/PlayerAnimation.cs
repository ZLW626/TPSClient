using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 玩家动画
public class PlayerAnimation : MonoBehaviour
{
    private PlayerController playerController;
    private Animator playerAnimator;
    private int currWeaponID = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
    }

    public void Shoot(bool gunShoot)
    {
        playerAnimator.SetBool("gunShoot", gunShoot);
    }

    public void Reload(bool reload)
    {
        playerAnimator.SetBool("reload", reload);
    }

    public void ChangeGun()
    {
        playerAnimator.SetBool("isHandgun", !playerAnimator.GetBool("isHandgun"));
    }

    public void Move(float h, float v, bool run)
    {
        float adjustRunWalk = 1f;
        if (!run)
            adjustRunWalk *= .5f;
        playerAnimator.SetFloat("speedV", v * adjustRunWalk);
        if (v < 0)
            playerAnimator.SetFloat("speedH", -h * adjustRunWalk);
        else
            playerAnimator.SetFloat("speedH", h * adjustRunWalk);
    }


}
