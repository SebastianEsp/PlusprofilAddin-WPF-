﻿using System.Windows;
using System.Windows.Controls;

namespace PlusprofilAddin.Views.Controls
{
	/// <summary>
	/// Interaction logic for TaggedValueList.xaml
	/// </summary>
	public partial class TaggedValueListControl : UserControl
	{
		/// <inheritdoc />
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
