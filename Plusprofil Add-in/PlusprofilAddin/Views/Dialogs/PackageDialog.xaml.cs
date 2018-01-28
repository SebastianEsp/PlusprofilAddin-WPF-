using System.Windows;
using System.Windows.Controls;

namespace PlusprofilAddin.Views.Dialogs
{
	/// <summary>
	///     Interaction logic for ElementDialog.xaml
	/// </summary>
	public partial class PackageDialog
	{
		public PackageDialog()
		{
			InitializeComponent();
		}

		private void TaggedValueListBox_OnLostFocus(object sender, RoutedEventArgs e)
		{
			if (sender is ListBox listBox) listBox.SelectedIndex = -1;
		}
	}
}