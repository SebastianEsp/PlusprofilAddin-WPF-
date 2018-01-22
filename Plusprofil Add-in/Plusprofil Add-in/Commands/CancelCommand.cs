using System;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
	public class CancelCommand : ICommand
	{
		private string _discardAllChangesString;
		private string _discardString;

#pragma warning disable 0067
		public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (parameter is Window window)
			{
				_discardAllChangesString = (string) window.FindResource("DiscardAllChanges");
				_discardString = (string) window.FindResource("Discard");
				MessageBoxResult result = MessageBox.Show(_discardAllChangesString, _discardString, MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.OK) window.Close();
			}
		}
	}
}