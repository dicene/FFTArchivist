using FFTArchivist.Models;
using System.ComponentModel;

namespace FFTArchivist.Entries
{
    internal class UIViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private UI ui;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UI UI
        {
            get
            {
                return ui;
            }

            set
            {
                if (ui == value)
                {
                    return;
                }

                ui = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UIViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unknown0)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        public int Id { get => ui.Id; set => ui.Id = value; }
        public int Unknown0 { get => ui?.Unknown0.Value ?? 0; set => ui.Unknown0.Value = value; }
        public string Text { get => ui?.Text.Value ?? ""; set => ui.Text.Value = value; }

        public UIViewModel()
        {
            this.ui = null;
        }

        public void ChangeIndex(int index)
        {
            UI = App.DataManager.GetDataList<UI>()[index];
        }
    }
}
