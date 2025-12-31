using System.Diagnostics;

namespace FFTArchivist.ViewModels.DataItems
{
    internal interface RevertableDataItemViewModel
    {
        public string OriginalValueString { get; set; }
        public string ValueString { get; set; }
        public void Revert();
    }
}
