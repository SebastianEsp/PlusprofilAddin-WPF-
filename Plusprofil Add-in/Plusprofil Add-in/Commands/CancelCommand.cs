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
			MessageBox.Show("Clicked save!");
		}
	}
}
