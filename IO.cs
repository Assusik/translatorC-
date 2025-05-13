using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace translatorc_
{
    public class IO
    {

        [DllImport("user32.dll")]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)] public KEYBDINPUT ki;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        const int INPUT_KEYBOARD = 1;
        const ushort VK_CONTROL = 0x11;
        const ushort VK_V = 0x56;
        const uint KEYEVENTF_KEYUP = 0x0002;

        public static void SendCtrlV()
        {
            INPUT[] inputs = new INPUT[4];

            // Нажать Ctrl
            inputs[0] = CreateKeyInput(VK_CONTROL, 0);
            // Нажать V
            inputs[1] = CreateKeyInput(VK_V, 0);
            // Отпустить V
            inputs[2] = CreateKeyInput(VK_V, KEYEVENTF_KEYUP);
            // Отпустить Ctrl
            inputs[3] = CreateKeyInput(VK_CONTROL, KEYEVENTF_KEYUP);

            SendInput(4, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        static INPUT CreateKeyInput(ushort key, uint flags)
        {
            return new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = key,
                        wScan = 0,
                        dwFlags = flags,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };
        }
    }
}
