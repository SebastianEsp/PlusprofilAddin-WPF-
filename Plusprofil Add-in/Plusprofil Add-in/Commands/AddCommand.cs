using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
    class AddCommand : ICommand
	{
        public AddCommand()
        {

        }

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
            if (parameter.GetType() == typeof(ObservableCollection<DisplayedTaggedValue>)){
                ObservableCollection<DisplayedTaggedValue> list = parameter as ObservableCollection<DisplayedTaggedValue>;
                string name = list.First().Name;
                list.Add(new DisplayedTaggedValue(name, ""));
            }
        }
	}
}
