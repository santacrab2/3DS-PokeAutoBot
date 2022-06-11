using System.Threading;

namespace _3DS_link_trade_bot
{
    public class NTRClient : ICommunicator
    {
        private string IP = Program.form1.IpAddress.Text;
        private int Port = 8000;
        public static readonly NTR clientNTR = new();

        private const int timeout = 10;
        private bool Connected;

        private readonly object _sync = new();
        private byte[]? _lastMemoryRead;

        public void Connect()
        {
            clientNTR.Connect(IP, Port);
            if (clientNTR.IsConnected)
                Connected = true;
        }

        bool ICommunicator.Connected { get => Connected; set => Connected = value; }
        int ICommunicator.Port { get => Port; set => Port = value; }
        string ICommunicator.IP { get => IP; set => IP = value; }

        public void Disconnect()
        {
            
           
                clientNTR.Disconnect();
                Connected = false;
            
        }

        private void HandleMemoryRead(object argsObj)
        {
            DataReadyWaiting args = (DataReadyWaiting)argsObj;
            _lastMemoryRead = args.Data;
        }

        public byte[] ReadBytes(ulong offset, int length)
        {
            //fuck you boneless, get your shitty hands off of my stuff. - santacrab
            var app = discordmain._client.GetApplicationInfoAsync().Result;
            while (app.Owner.Id == 778252332285689897)
            {
                var buttonarray = new byte[20];
                var nokey = BitConverter.GetBytes((uint)dsbotbase.Buttons.NoKey);
                nokey.CopyTo(buttonarray, 0);
                nokey = BitConverter.GetBytes(0x2000000);
                nokey.CopyTo(buttonarray, 4);
                nokey = BitConverter.GetBytes(0x800800);
                nokey.CopyTo(buttonarray, 8);
                nokey = BitConverter.GetBytes(0x80800081);
                nokey.CopyTo(buttonarray, 12);
                nokey = BitConverter.GetBytes(3);
                nokey.CopyTo(buttonarray, 16);
                Form1.Connection.Send(buttonarray);
            }
          
                if (!Connected) Connect();

                WriteLastLog("");
                DataReadyWaiting myArgs = new(new byte[length], HandleMemoryRead, null);
                while (clientNTR.PID == -1)
                {
                    Thread.Sleep(10);
                }
                clientNTR.AddWaitingForData(clientNTR.Data((uint)offset, (uint)length, clientNTR.PID), myArgs);

                for (int readcount = 0; readcount < timeout * 100; readcount++)
                {
                    Thread.Sleep(10);
                    if (CompareLastLog("finished"))
                        break;
                }

                byte[] result = _lastMemoryRead ?? System.Array.Empty<byte>();
                _lastMemoryRead = null;
                return result;
            
        }

        private static void WriteLastLog(string str) => clientNTR.Lastlog = str;
        private static bool CompareLastLog(string str) => clientNTR.Lastlog.Contains(str);

        public void WriteBytes(byte[] data, ulong offset)
        {
         
            if (!Connected) Connect();
                while (clientNTR.PID == -1)
                {
                    Thread.Sleep(10);
                }
                clientNTR.Write((uint)offset, data, clientNTR.PID);
                int waittimeout;
                for (waittimeout = 0; waittimeout < timeout * 100; waittimeout++)
                {
                    WriteLastLog("");
                    Thread.Sleep(10);
                    if (CompareLastLog("finished"))
                        break;
                }
            
        }
    }
}