using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Common
{
    public class Conf
    {
        public static short MSG_CS_LOGIN = 0x1001;
        public static short MSG_CS_REGISTER = 0x1002;
        public static short MSG_SC_LOGIN_CONFIRM = 0x1003;
        public static short MSG_CS_START_GAME = 0x1004;
        public static short MSG_CS_SAVE_PLAYER = 0x1005;
        public static short MSG_SC_PLAYER_INFO_IN_HALL = 0x1006;

        public static short MSG_SC_CONFIRM = 0x2001;

        public static short MSG_SC_ENEMY_INITIALIZE = 0x3002;
        public static short MSG_CS_ASK_FOR_ENEMY = 0x3001;
        public static short MSG_CS_ASK_FOR_ENEMY_POSITION = 0x3003;
        public static short MSG_SC_ENEMY_POSITION = 0x3004;
        public static short MSG_CS_ENEMY_DEATH = 0x3005;
        public static short MSG_CS_ENEMY_TAKE_DAMAGE = 0x3006;
        public static short MSG_SC_ENEMY_TAKE_DAMAGE = 0x3007;

        public static short MSG_CS_PLAYER_POSITION = 0x4001;
        public static short MSG_SC_BROADCAST_PLAYER_POSITION = 0x4002;
        public static short MSG_CS_ASK_FOR_OTHER_PLAYER = 0x4003;
        public static short MSG_SC_OTHER_PLAYER = 0x4004;

        public static short MSG_SC_ROUND_END = 0x5001;

        public static int NET_HEAD_LENGTH_SIZE = 4;
        public static int NET_SID_CID_LENGTH_SIZE = 2;
    }
}
