using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //连射机枪
    //private float timeBetweenBulltets = 0.1f;
    //private float timer = 0f;

    private Ray shootRay;
    private RaycastHit hitPoint;
    private int shootableMask;
    private ParticleSystem bulletParticles;

    [SerializeField] private GameObject handGun;
    [SerializeField] private GameObject infantry;
    [SerializeField] private Rigidbody grenadeRigibodyPrefab;
    [SerializeField] private Transform grenadeTransform;
    private float throwForce = 8f;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerStatusBarController playerStatusBarController;
    [SerializeField] private PlayerAnimation playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        bulletParticles = 
            GameObject.Find("BulletParticles").GetComponent<ParticleSystem>();
        shootableMask = LayerMask.GetMask("Shootable");

        handGun.SetActive(true);
        infantry.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        //if(Input.GetButtonDown("Fire1"))
        //{
        //弹夹里没有子弹,请先换弹夹
        //Debug.Log(playerStatusBarController.currClip);
        if (playerStatusBarController.currClip <= 0)
            return;
        playerAnimation.Shoot(true);
        playerStatusBarController.currClip -= 1;
        playerStatusBarController.UpdateAmmoText();

        shootRay.origin = playerCamera.transform.position;
        Vector3 shootDirScreen = new Vector3(
            Screen.width / 2f,
            Screen.height / 2f,
           playerCamera.farClipPlane);
        Vector3 shootDir = playerCamera.ScreenToWorldPoint(shootDirScreen);
        shootRay.direction = playerCamera.transform.forward;

        if (Physics.Raycast(shootRay, out hitPoint, 200, shootableMask))
        {
            EnemyHealth enemyHealth =
                hitPoint.collider.GetComponent<EnemyHealth>();
            bulletParticles.transform.position = hitPoint.point;
            bulletParticles.Play();
            if (enemyHealth != null)
            {
                //发送玩家击中敌人的信息到服务器
                int damage = 10;
                //根据枪类型选择不同的伤害
                enemyHealth.TakeDamage(damage);
            }
        }

        //}
    }

    public void ThrowGrenade()
    {
        Debug.Log(playerStatusBarController.shell);
        //if (playerStatusBarController.shell <= 0)
        //    return;
        Debug.Log(playerStatusBarController.shell);
        Rigidbody grenadeRigidbody = Instantiate(grenadeRigibodyPrefab,
           grenadeTransform.position, grenadeTransform.rotation) as Rigidbody;

        grenadeRigidbody.velocity = grenadeTransform.forward * throwForce; 


    }

    public void ChangeGun()
    {
        if (handGun.activeSelf)
            handGun.SetActive(false);
        else
            handGun.SetActive(true);

        if (infantry.activeSelf)
            infantry.SetActive(false);
        else
            infantry.SetActive(true);
    }

    public void ChangeClip()
    {
        if (playerStatusBarController.currClip > 0 || playerStatusBarController.ammo <= 0)
            return;
        if(playerStatusBarController.ammo >= 
            playerStatusBarController.clipStorage)
        {
            playerAnimation.Reload(true);

            playerStatusBarController.ammo -= 
                playerStatusBarController.clipStorage;
            playerStatusBarController.currClip = 
                playerStatusBarController.clipStorage;

            playerStatusBarController.UpdateAmmoText();
        }
        else
        {
            playerAnimation.Reload(true);

            playerStatusBarController.currClip = playerStatusBarController.ammo;
            playerStatusBarController.ammo = 0;
            playerStatusBarController.UpdateAmmoText();
        }
    }
}
