using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PlusprofilAddin.ViewModels.Commands
{
	/// <summary>
	/// Command used to add an additional <c>ViewmodelTaggedValue</c> to the <c>ItemsSource</c> of a View element
	/// </summary>
	public class AddCommand : ICommand
	{
#pragma warning disable 0067
		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

		/// <summary>
		/// Determines if <c>AddCommand.Execute(object parameter)</c> can be called.
		/// As View currently hides elements that call <c>AddCommand.Execute(object parameter)</c>, this will always return true.
		/// </summary>
		/// <returns>Returns true.</returns>
		/// <inheritdoc/>
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Adds a new <c>ViewmodelTaggedValue</c> to the <c>ObservableCollection</c> using the <c>Key</c> of the first element in the <c>ObservableCollection</c>
		/// </summary>
		/// <param name="parameter">
		/// <c>IEnumerable</c>to add the new <c>ViewmodelTaggedValue</c>.
		/// Currently only supports <c>ObservableCollection</c> as used in the viewmodel
		/// </param>
		public void Execute(object parameter)
		{
			if (parameter is ObservableCollection<ViewModelTaggedValue> list)
			{
				list.Add(new ViewModelTaggedValue(list.First().Key));
			}
		}
	}
}