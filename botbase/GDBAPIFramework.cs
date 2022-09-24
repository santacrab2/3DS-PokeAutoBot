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
            _netStream.Socket.Send(Encoding.Unicode.GetBytes("PacketSize = 400; qXfer: features: read/write +; multiprocess +; QStartNoAckMode +"));
                return Task.CompletedTask;

               
            
        }

        public static Task GDBSendContinueCommand()
        {
            var c = "$C";
            var b = Encoding.Unicode.GetBytes(c);
            _netStream.Socket.Send(b);
            var buf = new byte[1024];
           // var a = _netStream.Socket.Receive(buf);
           // Form1.ChangeStatus(Encoding.Default.GetString(buf));
            c = $"$k";
            b = Encoding.Unicode.GetBytes(c);
            _netStream.Socket.Send(b);
           // a = _netStream.Socket.Receive(buf);
           // Form1.ChangeStatus(Encoding.Default.GetString(buf));
            //_netStream.Socket.Send(Encoding.BigEndianUnicode.GetBytes($"$A;{38}"));
           // a = _netStream.Socket.Receive(buf);

            return Task.CompletedTask;
        }   
    }
}
