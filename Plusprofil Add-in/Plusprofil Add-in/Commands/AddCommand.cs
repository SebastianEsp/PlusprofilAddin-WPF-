﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
	public class AddCommand : ICommand
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
			if (parameter.GetType() == typeof(ObservableCollection<DisplayedTaggedValue>))
			{
				ObservableCollection<DisplayedTaggedValue> list = parameter as ObservableCollection<DisplayedTaggedValue>;
				string name = list.First().Name;
				//TODO: Create new tagged value instead
				list.Add(new DisplayedTaggedValue(name, ""));
			}
		}
	}
}