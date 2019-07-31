using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
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
            //Rigidbody currRigibody = colliders[i].GetComponent<Rigidbody>();
            if (!currEnemyHealth)
                continue;
            int damage = 20;
            currEnemyHealth.TakeDamage(damage);
            Debug.Log("shell");

        }
        explodeParticles.transform.position = transform.position;
        explodeParticles.Play();
        //Destroy(explodeParticles.gameObject, explodeParticles.main.duration);
        Destroy(gameObject);
    }
}
