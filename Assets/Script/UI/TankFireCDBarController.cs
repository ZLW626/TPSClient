using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 坦克CD UI控制
public class TankFireCDBarController : MonoBehaviour
{
    private float cdTime = 3f;
    public float waitingTime = 0f;
    public Slider slider;
    public Image cdBarBkg;


    private TankFireCDBarController tankFireCDBarController;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0f;
        cdBarBkg.fillAmount = 0;

        tankFireCDBarController =
            GameObject.Find("TankFireCDBar").
            GetComponent<TankFireCDBarController>();

    }

    // Update is called once per frame
    void Update()
    {
        waitingTime += Time.deltaTime;
        slider.value = waitingTime / cdTime;
        cdBarBkg.fillAmount = slider.value;
        if (waitingTime >= cdTime)
        {
            slider.value = 0f;
            waitingTime = 0f;
            gameObject.SetActive(false);
        }
            
    }
}
