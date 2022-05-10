using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Buffers.Binary;

namespace _3DS_link_trade_bot
{
    public class RAM
    {
        public static uint Friendslistoffset = 0x30011134;
        public static int FriendListSize = 0x8FC0;
        public static uint friendsize = 0x2E0;
        public static uint namestart = 0x18;
        public static uint isconnectedoff = 0x318C5A12;
        public static bool isconnected => Form1.ntr.ReadBytes(isconnectedoff, 1)[0] != 0x6C;

        public static uint FailedTradeoff = 0x3023E34C;
        public static bool failedtrade => Form1.ntr.ReadBytes(FailedTradeoff, 1)[0] == 0x64;

    }
    public readonly ref struct FriendList
    {
        public const int friendlistsize = 0x8FC0;
        public const int numofguests = 50;
        private readonly Span<byte> Data;
        public FriendList(Span<byte> data) => Data = data;
        public friend this[int index]=>new(Data.Slice(friend.friendsize*index,friend.friendsize));

    }
    public readonly ref struct friend
    {
        public const int friendsize = 0x2E0;
        private readonly Span<byte> Data;
        public friend(Span<byte> data) => Data = data;
        public byte first => Data[0];
        public byte[] test => new byte[12] {Data[24], Data[26], Data[28], Data[30],Data[32],Data[34],Data[36],Data[38],Data[40],Data[42],Data[44],Data[46] };
        public string friendname => Encoding.ASCII.GetString(test);
    }
}
