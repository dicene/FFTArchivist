using FFTArchivist.ViewModels.DataItems;
using System.Diagnostics;
using System.Windows;

namespace FFTArchivist.Controls.TextBox
{
    /// <summary>
    /// Interaction logic for TextBoxIntDataItemControl.xaml
    /// </summary>
    public partial class TextBoxIntDataItemControl : System.Windows.Controls.UserControl
    {
        //public string Label
        //{
        //    get { return (string)GetValue(LabelProperty); }
        //    set
        //    {
        //        SetValue(LabelProperty, value);
        //        DataItemViewModel.Label = value;
        //    }
        //}

        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
        nameof(MaxLength),
        typeof(int),
        typeof(TextBoxIntDataItemControl),
        new PropertyMetadata( 100, OnMaxLengthChanged ),
        ValidateMaxLength );

        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, value);
        }


        private static void OnMaxLengthChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxIntDataItemControl) d;
            Debug.WriteLine($"MaxLengthChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue}");
            //Debug.WriteLine($"MaxLengthChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue} {d.GetValue(MaxLengthProperty)}");
        }

        private static bool ValidateMaxLength(object value) => value is > 0;

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(TextBoxIntDataItemControl), new PropertyMetadata("missingLabel"));

        public bool DisplayAsHex
        {
            get { return (bool)GetValue(DisplayAsHexProperty); }
            set
            {
                SetValue(DisplayAsHexProperty, value);
                var context = DataContext as IntDataItemViewModel;
                context.DisplayAsHex = value;
                //DataItemViewModel.DisplayAsHex = value;
            }
        }

        public static readonly DependencyProperty DisplayAsHexProperty =
            DependencyProperty.Register("DisplayAsHex", typeof(bool), typeof(TextBoxIntDataItemControl), new PropertyMetadata(false, OnDisplayAsHexChanged));

        private static void OnDisplayAsHexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxIntDataItemControl) d;
            Debug.WriteLine($"OnDisplayAsHexChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue}");
            //var context = control.DataContext as IntDataItemViewModel;
            //context.DisplayAsHex = (bool)e.NewValue;
            //control.DataItemViewModel.DisplayAsHex = (bool)e.NewValue;
            //Debug.WriteLine($"MaxLengthChanged: {e.Property.Name}: {e.OldValue} -> {e.NewValue} {d.GetValue(MaxLengthProperty)}");
        }

        public TextBoxIntDataItemControl()
        {
            Debug.WriteLine($"Constructing new {nameof(TextBoxIntDataItemControl)}");
            InitializeComponent();
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
