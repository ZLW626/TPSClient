using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerManagerPre : MonoBehaviour
{
    //public Dictionary<string, int> otherPlayerDict;
    public List<string> otherPlayerNameList;
    public List<int> otherPlayerHpList;
    public List<int> otherPlayerLoginIDList;

    public int loginIDMain;
    private void Awake()
    {
        otherPlayerNameList = new List<string>();
        otherPlayerHpList = new List<int>();
        otherPlayerLoginIDList = new List<int>();
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
