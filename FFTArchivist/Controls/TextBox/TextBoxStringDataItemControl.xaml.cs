using FFTArchivist.ViewModels;
using FFTArchivist.ViewModels.DataItems;
using System.Diagnostics;
using System.Windows;

namespace FFTArchivist.Controls.TextBox
{
    /// <summary>
    /// Interaction logic for TextBoxStringDataItemControl.xaml
    /// </summary>
    public partial class TextBoxStringDataItemControl : System.Windows.Controls.UserControl
    {
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }

            set
            {
                SetValue(LabelProperty, value);
                DataItemViewModel.Label = value;
            }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(TextBoxStringDataItemControl), new PropertyMetadata("missingLabel", OnLabelChanged));

        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
            nameof(MaxLength), typeof(int), typeof(TextBoxStringDataItemControl), new PropertyMetadata( 100, OnMaxLengthChanged ), ValidateMaxLength );

        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }


        private static void OnMaxLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStringDataItemControl) d;
            Debug.WriteLine($"MaxLengthChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue}");
            //Debug.WriteLine($"MaxLengthChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue} {d.GetValue(MaxLengthProperty)}");
        }

        private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStringDataItemControl) d;
            Debug.WriteLine($"LabelChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue}");
            
            //Debug.WriteLine($"MaxLengthChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue} {d.GetValue(MaxLengthProperty)}");
        }

        private static bool ValidateMaxLength(object value) => value is > 0;

        public TextBoxStringDataItemControl()
        {
            Debug.WriteLine($"Constructing new {nameof(TextBoxStringDataItemControl)}");
            InitializeComponent();
            var val = (string)GetValue(LabelProperty);
            //var val = (string)GetValue(LabelProperty);
            Debug.WriteLine($"Val: {val}");
        }

        private void RevertButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RevertableDataItemViewModel revertableDataItem)
                revertableDataItem.Revert();

            FieldText.Focus();
            FieldText.Select(FieldText.Text.Length, 0);
        }
    }
}
