using FFTArchivist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Entries
{
    internal class ENTDViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private ENTD entry;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ENTD Entry
        {
            get
            {
                return entry;
            }

            set
            {
                if (entry == value)
                {
                    return;
                }

                entry = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ENTDViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Entry)));
            }
        }

        public int Id { get => entry.Id; set => entry.Id = value; }
        public byte Unit1SpriteSet
        {
            get => entry.Units[0].Value.SpriteSet;
            set
            {
                if (entry?.Units[0]?.Value != null)
                {
                    entry.Units[0].Value.SpriteSet = value;
                }
            }
        }

        public ENTDViewModel()
        {
            //this.entry = new ENTD();
        }

        public ENTDViewModel(ENTD entry)
        {
            //this.entry = entry;
        }

        public void ChangeIndex(int index)
        {
            //ENTD = App.DataManager.GetDataList<ENTD>()[index];
        }
    }
}
