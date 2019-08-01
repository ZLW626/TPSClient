using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBarController : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text grenadeText;
    [SerializeField] private Text shellText;
    [SerializeField] private Slider slider;
    [SerializeField] private Image hpImage;
    [SerializeField] private Text hpText;
    private float maxHpVal = 100;
    public int clipStorage = 20;

    public int money;
    public int ammo;
    public int currClip;
    public int grenade;
    public int shell;
    private int hp;

    // Start is called before the first frame update
    void Start()
    {
        money = PlayerPrefs.GetInt("money");
        Debug.Log(money);
        moneyText.text = money.ToString();

        ammo = PlayerPrefs.GetInt("ammo"); // ammo + currClip = 总的子弹数
        currClip = ammo % clipStorage;
        ammoText.text = currClip.ToString() + " \\ " + ammo.ToString();

        grenade = PlayerPrefs.GetInt("grenade");
        grenadeText.text = grenade.ToString();

        shell = PlayerPrefs.GetInt("shell");
        shellText.text = shell.ToString();

        hp = PlayerPrefs.GetInt("hp");
        slider.value = hp / maxHpVal;
        hpImage.fillAmount = slider.value;
        hpText.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(money);
        //moneyText.text = Time.deltaTime.ToString();
    }

    public void UpdateMoneyText(int delta)
    {
        money+= delta;
        moneyText.text = "" + money.ToString();
        PlayerPrefs.SetInt("money", money);
    }

    public void UpdateAmmoText()
    {
        //moneyText.text = currClip.ToString() + " \\ " + ammo.ToString();
        ammoText.text = currClip.ToString() + " \\ " + ammo.ToString();
        PlayerPrefs.SetInt("ammo", ammo + currClip);
    }

    public void UpdateGrenadeText()
    {
        grenade--;
        grenadeText.text = "" + grenade.ToString();
        PlayerPrefs.SetInt("grenade", grenade);
    }

    public void UpdateShellText()
    {
        shell--;
        shellText.text = "" + shell.ToString();
        PlayerPrefs.SetInt("shell", shell);
    }

    public void SetHpBar(int val)
    {
        slider.value = PlayerPrefs.GetInt("hp") / maxHpVal;
        hpImage.fillAmount = slider.value;
        hpText.text = val.ToString();

        PlayerPrefs.SetInt("hp", hp);
    }

}
