using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Buffers.Binary.BinaryPrimitives;

namespace _3DS_link_trade_bot
{
    public class RAM
    {
        //gen7
        public static uint Friendslistoffset = 0x30011134;
        public static int FriendListSize = 0x8FC0;
        public static uint friendsize = 0x2E0;
        public static uint namestart = 0x18;
        public static uint isconnectedoff = 0x318C5A12;
        public static uint L_inlocalwireless = 0x6c;
        public static bool isconnected => Form1.ntr.ReadBytes(isconnectedoff, 1)[0] != L_inlocalwireless;

        public static uint FailedTradeoff = 0x3023E34C;
        public static bool failedtrade => Form1.ntr.ReadBytes(FailedTradeoff, 1)[0] == 0x64;
        public static uint OfferedPokemonoff = 0x006A6DD4;
        public static uint finalofferscreenoff = 0x30192EEA;
        public static uint box1slot1 = 0x33015AB0;
        public static uint tradevolutionscreenoff = 0x3002040C;
        public static bool tradeevolution => Form1.ntr.ReadBytes(tradevolutionscreenoff, 1)[0] == 0x57;
        public static uint screenoff = 0x006A610A;
        public static uint boxscreen = 0x4120;
        public static uint start_seekscreen = 0x3F2B;
        public static bool onboxscreen => BitConverter.ToUInt16(Form1.ntr.ReadBytes(screenoff, 2)) == boxscreen;
        public static uint GTSpagesizeoff = 0x329921A4;
        public static uint GTSblockoff = 0x329927C4;
        public static uint GTScurrentview = 0x305CD9F4;
        public static uint GTSDeposit = 0x32992180;
        public static uint festscreenoff = 0x318CBFEC;
        public static int festscreendisplayed = 0x38;
        public static uint Userinvitedbotscreenoff = 0x31928D74;
        public static int userinvitedbotscreenval = 0x21;
        public static bool userinvitedbot => Form1.ntr.ReadBytes(Userinvitedbotscreenoff, 1)[0] == userinvitedbotscreenval;

        public static bool infestivalplaza => Form1.ntr.ReadBytes(festscreenoff, 1)[0] == festscreendisplayed;

        public static uint SoftBanOff = 0x32994352;
        public static int softbanscreen = 0x50;
        public static bool IsSoftBanned => Form1.ntr.ReadBytes(SoftBanOff, 1)[0] == softbanscreen;

        public static uint WTReceivingPokemon = 0x314FF4D1;
        public static uint WTTrainerMatch = 0x303987B4;
        public static uint HasEggOff = 0x3307B1E8;

        //gen6
        public static uint PSSFriendoff = 0x08C6FFDC;
        public static uint SelectedFriendoff = 0x0866048E;
        public static uint secondSelectedFriendoff = 0x0865EED6;
        public static uint PSSBlockSize = 0x4E30;
        public static int PSSDataSize = 0x4E20;
        public static uint pokemonwantedoff = 0x08334988;
        public static uint currentscreenoff = 0x08334988;
        //oras
        public static uint Party1Slot1 = 0x08CFB26C;
        public static uint AcceptedTradeScreenVal = 0x040054e0;
        public static uint DoMoreScreen = 0x040008d0;
        public static uint OverWorldScreenVal = 0x043229F0;
        public static uint GTSScreenVal = 0x407F720;
        public static uint BoxScreenVal = 0x4011170;
        public static uint AcceptScreenVal = 0x40a2b90;
        public static uint MenuScreenVal = 0x0434b1a0;
        public static uint finaltradebuttonoff = 0x08554B24;
        public static uint tradeanimationscreenoff = 0x084207DC;
        public static uint oncommunicatingscreenoff = 0x084207B0;
        public static uint GTSListBlockOff = 0x8C694F8;
        public static uint GTSPageSize = 0x08C6D69C;
        public static uint GTSPageIndex = 0x08C6945C;
        public static uint GTSCurrentView6 = 0x08C6D6AC;
        public static uint UserInvitedBotOff6 = 0x15A57A00;
        public static uint WTTrainerMatch6 = 0x0824EFC8;
        public static uint WTReceivingPokemon6 = 0X0824EDD4;
       
        public static bool ontradeanimationscreen => Form1.ntr.ReadBytes(tradeanimationscreenoff, 1)[0] == 0x48;
        
        public static bool oncommunicatingscreen => Form1.ntr.ReadBytes(oncommunicatingscreenoff, 1)[0] == 0x11;
        public static bool TradeButtonOnScreen => Form1.ntr.ReadBytes(finaltradebuttonoff,1)[0] != 0;
        public static bool isconnected6 => Form1.ntr.ReadBytes(isconnectedoff, 1)[0] == 1;
        public static bool userinvitedbot6 => Form1.ntr.ReadBytes(UserInvitedBotOff6, 1)[0] == 0xAC;
        public static bool checkscreen(uint CurrentScreen,uint screenval)
        {
            var screenread = BitConverter.ToUInt32(Form1.ntr.ReadBytes(CurrentScreen, 4));
            if (screenread == screenval)
                return true;
            else
                return false;
        }
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
      
        public string friendname => Encoding.Unicode.GetString(Data.Slice(24,24)).Trim('\0');
    }
    public readonly ref struct GTSPage
    {
        public const int GTSBlocksize = 0x6400;
        private readonly Span<byte> Data;
        public GTSPage(Span<byte> data) => Data = data;
        public GTSEntry this[int index] => new(Data.Slice(GTSEntry.GTSEntrySize * index, GTSEntry.GTSEntrySize));
    }
    public readonly ref struct GTSEntry
    {
        public const int GTSEntrySize = 0x100;
        private readonly Span<byte> Data;
        public GTSEntry(Span<byte> data) => Data = data;
        public ushort RequestedPoke => BitConverter.ToUInt16(Data[0xC..]);
        public string trainername => Encoding.Unicode.GetString(Data.Slice(0x4c,24)).Trim('\0');
        public int genderindex => Data[0xE];
        public int levelindex => Data[0xF];
        public string Phrase => Encoding.Unicode.GetString(Data.Slice(0x5A, 30)).Trim('\0');

    }
    public readonly ref struct PSSfriendlist
    {

        private readonly Span<byte> Data;
        public PSSfriendlist(Span<byte> data) => Data = data;
        public PSSFriend this[int index] => new(Data.Slice(PSSFriend.friendsize*index,PSSFriend.friendsize));
        

    }
    public readonly ref struct PSSFriend
    {
        private readonly Span<byte> Data;
        public PSSFriend(Span<byte> data) => Data = data;
        public const int friendsize = 0xc8;
        public ulong pssID => ReadUInt64LittleEndian(Data);
        
        public string otname => Encoding.Unicode.GetString(Data.Slice(8, 24)).Trim('\0');
    }
    public readonly ref struct GTSPage6
    {
        public const int GTSBlocksize6 = 0x3E80;
        private readonly Span<byte> Data;
        public GTSPage6(Span<byte> data) => Data = data;
        public GTSEntry6 this[int index] => new(Data.Slice(GTSEntry6.GTSEntrySize6 * index, GTSEntry6.GTSEntrySize6));
    }
    public readonly ref struct GTSEntry6
    {
        public const int GTSEntrySize6 = 0xA0;
        private readonly Span<byte> Data;
        public GTSEntry6(Span<byte> data) => Data = data;
        public ushort RequestedPoke => BitConverter.ToUInt16(Data[0x0..]);
        public uint RequestedGender => Data[0x2];
        public int RequestLevel=>Data[0x3];
        public string trainername => Encoding.Unicode.GetString(Data.Slice(0x40, 24)).Trim('\0');
        public string GTSmsg => Encoding.Unicode.GetString(Data.Slice(0x5A, 24)).Trim('\0');
    }
}
