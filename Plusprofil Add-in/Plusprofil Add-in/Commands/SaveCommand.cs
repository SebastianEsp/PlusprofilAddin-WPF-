using System;
using System.Windows;
using System.Windows.Input;
using EA;
using PlusprofilAddin.ViewModels;

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

		// Takes a DialogViewModel as parameter
		public void Execute(object parameter)
		{
			if (parameter is DialogViewModel viewModel)
			{
				Repository repository = viewModel.Repository;
				// TODO: Update values belonging to element
			}
		}
	}
}