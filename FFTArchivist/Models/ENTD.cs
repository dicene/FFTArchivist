using FFTArchivist.DataSources.EXE.TempInterfaces.ENTD;
using FFTArchivist.Models.Base;
using System.ComponentModel;

namespace FFTArchivist.Models
{
    public class ENTD : BaseModel, INotifyPropertyChanged
    {
        public ENTD() : base() { }
        public ENTD(int id) : base(id) { }

        public event PropertyChangedEventHandler? PropertyChanged;

        //[EXESourceMapping(typeof(ENTDEXESource), nameof(FFTArchivist.DataSources.EXE.TempInterfaces.ENTD.Units))]
        public List<DataItem<ENTD_EntryUnit>> Units { get; set; }

        //[NEXMapping(typeof(ItemNEXSource), nameof(ItemNEXModel.Name))]
        //public DataItem<string> Name { get; set; }

        //[NEXMapping(typeof(ItemNEXSource), nameof(ItemNEXModel.Description))]
        //public DataItem<string> Description { get; set; }

        //[EXESourceMapping(typeof(ItemEXESource), nameof(Item.Price))]
        //public DataItem<ushort> Price { get; set; }
    }
}
