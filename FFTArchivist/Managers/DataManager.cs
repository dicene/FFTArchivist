using FF16Tools.Pack;
using FFTArchivist.DataSources;
using FFTArchivist.Models;
using FFTArchivist.Models.Base;
using FFTArchivist.Properties;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace FFTArchivist.Managers
{
    public class DataManager : IDataManager
    {
        private static DataManager _instance = new DataManager();
        public static DataManager Instance { get => _instance; }
        public string FFTBasePath => Settings.Default.FFTIVCRootPath;
        public string FFTExecutablePath => Path.Combine([FFTBasePath, "fft_enhanced - 1.2.0.exe"]);
        public string DataFolderPath => Path.Combine([FFTBasePath, "data", "enhanced"]);
        public string Locale = "en";
        public string CodeName = FF16Tools.Pack.Crypto.PackKeyStore.FFT_IVALICE_CODENAME;

        public event EventHandler OnDataReloaded;

        public Dictionary<Type, IDataSource> DataSources { get; set; } = new();

        public FF16PackManager FF16PackManager { get; set; } = new();

        // TODO: At some point consider refactoring this to a custom class.
        public Dictionary<Type, IEnumerable<BaseModel>> DataLists { get; } = new();

        public async Task<bool> OpenPack(string packDirectory)
        {
            if (FF16PackManager != null)
            {
                await FF16PackManager.DisposeAsync();
            }

            FF16PackManager = new();
            FF16PackManager.Open(packDirectory, CodeName);

            return true;
        }

        public async Task<bool> ClosePack()
        {
            await FF16PackManager.DisposeAsync();
            FF16PackManager = null;

            return true;
        }

        public static List<Type> GetModelTypes()
        {
            var modelTypes = new List<Type>();
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.BaseType == typeof(BaseModel) && !type.IsAbstract))
            {
                modelTypes.Add(t);
            }

            foreach (var type in modelTypes)
            {
                Debug.WriteLine($"ModelTypes: {type}");
            }

            return modelTypes;
        }

        public async Task<bool> LoadData()
        {
            foreach (var pac in FF16PackManager.PackFiles)
            {
                Debug.WriteLine($"Loaded pac: {pac}");
            }

            var items = new List<Item>();

            for (int i = 0; i < 256; i++)
            {
                var item = new Item(i);
                item.SetOriginalSources(DataSources);
                await item.ReadData();
                items.Add(item);
            }

            var poaches = new List<PoachItem>();

            for (int i = 1; i < 97; i++)
            {
                var poach = new PoachItem(i);
                poach.SetOriginalSources(DataSources);
                await poach.ReadData();
                poaches.Add(poach);
            }

            var abilities = new List<Ability>();

            for (int i = 0; i < 512; i++)
            {
                var ability = new Ability(i);
                ability.SetOriginalSources(DataSources);
                await ability.ReadData();
                abilities.Add(ability);
            }

            var actionAbilities = new List<ActionAbility>();

            for (int i = 0; i < 368; i++)
            {
                var actionAbility = new ActionAbility(i);
                actionAbility.SetOriginalSources(DataSources);
                actionAbility.SetOriginalOverrideSources(DataSources);
                await actionAbility.ReadData();
                actionAbilities.Add(actionAbility);
            }

            var itemAbilities = new List<ItemAbility>();

            for (int i = 0; i < 14; i++)
            {
                var itemAbility = new ItemAbility(i);
                itemAbility.SetOriginalSources(DataSources);
                await itemAbility.ReadData();
                itemAbilities.Add(itemAbility);
            }

            var throwAbilities = new List<ThrowAbility>();

            for (int i = 0; i < 12; i++)
            {
                var throwAbility = new ThrowAbility(i);
                throwAbility.SetOriginalSources(DataSources);
                await throwAbility.ReadData();
                throwAbilities.Add(throwAbility);
            }

            var jumpAbilities = new List<JumpAbility>();

            for (int i = 0; i < 12; i++)
            {
                var jumpAbility = new JumpAbility(i);
                jumpAbility.SetOriginalSources(DataSources);
                await jumpAbility.ReadData();
                jumpAbilities.Add(jumpAbility);
            }

            var chargeAbilities = new List<ChargeAbility>();

            for (int i = 0; i < 8; i++)
            {
                var chargeAbility = new ChargeAbility(i);
                chargeAbility.SetOriginalSources(DataSources);
                await chargeAbility.ReadData();
                chargeAbilities.Add(chargeAbility);
            }

            var mathAbilities = new List<MathAbility>();

            for (int i = 0; i < 8; i++)
            {
                var mathAbility = new MathAbility(i);
                mathAbility.SetOriginalSources(DataSources);
                await mathAbility.ReadData();
                mathAbilities.Add(mathAbility);
            }

            var supportAbilities = new List<Models.SupportAbility>();

            for (int i = 0; i < 90; i++)
            {
                var supportAbility = new Models.SupportAbility(i);
                supportAbility.SetOriginalSources(DataSources);
                await supportAbility.ReadData();
                supportAbilities.Add(supportAbility);
            }

            var uis = new List<Models.UI>();

            for (int i = 0; i < 4031; i++)
            {
                var ui = new Models.UI(i);
                ui.SetOriginalSources(DataSources);
                await ui.ReadData();
                uis.Add(ui);
            }

            DataLists[typeof(Item)] = items;
            DataLists[typeof(PoachItem)] = poaches;
            DataLists[typeof(Ability)] = abilities;
            DataLists[typeof(ActionAbility)] = actionAbilities;
            DataLists[typeof(ItemAbility)] = itemAbilities;
            DataLists[typeof(ThrowAbility)] = throwAbilities;
            DataLists[typeof(JumpAbility)] = jumpAbilities;
            DataLists[typeof(ChargeAbility)] = chargeAbilities;
            DataLists[typeof(MathAbility)] = mathAbilities;
            DataLists[typeof(Models.SupportAbility)] = supportAbilities;
            DataLists[typeof(UI)] = uis;

            OnDataReloaded?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public async Task<bool> LoadDataSources()
        {
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes().Where(type => type.GetInterface("IDataSource") != null && !type.IsAbstract))
            {
                Debug.WriteLine($"Initializing new datasource: {t.Name}");
                if (Activator.CreateInstance(t) is IDataSource dataSource)
                {
                    DataSources.Add(t, dataSource);

                    if (dataSource is ANEXDataSource aNEXDataSource)
                    {
                        await aNEXDataSource.LoadPackSource(FF16PackManager);
                    }
                }
            }

            return true;
        }

        // TODO: Move data source loading and caching logic to their respective classes

        public List<T> GetDataList<T>() where T : BaseModel
        {
            if (DataLists.TryGetValue(typeof(T), out var list))
            {
                return list.Cast<T>().ToList();
            }

            return new List<T>();
        }

        public List<object> GetDataList(Type dataType)
        {
            if (DataLists.TryGetValue(dataType, out var list))
            {
                var returnList = new List<object>();
                returnList.AddRange(list);
                return returnList;
            }

            return new List<object>();
        }

        public IDataSource GetDataSource(Type type)
        {
            if (DataSources.TryGetValue(type, out var source))
            {
                return source;
            }

            return default;
        }
    }
}
