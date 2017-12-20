using System;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
	class CancelCommand : ICommand
	{
		public CancelCommand()
		{

		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
            //TODO: Retrieve strings from resources
            Window window = parameter as Window;
            MessageBoxResult result = MessageBox.Show("Discard all changes?", "Confirm", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK) window.Close();
		}
	}
}
