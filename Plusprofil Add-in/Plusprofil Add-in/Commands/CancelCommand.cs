using System;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
    class CancelCommand : ICommand
    {

        string DiscardAllChangesString;
        string DiscardString;

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
            Window window = parameter as Window;
            DiscardAllChangesString = (string)window.FindResource("DiscardAllChanges");
            DiscardString = (string)window.FindResource("Discard");
            MessageBoxResult result = MessageBox.Show(DiscardAllChangesString, DiscardString, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK) window.Close();
		}
	}
}
