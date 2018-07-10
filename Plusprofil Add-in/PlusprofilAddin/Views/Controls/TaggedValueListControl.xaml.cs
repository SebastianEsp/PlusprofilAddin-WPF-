using PlusprofilAddin.ViewModels.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PlusprofilAddin.ViewModels;
using System.Windows.Data;

namespace PlusprofilAddin.Views.Controls
{
    /// <summary>
    /// Interaction logic for TaggedValueList.xaml
    /// </summary>
    /// <inheritdoc cref="UserControl"/>
    public partial class TaggedValueListControl : UserControl , INotifyPropertyChanged
    {
        int _id = -1;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        /// <inheritdoc />
        public TaggedValueListControl()
		{
			InitializeComponent();
		}

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            var test = (sender as Button).CommandParameter;
            RemoveCommand rm = new RemoveCommand();
            object[] param = new object[3];
            param[0] = DataContext;
            param[1] = _id;
            param[2] = window.DataContext;
            rm.Execute(param);
        }

        private void TaggedValueListBox_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var item = VisualTreeHelper.HitTest(TaggedValueListBox, Mouse.GetPosition(TaggedValueListBox)).VisualHit;

            // find ListViewItem (or null)
            while (item != null && !(item is ListBoxItem))
                item = VisualTreeHelper.GetParent(item);

            if (item != null)
            {
                _id = TaggedValueListBox.Items.IndexOf(((ListBoxItem)item).DataContext);
                Debug.WriteLine($"I'm on item {_id}");
                Debug.WriteLine("wpf id: " + FindResource("selectedId"));
            }

            FindResource("selectedId");
            Resources["selectedId"] = _id;
        }
    }
}
