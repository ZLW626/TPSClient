using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

// 坦克炮弹控制
public class ShellController : MonoBehaviour
{
    private LayerMask shootableLayer;
    private float explodeRadius = 5f;
    private ParticleSystem explodeParticles;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        explodeParticles = GameObject.Find("ShellExplosion").GetComponent<ParticleSystem>();
        shootableLayer = LayerMask.GetMask("Shootable");
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            explodeRadius,
            shootableLayer);
        for (int i = 0; i < colliders.Length; ++i)
        {
            EnemyHealth currEnemyHealth = colliders[i].GetComponent<EnemyHealth>();
            if(currEnemyHealth != null)
            {
                int damage = 10;
                EnemyController enemyController =
                    colliders[i].GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    MsgCSEnemyTakeDamage msg =
                        new MsgCSEnemyTakeDamage(enemyController.enemyID);
                    byte[] dataToSend = msg.Marshal();
                    SocketClient.netStream.Write(dataToSend, 0, dataToSend.Length);
                }
            }

            if (!currEnemyHealth)
                continue;
            //int damage = 20;
            //currEnemyHealth.TakeDamage(damage);
            //Debug.Log("shell");

        }
        explodeParticles.transform.position  = transform.position;
        explodeParticles.Play();
        Destroy(gameObject);
    }
}
