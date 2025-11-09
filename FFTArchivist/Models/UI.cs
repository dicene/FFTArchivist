using FFTArchivist.DataSources.NEX.UI;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class UI(int id) : BaseModel(id), INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [NEXMapping(typeof(UINEXSource), nameof(UINEXModel.Unknown0))]
        public DataItem<int> Unknown0 { get; set; }

        [NEXMapping(typeof(UINEXSource), nameof(UINEXModel.Text))]
        public DataItem<string> Text { get; set; }
    }
}
