﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
	public class RemoveCommand : ICommand
	{

#pragma warning disable 0067
		public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (parameter.GetType() == typeof(object[]))
			{
				object[] values = parameter as object[];
				ObservableCollection<DisplayedTaggedValue> list = (ObservableCollection<DisplayedTaggedValue>) values[0];
				int index = (int) values[1];
				if (index != -1 && index != 0) list.RemoveAt(index);
			}
		}
	}

	public class RemoveCommandConverter : IMultiValueConverter
	{
		//values[0]: ListBox
		//values[1]: ListBox.SelectedIndex
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			return values.Clone();
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("Cannot convert back");
		}
	}
}