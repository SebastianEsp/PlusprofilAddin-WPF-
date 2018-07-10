using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PlusprofilAddin.ViewModels.Commands
{
	
	/// <summary>
	/// Command used to add an additional <c>ViewmodelTaggedValue</c> to the <c>ItemsSource</c> of a View element
	/// </summary>
	/// <inheritdoc />
	public class AddCommand : ICommand
	{

		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Determines if <c>AddCommand.Execute(object parameter)</c> can be called.<para/>
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
		/// <c>IEnumerable</c>to add the new <c>ViewmodelTaggedValue</c>.<para/>
		/// Currently only supports <c>ObservableCollection</c> as used in the viewmodel
		/// </param>
		/// <inheritdoc />
		public void Execute(object parameter)
		{
			if (parameter is ObservableCollection<ViewModelTaggedValue> list)
			{
                //var test = new PlusprofilTaggedValue(list.First().PlusprofilTaggedValue.Key, list.First().PlusprofilTaggedValue.Name, list.First().PlusprofilTaggedValue.HasMemoField, list.First().PlusprofilTaggedValue.ManyMultiplicity, true);
                list.Add(new ViewModelTaggedValue(list.First().Key, true));
            } 
		}
	}
}