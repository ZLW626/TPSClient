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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int health)
    {
        playerAnimator.SetInteger("hp", health);
        playerAnimator.SetTrigger("damage");
    }

    public void Shoot()
    {
        playerAnimator.SetTrigger("shoot");
    }
}
