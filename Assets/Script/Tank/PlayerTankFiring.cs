using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankFiring : MonoBehaviour
{
    [SerializeField] private Rigidbody shellRigibodyPrefab;
    [SerializeField] private Transform fireTransform;
    private PlayerTankController playerTankController;
    private float fireForce = 12f;
    private float cdTime = 3f;
    //private float waitingTime = 0f;

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

    // Update is called once per frame
    void Update()
    {    
        //TankFire();
    }

    public void TankFire(ref float waitingTime)
    {
        //waitingTime += Time.deltaTime;
        //Debug.Log("waiting :" + waitingTime);
        //if(waitingTime >= cdTime)
        //{
        //    cdBar.SetActive(false);
        //}
        //if (Input.GetButtonDown("Fire1") && playerTankController.hasPlayer && waitingTime >= cdTime)
        if (waitingTime >= cdTime && playerStatusBarController.shell > 0)
        {
            Debug.Log("Fire2");
            Rigidbody shellRigidbody = Instantiate(shellRigibodyPrefab,
            fireTransform.position, fireTransform.rotation) as Rigidbody;

            shellRigidbody.velocity = fireTransform.forward * fireForce;
            waitingTime = 0f;
            cdBar.SetActive(true);
            //float v1 = tankFireCDBarController.slider.value;
            //float v2 = tankFireCDBarController.cdBarBkg.fillAmount;
            //Debug.Log("slider :" + v1);
            //Debug.Log("cdBarBkg :" + v2);
            tankFireCDBarController.slider.value = 0f;
            tankFireCDBarController.cdBarBkg.fillAmount = 0f;
            playerStatusBarController.UpdateShellText();
        }
            
    }
}
