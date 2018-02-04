using System;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.ViewModels.Commands
{
	/// <summary>
	/// Command used to close the current <c>Window</c> without saving any changes made
	/// </summary>
	/// <inheritdoc/>
	public class CancelCommand : ICommand
	{
		private string _discardAllChangesString;
		private string _discardString;

		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Determines if <c>CancelCommand.Execute(object parameter)</c> can be called.<para/>
		/// As exiting should be available at all times, this always returns true.
		/// </summary>
		/// <returns>Returns <c>true</c>.</returns>
		/// <inheritdoc/>
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Presents a warning <c>MessageBox</c>, then closes the current <c>Window</c> if confirmed.<para/>
		/// Expected parameter is type <c>Window</c> 
		/// </summary>
		/// <inheritdoc/>
		public void Execute(object parameter)
		{
			if (!(parameter is Window window)) return;
			_discardAllChangesString = (string) window.FindResource("DiscardAllChanges");
			_discardString = (string) window.FindResource("Discard");
			var result = MessageBox.Show(_discardAllChangesString, _discardString, MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.OK) window.Close();
		}
	}
}