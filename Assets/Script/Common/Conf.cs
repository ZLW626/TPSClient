using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Common
{
    public class Conf
    {
        
        public static short MSG_CS_LOGIN = 0x1001;                    // 玩家登陆消息
        public static short MSG_CS_REGISTER = 0x1002;                 // 玩家注册消息
        public static short MSG_SC_LOGIN_CONFIRM = 0x1003;            // 服务器返回登陆确认消息
        public static short MSG_CS_START_GAME = 0x1004;               // 请求服务器开始游戏的消息
        public static short MSG_CS_SAVE_PLAYER = 0x1005;              // 发送玩家信息保存到服务器数据库
        public static short MSG_SC_PLAYER_INFO_IN_HALL = 0x1006;      // 服务器返回大厅中的玩家消息

        public static short MSG_SC_CONFIRM = 0x2001;  // 一般的确认消息

        public static short MSG_SC_ENEMY_INITIALIZE = 0x3002;          // 服务器返回的敌人初始信息
        public static short MSG_CS_ASK_FOR_ENEMY = 0x3001;             // 向服务器请求敌人信息
        public static short MSG_CS_ASK_FOR_ENEMY_POSITION = 0x3003;    // 向服务器请求一个敌人的位置
        public static short MSG_SC_ENEMY_POSITION = 0x3004;            // 服务器返回一个敌人的位置
        public static short MSG_CS_ENEMY_DEATH = 0x3005;               // 告知服务器敌人死亡
        public static short MSG_CS_ENEMY_TAKE_DAMAGE = 0x3006;         // 告知服务器敌人受伤
        public static short MSG_SC_ENEMY_TAKE_DAMAGE = 0x3007;         // 告知客户端敌人受伤

        public static short MSG_CS_PLAYER_POSITION = 0x4001;           // 向服务器发送玩家位置
        public static short MSG_SC_BROADCAST_PLAYER_POSITION = 0x4002; // 服务器广播玩家位置
        public static short MSG_CS_ASK_FOR_OTHER_PLAYER = 0x4003;      // 请求其他玩家位置
        public static short MSG_SC_OTHER_PLAYER = 0x4004;              // 服务器返回其他玩家位置

        public static short MSG_SC_ROUND_END = 0x5001;    // 本回合结束

        public static int NET_HEAD_LENGTH_SIZE = 4;       // 消息头部：消息总长
        public static int NET_SID_CID_LENGTH_SIZE = 2;    // 消息头部：协议号
    }
}
