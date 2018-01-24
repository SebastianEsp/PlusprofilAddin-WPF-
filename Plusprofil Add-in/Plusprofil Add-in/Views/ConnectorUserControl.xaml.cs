using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

		private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
		{
			MessageBox.Show(DataContext.ToString());
		}
	}
}
