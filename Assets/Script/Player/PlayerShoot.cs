using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Network;
using Assets.Script.Common;


// 玩家射击
public class PlayerShoot : MonoBehaviour
{
    // 射线检测
    private Ray shootRay;
    private RaycastHit hitPoint;
    private int shootableMask;
    private ParticleSystem bulletParticles;

    [SerializeField] private GameObject handGun;
    [SerializeField] private GameObject infantry;
    [SerializeField] public GameObject grenadeAim;
    [SerializeField] public GameObject shootAim;
    [SerializeField] private Rigidbody grenadeRigibodyPrefab;
    [SerializeField] private Transform grenadeTransform;
    private float throwForce = 8f; // 手榴弹初始速度

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
        grenadeAim.SetActive(false);
        shootAim = GameObject.Find("PlayerAimCanvas");
        shootAim.SetActive(true);
    }


    // 手枪和机枪
    public void Shoot()
    {
        // 弹夹里没有子弹,请先换弹夹
        if (playerStatusBarController.currClip <= 0)
            return;
        playerAnimation.Shoot(true);
        playerStatusBarController.currClip -= 1;
        playerStatusBarController.UpdateAmmoText();

        // 计算击中点
        shootRay.origin = playerCamera.transform.position;
        Vector3 shootDirScreen = new Vector3(
            Screen.width / 2f,
            Screen.height / 2f,
           playerCamera.farClipPlane);
        Vector3 shootDir = playerCamera.ScreenToWorldPoint(shootDirScreen);
        shootRay.direction = playerCamera.transform.forward;

        // 判断是否击中敌人
        if (Physics.Raycast(shootRay, out hitPoint, 200, shootableMask))
        {
            EnemyHealth enemyHealth =
                hitPoint.collider.GetComponent<EnemyHealth>();
            bulletParticles.transform.position = hitPoint.point;
            bulletParticles.Play();
            if (enemyHealth != null)
            {
                //发送玩家击中敌人的信息到服务器进行判断
                int damage = 10;
                EnemyController enemyController =
                    hitPoint.collider.GetComponent<EnemyController>();
                if(enemyController != null)
                {
                    MsgCSEnemyTakeDamage msg = 
                        new MsgCSEnemyTakeDamage(enemyController.enemyID);
                    byte[] dataToSend = msg.Marshal();
                    SocketClient.netStream.Write(dataToSend, 0, dataToSend.Length);
                }
            }
        }
    }

    // 手榴弹
    public void ThrowGrenade()
    {
        if (playerStatusBarController.grenade <= 0)
            return;

        Rigidbody grenadeRigidbody = Instantiate(grenadeRigibodyPrefab,
           grenadeTransform.position, grenadeTransform.rotation) as Rigidbody;

        grenadeRigidbody.velocity = grenadeTransform.forward * throwForce;
        playerStatusBarController.UpdateGrenadeText();
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

    // 换弹夹
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
