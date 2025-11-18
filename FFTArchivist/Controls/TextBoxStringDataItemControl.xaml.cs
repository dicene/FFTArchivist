using System.Windows;
using System.Windows.Input;

namespace FFTArchivist.Controls
{
    /// <summary>
    /// Interaction logic for TextBoxStringDataItemControl.xaml
    /// </summary>
    public partial class TextBoxStringDataItemControl : System.Windows.Controls.UserControl
    {
        public TextBoxStringDataItemControl()
        {
            InitializeComponent();
        }

        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.DataItems.TextBoxStringDataItemViewModel vm)
                vm.Revert();

            FieldText.Focus();
            FieldText.Select(FieldText.Text.Length, 0);
        }
    }
}
