using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FFTArchivist.Controls
{
    /// <summary>
    /// Interaction logic for TextBoxIntDataItemControl.xaml
    /// </summary>
    public partial class TextBoxIntDataItemControl : System.Windows.Controls.UserControl
    {
        public TextBoxIntDataItemControl()
        {
            InitializeComponent();
        }

        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.DataItems.TextBoxIntDataItemViewModel vm)
                vm.Revert();

            FieldText.Focus();
            FieldText.Select(FieldText.Text.Length, 0);
        }
    }
}
