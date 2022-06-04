using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _3DS_link_trade_bot.Program;
using static _3DS_link_trade_bot.dsbotbase.Buttons;


namespace _3DS_link_trade_bot
{
    
    public class dsbotbase
    {

        
        public enum Buttons : uint
        {
           NoKey=0xFFF,
           A=0xFFE,
           B=0xFFD,
           X=0xBFF,
           Y= 0x7FF,
           R= 0xEFF,
           L= 0xDFF,
           Start= 0xFF7,
           Select= 0xFFB,
           DpadUP = 0xFBF,
           DpadDOWN = 0xF7F,
           DpadLEFT = 0xFDF,
           DpadRIGHT = 0xFEF,
           runUP = 0xFBD,
           runDOWN = 0xF7D,
           runLEFT = 0xFDD,
           runRIGHT = 0xFED,


        }
        public enum numpadx : int
        {
            one = 80,
            two = 160,
            three = 250,
            four = 80,
            five = 160,
            six = 250,
            seven = 80,
            eight = 160,
            nine = 250,
            zero = 160,

        }
        public enum numbpady : int
        {
            one = 70,
            two = 70,
            three = 70,
            four = 120,
            five = 120,
            six = 120,
            seven = 160,
            eight = 160,
            nine = 160,
            zero = 180,
        }
        public static async Task DpadClick(Buttons key,double delayinseconds)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)key);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            await Task.Delay(50);
            buttonarray = new byte[20];
            nokey = BitConverter.GetBytes(0xFFF);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            var delay = delayinseconds * 1000;
            await Task.Delay((int)delay);
        }
        public static async Task click(Buttons key,double delayinseconds)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)key);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            await Task.Delay(250);
            buttonarray = new byte[20];
            nokey = BitConverter.GetBytes(0xFFF);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            var delay = delayinseconds * 1000;
            await Task.Delay((int)delay);
        }

        public static async Task touch(decimal x,decimal y,double delayinseconds)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)NoKey);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(gethexcoord(x,y));
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            await Task.Delay(200);
            buttonarray = new byte[20];
            nokey = BitConverter.GetBytes(0xFFF);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            var delay = delayinseconds * 1000;
            await Task.Delay((int)delay);
        }
        public static async Task presshome(double delayinseconds)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)NoKey);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(1);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            await Task.Delay(500);
            buttonarray = new byte[20];
            nokey = BitConverter.GetBytes(0xFFF);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            var delay = delayinseconds * 1000;
            await Task.Delay((int)delay);
        }
        public static async Task PressPower(double delayinseconds)
        {
            var buttonarray = new byte[20];
            var nokey = BitConverter.GetBytes((uint)NoKey);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(2);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            await Task.Delay(500);
            buttonarray = new byte[20];
            nokey = BitConverter.GetBytes(0xFFF);
            nokey.CopyTo(buttonarray, 0);
            nokey = BitConverter.GetBytes(0x2000000);
            nokey.CopyTo(buttonarray, 4);
            nokey = BitConverter.GetBytes(0x800800);
            nokey.CopyTo(buttonarray, 8);
            nokey = BitConverter.GetBytes(0x80800081);
            nokey.CopyTo(buttonarray, 12);
            nokey = BitConverter.GetBytes(0);
            nokey.CopyTo(buttonarray, 16);
            Form1.Connection.Send(buttonarray);
            var delay = delayinseconds * 1000;
            await Task.Delay((int)delay);
        }

        public static uint gethexcoord(decimal Xvalue, decimal Yvalue)
        {
            uint hexX = Convert.ToUInt32(Math.Round(Xvalue * 0xFFF / 319));
            uint hexY = Convert.ToUInt32(Math.Round(Yvalue * 0xFFF / 239));
            return 0x01000000 + hexY * 0x1000 + hexX;
        }

       
        public static async Task enterfriendcode(string fc)
        {
            for (int i = 0; i < fc.Length; i++)
            {
                numpadx numberkeyx = numpadx.one;
                numbpady numberkeyy = numbpady.one;
                switch (fc[i])
                {
                    case '1': numberkeyx = numpadx.one; break;
                    case '2': numberkeyx = numpadx.two; break;
                    case '3': numberkeyx = numpadx.three; break;
                    case '4': numberkeyx = numpadx.four; break;
                    case '5': numberkeyx = numpadx.five; break;
                    case '6': numberkeyx = numpadx.six; break;
                    case '7': numberkeyx = numpadx.seven; break;
                    case '8': numberkeyx = numpadx.eight; break;
                    case '9': numberkeyx = numpadx.nine; break;
                    case '0': numberkeyx = numpadx.zero; break;
                    default: break;

                }
                switch (fc[i])
                {
                    case '1': numberkeyy = numbpady.one; break;
                    case '2': numberkeyy = numbpady.two; break;
                    case '3': numberkeyy = numbpady.three; break;
                    case '4': numberkeyy = numbpady.four; break;
                    case '5': numberkeyy = numbpady.five; break;
                    case '6': numberkeyy = numbpady.six; break;
                    case '7': numberkeyy = numbpady.seven; break;
                    case '8': numberkeyy = numbpady.eight; break;
                    case '9': numberkeyy = numbpady.nine; break;
                    case '0': numberkeyy = numbpady.zero; break;
                    default: break;

                }
                await touch((decimal)(int)numberkeyx, (decimal)(int)numberkeyy,0.8);
               
            }
        }
    }

}
