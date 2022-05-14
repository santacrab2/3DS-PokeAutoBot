﻿using System;
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
        public static uint OfferedPokemonoff = 0x006A6DD4;
        public static uint finalofferscreenoff = 0x30192EEA;
        public static uint box1slot1 = 0x33015AB0;
        public static uint tradevolutionscreenoff = 0x3002040C;
        public static bool tradeevolution => Form1.ntr.ReadBytes(tradevolutionscreenoff, 1)[0] == 0x57;
        public static uint screenoff = 0x006A610A;
        public static bool onboxscreen => BitConverter.ToUInt16(Form1.ntr.ReadBytes(screenoff, 2)) == 0x4120;
        public static uint GTSpagesizeoff = 0x329921A4;
        public static uint GTSblockoff = 0x329927C4;
        public static uint GTScurrentview = 0x305CD9F4;
        public static uint coordinatesoff = 0x3182dbc4;
        public static uint initialfestcoords = 0x2300;
        public static bool infestivalplaza = BitConverter.ToUInt16(Form1.ntr.ReadBytes(coordinatesoff, 2)) == initialfestcoords;

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
        public int RequestedPoke => BitConverter.ToInt16(Data[0xC..]);
        public string trainername => Encoding.Unicode.GetString(Data.Slice(0x4c,24)).Trim('\0');
        public int genderindex => Data[0xE];
        public int levelindex => Data[0xF];


    }
}
