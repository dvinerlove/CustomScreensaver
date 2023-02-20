using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WindowsInput;
using WindowsInput.Native;

namespace CustomScreensaver
{
    public class SendInput
    {
        public static void StopSwitchWindows()
        {
            Task.Factory.StartNew(() =>
            {
                InputSimulator inputSimulator = new InputSimulator();
                for (int i = 0; i < 1; i++)
                {
                    inputSimulator.Keyboard.Sleep(50);
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);
                }
            });
        }

        public static void LockPC()
        {
            Task.Factory.StartNew(() =>
            {
                InputSimulator inputSimulator = new InputSimulator();

                inputSimulator.Keyboard.Sleep(200);
                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RWIN);
                inputSimulator.Keyboard.Sleep(200);
                inputSimulator.Keyboard.TextEntry("HEllo)");
                inputSimulator.Keyboard.Sleep(200);
                inputSimulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.LCONTROL, WindowsInput.Native.VirtualKeyCode.VK_A);
                inputSimulator.Keyboard.Sleep(200);
                inputSimulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.LCONTROL, WindowsInput.Native.VirtualKeyCode.VK_X);
                inputSimulator.Keyboard.Sleep(2000);
            });
        }

        public static void MoveMouseRandomly()
        {
            Task.Factory.StartNew(() =>
            {
                InputSimulator inputSimulator = new InputSimulator();
                Random Random = new Random();
                for (int i = 0; i < 5; i++)
                {
                    double width = SystemParameters.FullPrimaryScreenWidth;
                    double height = SystemParameters.PrimaryScreenHeight;
                    var x = (double)Random.Next(ushort.MaxValue);
                    var y = (double)Random.Next(ushort.MaxValue);
                    inputSimulator.Mouse.MoveMouseTo(x, y);
                }
            });
        }
    }
}
