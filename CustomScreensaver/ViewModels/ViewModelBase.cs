using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomScreensaver
{
    public class ViewModelBase
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string v = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}