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
        public static async Task GDBConnect(string ip, int port)
        {
            try
            {
                _tcp = new TcpClient { NoDelay = true };
                _tcp.Connect(ip, port);

                _netStream = _tcp.GetStream();
                if (_tcp.Connected)
                    Form1.ChangeStatus("GDB Connected");

                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$QStartNoAckMode#b0"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$!#21"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$Hg0#df"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$qXfer:features:read:target.xml:0,3fb#46"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$qXfer:features:read:target.xml:3fb,3fb#11"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$?#3f"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$Hc-1#09"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$qC#b4"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$qAttached#8f"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$g#67"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$m4ec4ec4f,1#5c"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$m4ec4ec4f,1#5c"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$qTStatus#49"));
                await receivebufferandlog();
                return;
            }catch(Exception ex)
            {
                Form1.ChangeStatus(ex.Message);
            }

               
            
        }

        public static async Task receivebufferandlog()
        {
            try
            {
                var buf = new byte[1024];
                _netStream.Socket.Receive(buf);
                var text = Encoding.UTF8.GetString(buf);
                Form1.ChangeStatus(text);
            }
            catch(Exception ex)
            {
                Form1.ChangeStatus(ex.Message);
            }

        }
        public static async Task GDBSendContinueCommand()
        {
            try
            {
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$qTStatus#49"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$D#44"));
                await receivebufferandlog();
                _netStream.Socket.Send(Encoding.UTF8.GetBytes("$?#3f"));
                await receivebufferandlog();

                return;
            }
            catch (Exception ex)
            {
                Form1.ChangeStatus(ex.Message);
            }
        }   
    }
}
