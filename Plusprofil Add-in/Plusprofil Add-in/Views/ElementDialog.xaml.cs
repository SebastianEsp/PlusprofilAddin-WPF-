using System.Windows;
using System.Windows.Controls;

namespace PlusprofilAddin.Views
{
	/// <summary>
	///     Interaction logic for ElementDialog.xaml
	/// </summary>
	public partial class ElementDialog
	{
		public ElementDialog()
		{
			InitializeComponent();
		}

		// TODO: Refactor (Breaks MVVM pattern)
		private void TaggedValueListBox_OnLostFocus(object sender, RoutedEventArgs e)
		{
			if (sender is ListBox listBox)
			{
				listBox.SelectedIndex = -1;
			}
		}
	}
}