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
    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        money = PlayerPrefs.GetInt("money");
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
        
    }

    public void UpdateMoneyText()
    {
        moneyText.text = money.ToString();
    }
    public void UpdateAmmoText()
    {
        ammoText.text = currClip.ToString() + " \\ " + ammo.ToString();
    }

    public void SetGrenadeText(int val)
    {

    }
    public void SetShellText(int val)
    {

    }
    public void SetHpBar(int val)
    {
        slider.value = PlayerPrefs.GetInt("hp") / maxHpVal;
        hpImage.fillAmount = slider.value;
        hpText.text = val.ToString();
    }
}
