using System.Windows;
using System.Windows.Controls;

namespace PlusprofilAddin.Views.Dialogs
{
	/// <summary>
	///     Interaction logic for ElementDialog.xaml
	/// </summary>
	/// <inheritdoc cref="Window"/>
	public partial class ElementDialog
	{
		/// <inheritdoc />
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