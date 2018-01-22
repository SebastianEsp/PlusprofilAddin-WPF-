using System;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
	public class SaveCommand : ICommand
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
			MessageBox.Show("Clicked save!");
			MessageBox.Show(parameter.ToString());
		}
	}
}