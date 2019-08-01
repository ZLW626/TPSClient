using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerManagerPre : MonoBehaviour
{
    // 保存大厅中其他玩家的信息
    public List<string> otherPlayerNameList;
    public List<int> otherPlayerHpList;
    public List<int> otherPlayerLoginIDList;
    // 本客户端玩家的登录号
    public int loginIDMain;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        otherPlayerNameList = new List<string>();
        otherPlayerHpList = new List<int>();
        otherPlayerLoginIDList = new List<int>();
    }
}
