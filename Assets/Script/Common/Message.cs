using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

namespace Assets.Script.Common
{
    
    public class UniParam
    {
        public char type; //'s': str; 'i': int; 
        public string strVal;
        public int intVal;
        public short shortVal;
    }


    public class EnemyData
    {
        public int enemyID;
        public int enemyType;
        public int bornPointID;
    }

    // 客户端发送消息基类
    public class MsgCSBase
    {
        //private ArrayList paramters = new ArrayList();
        private List<UniParam> parameters = new List<UniParam>();
        protected MemoryStream memoryStream;
        protected BinaryWriter binaryWriter;
        protected short sid_cid;

        public MsgCSBase()
        {
            memoryStream = new MemoryStream();
            binaryWriter = new BinaryWriter(memoryStream);
        }
        protected void appendParamStr(string param)
        {
            UniParam uniParam = new UniParam();
            uniParam.type = 's';
            uniParam.strVal = param;

            parameters.Add(uniParam);
        }
 
        protected void appendParamInt(int param)
        {
            UniParam uniParam = new UniParam();
            uniParam.type = 'i';
            uniParam.intVal = param;
            parameters.Add(uniParam);
        }

        public byte[] Marshal()
        {
            int len = parameters.Count;
            for(int i = 0;i < len;++i)
            {
                switch(parameters[i].type)
                {
                    case 's':
                        binaryWriter.Write(Encoding.UTF8.GetBytes(parameters[i].strVal));
                        break;
                    case 'i':
                        binaryWriter.Write(parameters[i].intVal);
                        break;
                    default:
                        break;
                }
                if(i == 0)
                {
                    binaryWriter.Write(sid_cid);
                }

            }
            binaryWriter.Close();
            return memoryStream.ToArray();
        }
    }
    
    // 客户端发送的登录消息
    public class MsgCSLogin : MsgCSBase
    {
        public MsgCSLogin(string name, string password)
        {
            sid_cid = Conf.MSG_CS_LOGIN;
            int lenLenFlag = sizeof(int) * 2;
            int datalen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + name.Length + password.Length + lenLenFlag;

            appendParamInt(datalen);
            appendParamInt(name.Length);
            appendParamStr(name);
            appendParamInt(password.Length);
            appendParamStr(password);
        }

        //public byte[] Marshal()
        //{
        //    //byte[] bytes;
        //    //using (MemoryStream memoryStream = new MemoryStream())
        //    //MemoryStream memoryStream = new MemoryStream();
        //    //{
        //    //using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
        //    //BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
        //    //{
        //    //int datalen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + name.Length + password.Length;
        //    int lenLenflag = sizeof(int) * 2;
        //    int datalen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + name.Length + password.Length + lenLenflag;
        //    Debug.Log("datalen " + datalen);
        //    Debug.Log("lenLenflag " + lenLenflag);
        //    Debug.Log("name.Length " + name.Length);
        //    Debug.Log("password.Length " + password.Length);
        //    binaryWriter.Write(datalen);
        //    binaryWriter.Write(Conf.MSG_CS_LOGIN);
        //    binaryWriter.Write(name.Length);
        //    binaryWriter.Write(Encoding.UTF8.GetBytes(name));
        //    binaryWriter.Write(password.Length);
        //    binaryWriter.Write(Encoding.UTF8.GetBytes(password));
        //    binaryWriter.Close();
        //    Debug.Log("memoryStream.ToArray().Length " + memoryStream.ToArray().Length);
        //    //bytes = (byte[])memoryStream.ToArray().Clone();
        //    return memoryStream.ToArray();
        //    //}
        //    //}

        //    //return bytes;
        //}
    }

    // 客户端发送的注册消息
    public class MsgCSRegister:MsgCSBase
    {
        public MsgCSRegister(string name, string password)
        {
            sid_cid = Conf.MSG_CS_REGISTER;
            int lenLenFlag = sizeof(int) * 2;
            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + name.Length + password.Length + lenLenFlag;

            appendParamInt(dataLen);
            appendParamInt(name.Length);
            appendParamStr(name);
            appendParamInt(password.Length);
            appendParamStr(password);
        }

        //public byte[] Marshal()
        //{
        //    int lenLenflag = size
        //}
    }

    // 客户端请求敌人初始位置消息
    public class MsgCSAskForEnemies: MsgCSBase
    {
        public MsgCSAskForEnemies(int roundNum)
        {
            sid_cid = Conf.MSG_CS_ASK_FOR_ENEMY;
            
            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + sizeof(int);
            appendParamInt(dataLen);
            appendParamInt(roundNum);
        }
    }

    // 客户端请求一个敌人的位置
    public class MsgCSAskForEnemyPosition: MsgCSBase
    {
        public MsgCSAskForEnemyPosition(int enemyID)
        {
            sid_cid = Conf.MSG_CS_ASK_FOR_ENEMY_POSITION;

            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + sizeof(int);
            appendParamInt(dataLen);
            appendParamInt(enemyID);
        }
    }

    // 告知服务器敌人死亡----------------------
    public class MsgCSEnemyDeath: MsgCSBase
    {
        public MsgCSEnemyDeath(int enemyID)
        {
            sid_cid = Conf.MSG_CS_ENEMY_DEATH;
            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + sizeof(int);
            appendParamInt(dataLen);
            appendParamInt(enemyID);
        }
    }

    // 告知服务器敌人受到攻击
    public class MsgCSEnemyTakeDamage:MsgCSBase
    {
        public MsgCSEnemyTakeDamage(int enemyID)
        {
            sid_cid = Conf.MSG_CS_ENEMY_TAKE_DAMAGE;
            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + sizeof(int);
            appendParamInt(dataLen);
            appendParamInt(enemyID);
        }
    }

    // 发送游戏结束后的玩家状态
    public class MsgCSSavePlayer: MsgCSBase
    {
        public MsgCSSavePlayer(string name, 
            int hp, int money, int ammo, int grenade, int shell)
        {
            sid_cid = Conf.MSG_CS_SAVE_PLAYER;
            int lenLenFlag = sizeof(int) * 1;

            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE +
                 lenLenFlag + name.Length + sizeof(int) * 5;
            appendParamInt(dataLen);
            appendParamInt(name.Length);
            appendParamStr(name);
            appendParamInt(hp);
            appendParamInt(money);
            appendParamInt(ammo);
            appendParamInt(grenade);
            appendParamInt(shell);

        }
    }

    // 发送玩家位置
    public class MsgCSPlayerPosition: MsgCSBase
    {
        public MsgCSPlayerPosition(float x, float z)
        {
            string xStr = x.ToString();
            string zStr = z.ToString();
            sid_cid = Conf.MSG_CS_PLAYER_POSITION;
            int lenLenFlag = sizeof(int) * 2;
            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + xStr.Length + xStr.Length + lenLenFlag;
            appendParamInt(dataLen);
            appendParamInt(xStr.Length);
            appendParamStr(xStr);
            appendParamInt(zStr.Length);
            appendParamStr(zStr);
        }
    }

    // 请求其他玩家位置
    public class MsgCSAskForOtherPlayer: MsgCSBase
    {
        public MsgCSAskForOtherPlayer()
        {
            sid_cid = Conf.MSG_CS_ASK_FOR_OTHER_PLAYER;
            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + sizeof(int);
            appendParamInt(dataLen);
            appendParamInt(1);
        }
    }

    // 请求开始游戏
    public class MsgCSStartGame: MsgCSBase
    {
        public MsgCSStartGame()
        {
            sid_cid = Conf.MSG_CS_START_GAME;
            int dataLen = Conf.NET_HEAD_LENGTH_SIZE + Conf.NET_SID_CID_LENGTH_SIZE + sizeof(int);
            appendParamInt(dataLen);
            appendParamInt(1);
        }
    }

    // 客户端接收消息基类
    public class MsgSCBase
    {
        public int sid;
        public int cid;
        public short sid_cid;
        protected MemoryStream memoryStream;
        protected BinaryReader binaryReader;

        public MsgSCBase()
        {
            //memoryStream = new MemoryStream();
            //binaryReader = new BinaryReader(memoryStream);
        }
    }

    // 服务器发送的一般确认消息
    public class MsgSCConfirm : MsgSCBase
    {
        public int confirm;

        public MsgSCConfirm()
        {
            confirm = -1;
        }

        public MsgSCConfirm Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            //int dataLen = binaryReader.ReadInt32();
            sid_cid = binaryReader.ReadInt16();
            confirm = binaryReader.ReadInt32();
            return this;
        }
    }

    // 服务器发送的敌人初始位置消息
    public class MsgSCEnemyInitialize: MsgSCBase
    {
        public List<EnemyData> enemies;
        public int enemyNum;

        public MsgSCEnemyInitialize Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            sid_cid = binaryReader.ReadInt16();
            enemyNum = binaryReader.ReadInt32();
            enemies = new List<EnemyData>(enemyNum);
            int strLen = binaryReader.ReadInt32();
            //敌人信息被转化为字符串进行传输, 这里接收的是字符串

            string enemyDataStr = new string(binaryReader.ReadChars(strLen));
            string[] enemyDataList = Regex.Split(enemyDataStr, "#");
            Debug.Log(enemyDataList);

            for (int i = 0;i < enemyDataList.Length;)
            {
                EnemyData enemyData = new EnemyData();
                enemyData.enemyID = int.Parse(enemyDataList[i++]);
                enemyData.enemyType = int.Parse(enemyDataList[i++]);
                enemyData.bornPointID = int.Parse(enemyDataList[i++]);

                enemies.Add(enemyData);
            }
            return this;
        }
    }

    // 服务器发送的一个敌人的初始位置
    public class MsgSCEnemyPosition: MsgSCBase
    {
        public int enemy_id;
        public int x;
        public int z;

        public MsgSCEnemyPosition Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            sid_cid = binaryReader.ReadInt16();
            enemy_id = binaryReader.ReadInt32();
            x = binaryReader.ReadInt32();
            z = binaryReader.ReadInt32();

            return this;

        }
    }

    // 服务器广播玩家位置消息
    public class MsgSCBroadcastPlayerPosition: MsgSCBase
    {
        public string playerName;
        public float x;
        public float z;

        public MsgSCBroadcastPlayerPosition Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            short sid_cid = binaryReader.ReadInt16();
            int nameLen = binaryReader.ReadInt32();
            playerName = new string(binaryReader.ReadChars(nameLen));
            int xStrLen = binaryReader.ReadInt32();
            string xStr = new string(binaryReader.ReadChars(xStrLen));
            x = float.Parse(xStr);
            int zStrLen = binaryReader.ReadInt32();
            string zStr = new string(binaryReader.ReadChars(zStrLen));
            z = float.Parse(zStr);

            return this;
        }
    }

    // 接收服务器发来的其他玩家信息
    public class MsgSCOtherPlayer: MsgSCBase
    {
        public string otherPlayerName;
        public int hp;

        public MsgSCOtherPlayer Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            short sid_cid = binaryReader.ReadInt16();

            //otherPlayerNum = binaryReader.ReadInt32();
            //otherPlayerName = new List<string>(otherPlayerNum);
            int strLen = binaryReader.ReadInt32();
            otherPlayerName = new string(binaryReader.ReadChars(strLen));
            hp = binaryReader.ReadInt32();
            return this;
        }
    }

    // 接收服务器发来的登录确认信息
    public class MsgSCLoginConfirm :MsgSCBase
    {
        public int confirm; 
        public int hp;
        public int money;
        public int ammo;
        public int grenade;
        public int shell;

        public MsgSCLoginConfirm Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            short sid_cid = binaryReader.ReadInt16();
            confirm = binaryReader.ReadInt32();
            //Debug.Log("confirm: " + confirm);
            hp = binaryReader.ReadInt32();
            money = binaryReader.ReadInt32();
            ammo = binaryReader.ReadInt32();
            grenade = binaryReader.ReadInt32();
            shell = binaryReader.ReadInt32();
            //Debug.Log(hp);
            //Debug.Log(money);
            //Debug.Log(ammo);
            //Debug.Log(grenade);
            //Debug.Log(shell);
            return this;
        }
    }

    // 接收服务器发来的本回合结束消息
    public class MsgSCRoundEnd: MsgSCBase
    {
        public int round;

        public MsgSCRoundEnd Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            //int dataLen = binaryReader.ReadInt32();
            sid_cid = binaryReader.ReadInt16();
            round = binaryReader.ReadInt32();
            return this;
        }
    }

    // 接收服务器发来的敌人受伤消息
    public class MsgSCEnemyTakeDamage: MsgSCBase
    {
        public int enemyID;

        public MsgSCEnemyTakeDamage Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            //int dataLen = binaryReader.ReadInt32();
            sid_cid = binaryReader.ReadInt16();
            enemyID = binaryReader.ReadInt32();
            return this;
        }
    }

    // 接收服务器发来的大厅玩家信息
    public class MsgSCPlayerInfoInHall: MsgSCBase
    {
        public string name;
        public int hp;
        public int loginID;
        public MsgSCPlayerInfoInHall Unmarshal(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream);
            sid_cid = binaryReader.ReadInt16();
            int strLen = binaryReader.ReadInt32();
            name = new string(binaryReader.ReadChars(strLen));
            hp = binaryReader.ReadInt32();
            loginID = binaryReader.ReadInt32();

            return this;
        }
    }

    // 统一解析服务器发送的消息,根据协议号交给相应的消息接收类处理
    public class UnifromUnmarshal//可以改成单例模式
    {
        public MsgSCBase Unmarshal(byte[] bytes)
        {
            // 根据协议号分发消息进行解析
            MsgSCBase msgSCBase = null;
            short sid_cid = ConvertBytesToInt16(bytes);
            switch(sid_cid)
            {
                case 0x1006:
                    msgSCBase = new MsgSCPlayerInfoInHall().Unmarshal(bytes);
                    break;
                case 0x2001:
                    msgSCBase = new MsgSCConfirm().Unmarshal(bytes);
                    break;
                case 0x3002:
                    msgSCBase = new MsgSCEnemyInitialize().Unmarshal(bytes);
                    break;
                case 0x1003:
                    msgSCBase = new MsgSCLoginConfirm().Unmarshal(bytes);
                    break;
                case 0x3004:
                    msgSCBase = new MsgSCEnemyPosition().Unmarshal(bytes);
                    break;
                case 0x4004:
                    msgSCBase = new MsgSCOtherPlayer().Unmarshal(bytes);
                    break;
                case 0x5001:
                    msgSCBase = new MsgSCRoundEnd().Unmarshal(bytes);
                    break;
                case 0x3007:
                    msgSCBase = new MsgSCEnemyTakeDamage().Unmarshal(bytes);
                    break;
                default:
                    return null;
            }
            msgSCBase.sid = sid_cid >> 8;
            msgSCBase.cid = sid_cid & 0x00FF;
            return msgSCBase;
        }

        
        private short ConvertBytesToInt16(byte[] bytes)
        {
            return (short)(bytes[0] & 0xff | ((bytes[1] & 0xff) << 8));
        }
    }
}
