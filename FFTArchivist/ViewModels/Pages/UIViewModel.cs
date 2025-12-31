using FFTArchivist.Models;
using System.ComponentModel;

namespace FFTArchivist.ViewModels.Pages
{
    internal class UIViewModel : BaseDataPageViewModel, INotifyPropertyChanged
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
            ui = null;
        }

        public void ChangeIndex(int index)
        {
            if (index < 0 || index >= App.DataManager.GetDataList<UI>().Count)
            {
                UI = new UI(0);
                return;
            }

            UI = App.DataManager.GetDataList<UI>()[index];
        }
    }
}
