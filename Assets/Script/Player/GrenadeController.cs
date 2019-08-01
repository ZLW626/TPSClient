using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;

// 手雷控制
public class GrenadeController : MonoBehaviour
{
    private LayerMask shootableLayer;
    private float explodeRadius = 5f;        // 爆炸半径
    private ParticleSystem explodeParticles; // 爆炸粒子特效

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
            if (currEnemyHealth != null)
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
                
            ////int damage = 20;
            //currEnemyHealth.TakeDamage(damage);

        }
        explodeParticles.transform.position = transform.position;
        explodeParticles.Play();
        Destroy(gameObject);
    }
}
