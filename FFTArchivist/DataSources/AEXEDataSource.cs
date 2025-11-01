using CommunityToolkit.HighPerformance;
using Reloaded.Memory.Sigscan;
using Syroot.BinaryData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vortice.Win32;

namespace FFTArchivist.DataSources
{
    public abstract class AEXEDataSource : IDataSource
    {
        public string Path { get; private set; }
        public long BaseOffset { get; private set; }
        public int Count { get; private set; }
        public int Size { get; private set; }

        private List<List<byte>> rows;

        public AEXEDataSource(string path, long baseOffset, int count, int size)
        {
            Path = path;
            BaseOffset = baseOffset;
            Count = count;
            Size = size;
        }

        public async Task<T> ReadData<T>(int id, int offset)
        {
            if (rows == null)
            {
                rows = new();

                var reader = new BinaryReader(File.OpenRead(Path));

                using (Stream stream = File.OpenRead(Path))
                {
                    stream.Seek(BaseOffset, SeekOrigin.Begin);

                    for (int i = 0; i < Count; i++)
                    {
                        rows.Add(stream.ReadBytes(Size).ToList());
                        //stream.Seek(Size, SeekOrigin.Current);
                    }
                }
            }

            if (rows.Count < id)
            {
                return default;
            }

            var rowBytes = rows[id][offset..(offset + Marshal.SizeOf(typeof(T)))];
            GCHandle handle = GCHandle.Alloc(rowBytes.ToArray(), GCHandleType.Pinned);
            T result = Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            handle.Free();
            return result;

            ////var scanner = new ScannerFactory().CreateScanner()

            //var a = new BinaryReader(File.OpenRead(path));

            //int size = Marshal.SizeOf(typeof(T));

            //using (Stream stream = File.OpenRead(path))
            //{
            //    stream.Seek((long)offset, SeekOrigin.Begin);

            //    BinaryReader reader = new BinaryReader(stream);
            //    var bytes = reader.ReadBytes(0x10);
            //    Debug.WriteLine($"EXEDataSource {offset:X}, count: {count} : {string.Join("  ", bytes.Select(b => $"{b:X}"))}");
            //}

            //Debug.WriteLine($"Attempting to read from EXEDataSource: {path}");
            //return default;
        }
    }
}
