using FFTArchivist.Managers;
using FFTArchivist.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Entries
{
    internal class PoachViewModel : BaseDataViewModel, INotifyPropertyChanged
    {
        private Poach poach;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Poach Poach
        {
            get
            {
                return poach;
            }

            set
            {
                if (poach == value)
                {
                    return;
                }

                poach = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PoachViewModel)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Poach.Name)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Poach.Description)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Poach.RewardID)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemName)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemNames)));
            }
        }

        public int Id { get => poach.Id; set => poach.Id = value; }
        public string Name { get => poach?.Name.Value ?? ""; set => poach.Name.Value = value; }
        public string Description { get => poach?.Description.Value ?? ""; set => poach.Description.Value = value; }
        public int RewardID { get => poach?.RewardID.Value ?? 0; set => poach.RewardID.Value = value; }
        public List<string> ItemNames { get => (poach != null ? App.DataManager.GetDataList<Item>().Select(i => (i.Name?.Value.Replace("<icon=103>", "+") ?? "N/A")).ToList() : new List<string>()); }
        public string ItemName { get => poach != null ? App.DataManager.GetDataList<Item>()[(poach.RewardID?.Value ?? 0)].Name.Value : ""; }

        public PoachViewModel()
        {
            this.poach = null;
        }

        public PoachViewModel(Poach poach)
        {
            this.poach = poach;
        }
    }
}
