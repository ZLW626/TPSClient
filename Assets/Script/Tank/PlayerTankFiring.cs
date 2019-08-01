using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 坦克射击管理
public class PlayerTankFiring : MonoBehaviour
{
    [SerializeField] private Rigidbody shellRigibodyPrefab;
    [SerializeField] private Transform fireTransform;
    private PlayerTankController playerTankController;
    private float fireForce = 12f;
    private float cdTime = 3f;

    [SerializeField] private GameObject cdBar;
    private TankFireCDBarController tankFireCDBarController;
    private PlayerStatusBarController playerStatusBarController;
    // Start is called before the first frame update
    void Start()
    {
        playerStatusBarController = GameObject.Find("SelfPlayerStatus").GetComponent<PlayerStatusBarController>();
        playerTankController = GetComponent<PlayerTankController>();
        tankFireCDBarController = cdBar.GetComponent<TankFireCDBarController>();
        cdBar.SetActive(false);
    }


    // 坦克射击, 具有CD时间
    public void TankFire(ref float waitingTime)
    {
        if (waitingTime >= cdTime && playerStatusBarController.shell > 0)
        {
            Debug.Log("Fire2");
            Rigidbody shellRigidbody = Instantiate(shellRigibodyPrefab,
            fireTransform.position, fireTransform.rotation) as Rigidbody;

            shellRigidbody.velocity = fireTransform.forward * fireForce;
            waitingTime = 0f;
            cdBar.SetActive(true);

            tankFireCDBarController.slider.value = 0f;
            tankFireCDBarController.cdBarBkg.fillAmount = 0f;
            playerStatusBarController.UpdateShellText();
        }
            
    }
}
