using System.Windows;
using System.Windows.Controls;

namespace PlusprofilAddin.Views
{
	/// <summary>
	/// Interaction logic for ConnectorUserControl.xaml
	/// </summary>
	public partial class ConnectorUserControl : UserControl
	{
		public ConnectorUserControl()
		{
			InitializeComponent();
		}

		private void TaggedValueListBox_OnLostFocus(object sender, RoutedEventArgs e)
		{
			if (sender is ListBox listBox) listBox.SelectedIndex = -1;
		}
	}
}
