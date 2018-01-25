using System;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
	/// <summary>
	/// Command used to close the current <c>Window</c> without saving any changes made
	/// </summary>
	public class CancelCommand : ICommand
	{
		private string _discardAllChangesString;
		private string _discardString;

#pragma warning disable 0067
		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

		/// <summary>
		/// Determines if <c>CancelCommand.Execute(object parameter)</c> can be called.
		/// As exiting should be available at all times, this always returns true.
		/// </summary>
		/// <param name="parameter">Currently not used</param>
		/// <returns>Returns true.</returns>
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Presents a warning <c>MessageBox</c>, then closes the current <c>Window</c> if confirmed
		/// </summary>
		/// <param name="parameter">The <c>Window</c> to shut down</param>
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