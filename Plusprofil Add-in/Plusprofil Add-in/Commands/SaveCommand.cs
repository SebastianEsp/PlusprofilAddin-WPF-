using System;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
	class SaveCommand : ICommand
	{
		public SaveCommand(){

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
