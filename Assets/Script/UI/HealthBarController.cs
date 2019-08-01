using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 一个通用的血条
public class HealthBarController : MonoBehaviour
{
    private int maxHpVal = 100;
    private int currHpVal;
    public Slider slider;
    public Image hpBarBkg;
    public Text hpValText;
    public Text otherPlayerName;

    private InputManager inputManager;
    private GameObject playerCamera;
    private GameObject tankCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        currHpVal = 100;
        slider.value = 1f;
        hpBarBkg.fillAmount = slider.value;
        hpValText.text = "HP: " + maxHpVal;

        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        playerCamera = GameObject.Find("Main Camera");
        tankCamera = GameObject.Find("TankCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.isOnTank)//坦克模式
            transform.rotation = tankCamera.transform.rotation;
        else
            transform.rotation = playerCamera.transform.rotation;
    }

    public void UpdateValue(int delta)
    {
        Debug.Log(currHpVal);
        Debug.Log(delta);
        currHpVal -= delta;
        if (currHpVal > maxHpVal)
            currHpVal = maxHpVal;
        else if (currHpVal < 0)
            currHpVal = 0;
        slider.value = currHpVal / (float)maxHpVal;
        hpBarBkg.fillAmount = slider.value;
        hpValText.text = "HP: " + currHpVal.ToString();
    }

    public void SetHpValue(int val)
    {
        currHpVal = val;
        if (currHpVal > maxHpVal)
            currHpVal = maxHpVal;
        else if (currHpVal < 0)
            currHpVal = 0;
        slider.value = currHpVal / (float)maxHpVal;
        hpBarBkg.fillAmount = slider.value;
        hpValText.text = "HP: " + currHpVal.ToString();
    }
}
