using System.Collections.Generic;
using System.Windows.Input;

namespace CustomScreensaver
{
    public class HotKeyCombination
    {
        public string Name { get; set; }
        public List<Key> Keys { get; set; }
        public ActionType Action { get; set; }
    }

}
