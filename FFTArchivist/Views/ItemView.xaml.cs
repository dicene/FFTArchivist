using FFTArchivist.Controls.TextBox;
using FFTArchivist.ViewModels.Pages;
using System.Diagnostics;
using System.Windows.Controls;

namespace FFTArchivist
{
    /// <summary>
    /// Interaction logic for ItemView.xaml
    /// </summary>
    public partial class ItemView : Page
    {
        public ItemView()
        {
            Debug.WriteLine($"Constructing new {nameof(ItemView)}");
            InitializeComponent();
        }
    }
}
