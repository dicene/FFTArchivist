using FFTArchivist.Managers;
using FFTArchivist.Models;
using FFTArchivist.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFTArchivist.Entries
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //public Poach Poach
        //{
        //    get
        //    {
        //        return poach;
        //    }

        //    set
        //    {
        //        if (poach == value)
        //        {
        //            return;
        //        }

        //        poach = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PoachViewModel)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Poach.Name)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Poach.Description)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Poach.ItemID)));
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemNames)));
        //    }
        //}

        //public int Id { get => poach.Id; set => poach.Id = value; }
        //public string Name { get => poach?.Name.Value ?? ""; set => poach.Name.Value = value; }
        //public string Description { get => poach?.Description.Value ?? ""; set => poach.Description.Value = value; }
        //public int ItemID { get => poach?.ItemID.Value ?? 0; set => poach.ItemID.Value = value; }
        //public List<string> ItemNames { get => (poach != null ? App.DataManager.GetDataList<Item>().Select(i => i.Name.Value.Replace("<icon=103>", "+")).ToList() : new List<string>()); }
        //public string ItemName { get => poach != null ? App.DataManager.GetDataList<Item>()[poach.ItemID.Value].Name.Value : ""; }

        //public string FFTIVCBasePath { get => {
        //        return Settings.Default.FFTIVCBasePath;
        //    }
        //    set => {
        //        Settings.Default.FFTIVCBasePath = value;
        //    } }
        public string FFTIVCRootPath
        {
            get => Settings.Default.FFTIVCRootPath;
            set
            {
                if (Settings.Default.FFTIVCRootPath != value)
                {
                    Settings.Default.FFTIVCRootPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FFTIVCRootPath)));
                }
            }
        }

        public string ModName
        {
            get => Settings.Default.ModName;
            set
            {
                if (Settings.Default.ModName != value)
                {
                    Settings.Default.ModName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModName)));
                }
            }
        }

        public string ReloadedIIModsPath
        {
            get => Settings.Default.ReloadedIIModsPath;
            set
            {
                if (Settings.Default.ReloadedIIModsPath != value)
                {
                    Settings.Default.ReloadedIIModsPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReloadedIIModsPath)));
                }
            }
        }

        public string ModId
        {
            get => Settings.Default.ModId;
            set
            {
                if (Settings.Default.ModId != value)
                {
                    Settings.Default.ModId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModId)));
                }
            }
        }

        public string ModAuthor
        {
            get => Settings.Default.ModAuthor;
            set
            {
                if (Settings.Default.ModAuthor != value)
                {
                    Settings.Default.ModAuthor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModAuthor)));
                }
            }
        }

        public string ModVersion
        {
            get => Settings.Default.ModVersion;
            set
            {
                if (Settings.Default.ModVersion != value)
                {
                    Settings.Default.ModVersion = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModVersion)));
                }
            }
        }

        public string ModDescription
        {
            get => Settings.Default.ModDescription;
            set
            {
                if (Settings.Default.ModDescription != value)
                {
                    Settings.Default.ModDescription = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModDescription)));
                }
            }
        }

        public SettingsViewModel()
        {

        }
    }
}
