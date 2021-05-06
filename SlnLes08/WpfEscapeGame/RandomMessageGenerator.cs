using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WpfEscapeGame.MainWindow;

namespace WpfEscapeGame
{
    public static class RandomMessageGenerator
    {
        public static string[] msg0 = { "No, don't think about this...", "You can't do this...", "Why would you even think about this..." };
        public static string[] msg1 = { "No, No, No...", "Stop, try to think about something else...", "Why don't you even try to think..." };
        public static string[] msg2 = { "Think about another solution...", "You can do it, just just no nevermind...", "Okey just forget it you will not escape this room..." };
        public enum MessageType { Message0, Message1, Message2 }
        public static string GetRandomMessage(MessageType t)
        {
            Random rnd = new Random();
            int a = rnd.Next(0,3);
            switch (t)
            {
                case MessageType.Message0:
                   string random =  msg0[a];
                    return random;
                   
                case MessageType.Message1:
                    random = msg1[a];
                        return random;

                default:
                    random = msg2[a];
                        return random;
            }
        }
    }
   
}
