using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // 敌人受击动画
    public void TakeDamage(int health)
    {
        playerAnimator.SetInteger("hp", health);
        playerAnimator.SetTrigger("damage");
    }

    // 敌人射击动画
    public void Shoot()
    {
        playerAnimator.SetTrigger("shoot");
    }
}
