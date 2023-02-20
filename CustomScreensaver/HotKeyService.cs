using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomScreensaver
{
    internal class HotKeyService
    {
        private bool _started;

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int i);

        public List<Key> KeysPressed { get; private set; } = new List<Key>();

        public int TimeOut { get; set; } = 200;

        public List<Tuple<List<Key>, Action>> Cimbinations { get; private set; } = new List<Tuple<List<Key>, Action>>();
        public bool Started { get => _started; }

        public void SetCombination(List<Key> keys, Action action)
        {
            Cimbinations.Add(new Tuple<List<Key>, Action>(keys, action));
        }

        public void Start()
        {
            _started = true;
            Update();
        }
        public void Stop()
        {
            _started = false;
        }

        private void Update()
        {
            Task.Factory.StartNew(() =>
            {
                while (_started)
                {
                    Thread.Sleep(10);
                    for (int i = 0; i < 255; i++)
                    {
                        var key = KeyInterop.KeyFromVirtualKey(i);
                        KeyState state = GetKeyState(i);

                        if (state != KeyState.Unknown)
                        {
                            if (KeysPressed.Contains(key) == false)
                            {
                                KeysPressed.Add(key);
                            }
                        }
                        else
                        {
                            if (KeysPressed.Contains(key) == true)
                            {
                                KeysPressed.Remove(key);
                            }
                        }
                    }
                    if (KeysPressed.Count > 0)
                    {
                        foreach (var item in Cimbinations)
                        {
                            List<Key> list1 = item.Item1;
                            List<Key> list2 = KeysPressed;
                            List<Key> list3 = item.Item1.Intersect(KeysPressed).ToList();
                            if (list3.Count == item.Item1.Count)
                            {
                                item.Item2.Invoke();
                                Thread.Sleep(TimeOut);
                                break;
                            }
                        }
                    }
                }
            });
        }

        private KeyState GetKeyState(int i)
        {
            var state = GetAsyncKeyState(i);

            KeyState keyState;
            switch (state)
            {
                case 32769:
                    keyState = KeyState.A;
                    break;
                case 32768:
                    keyState = KeyState.B;
                    break;
                case 1:
                    keyState = KeyState.C;
                    break;
                default:
                    keyState = KeyState.Unknown;
                    break;
            }
            if (Enum.IsDefined(typeof(Key), i))
            {
                if (state != 0)
                {
                    return keyState;
                }
            }

            return KeyState.Unknown;
        }
    }
}
