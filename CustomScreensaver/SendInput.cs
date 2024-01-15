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
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);
        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static void StopSwitchWindows()
        {
            Task.Factory.StartNew(() =>
            {
                InputSimulator inputSimulator = new InputSimulator();
                for (int i = 0; i < 1; i++)
                {
                    inputSimulator.Keyboard.Sleep(50);
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);
                    inputSimulator.Keyboard.Sleep(50);
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.ESCAPE);
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
                    int width = 10;// ushort.MaxValue;
                    int height = 10;// ushort.MaxValue;

                    var mousePosition = GetMousePosition();
                    Debug.WriteLine("aaa");
                    Debug.WriteLine((-2 + (0.5 * Random.Next(0, 10))));
                    var x = mousePosition.X * 34.1328125 + (-3 + (0.5 * Random.Next(0, 100)));
                    var y = mousePosition.Y * 60.68055555555556 + (-3 + (0.5 * Random.Next(0, 100)));
                    //inputSimulator.Mouse.
                    //Control.MousePosition
                    //SystemParameters.mouse
                    inputSimulator.Mouse.MoveMouseTo(x, y);
                }
            });
        }
        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new Point(w32Mouse.X, w32Mouse.Y);
        }
    }
}
