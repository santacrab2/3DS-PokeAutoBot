using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using PKHeX.Core;
using PKHeX.Core.AutoMod;

namespace _3DS_link_trade_bot
{
    public class GDBAPIFramework
    {
        public static TcpClient _tcp;
        public static NetworkStream _netStream;
        public static Task GDBConnect(string ip, int port)
        {
            
                _tcp = new TcpClient { NoDelay = true };
                _tcp.Connect(ip, port);
               
                _netStream = _tcp.GetStream();
                if(_tcp.Connected)
                    Form1.ChangeStatus("GDB Connected");
                return Task.CompletedTask;

               
            
        }

        public static Task GDBSendContinueCommand()
        {
            var c = "$C";
            var b = Encoding.BigEndianUnicode.GetBytes(c);
            _netStream.Socket.Send(b);
            var buf = new byte[1024];
            var a = _netStream.Socket.Receive(buf);
            Form1.ChangeStatus(Encoding.Default.GetString(buf));
            c = $"$D;{38}";
            b = Encoding.BigEndianUnicode.GetBytes(c);
            _netStream.Socket.Send(b);
            a = _netStream.Socket.Receive(buf);
            Form1.ChangeStatus(Encoding.Default.GetString(buf));

            return Task.CompletedTask;
        }   
    }
}
