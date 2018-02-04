using System.Windows;
using System.Windows.Controls;

namespace PlusprofilAddin.Views.Controls
{
	/// <summary>
	/// Interaction logic for ConnectorUserControl.xaml
	/// </summary>
	public partial class ConnectorUserControl : UserControl
	{
		/// <inheritdoc />
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
