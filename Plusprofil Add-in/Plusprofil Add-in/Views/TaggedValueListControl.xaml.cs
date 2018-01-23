using System.Windows;
using System.Windows.Controls;

namespace PlusprofilAddin.Views
{
	/// <summary>
	/// Interaction logic for TaggedValueList.xaml
	/// </summary>
	public partial class TaggedValueListControl : UserControl
	{
		public TaggedValueListControl()
		{
			InitializeComponent();
		}
		private void TaggedValueListBox_OnLostFocus(object sender, RoutedEventArgs e)
		{
			if (sender is ListBox listBox) listBox.SelectedIndex = -1;
		}
	}
}
