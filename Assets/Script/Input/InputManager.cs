using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerTankController playerTankController;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private PlayerTankFiring playerTankFiring;

    public bool isOnTank = false;
    private bool isOtherPlayerOnTank = false;
    private bool isJump;
    private float waitingTime = 0f; //坦克下次射击等待时间

    public bool enablePlayer = true;

    private bool grenadeMode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enablePlayer)
            return;
        //获取鼠标移动
        float hMouse = Input.GetAxis("Mouse X");
        float vMouse = Input.GetAxis("Mouse Y");

        //玩家和坦克的第三人称视角
        if(isOnTank)
        {
            playerTankController.CameraRotate(hMouse);
            playerTankController.TankTurretRotate();
        }
        else
        {
            playerController.CameraRotate(hMouse, vMouse);
        }

        //玩家和坦克的射击
        waitingTime += Time.deltaTime;
        playerAnimation.Shoot(false);
        if (Input.GetButtonDown("Fire1"))
        {
            if(isOnTank)
            {
                Debug.Log("Fire1");
                playerTankFiring.TankFire(ref waitingTime);
            }
            else
            {
                Debug.Log("Fire1");
                if (!grenadeMode)
                    playerShoot.Shoot();
                else
                    playerShoot.ThrowGrenade();
                
            }
        }

        //玩家换弹夹
        playerAnimation.Reload(false);
        if(Input.GetKeyDown(KeyCode.R))
        {
            playerShoot.ChangeClip();
        }

        //玩家换枪
        if(Input.GetKeyDown(KeyCode.E))
        {
            playerShoot.ChangeGun();
            playerAnimation.ChangeGun();
        }

        //玩家扔手雷
        if(Input.GetKeyDown(KeyCode.G))
        {
            grenadeMode = !grenadeMode;
        }

        //玩家上下坦克
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(isOnTank)
            {
                playerTankController.PlayerGetOffTank();
                isOnTank = false;
            }
            else
            {
                if(!isOtherPlayerOnTank)
                {
                    playerController.PlayerGetOnTank();
                    isOnTank = true;
                }
                
            }
        }

        if (Input.GetButtonDown("Jump") && !isJump)
        {
            //Debug.Log("Jump");
            isJump = true;
        }


        //锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if (!enablePlayer)
            return;

        //获取键盘输入
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if(isOnTank)
        {
            playerTankController.TankMove(h, v);
        }
        else
        {
            playerController.PlayerMove(h, v, isRunning, ref isJump);
            playerAnimation.Move(h, v, isRunning);
        }
    }
}
