using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PlusprofilAddin.ViewModels.Commands
{
	/// <summary>
	/// Command used to remove an <c>ViewmodelTaggedValue</c> from the <c>ItemsSource</c> of a View element and add it to a list of items to delete in the <c>DialogViewModel</c>.
	/// </summary>
	/// <inheritdoc/>
	public class RemoveCommand : ICommand
	{
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Determines if <c>RemoveCommand.Execute(object parameter)</c> can be called.
		/// </summary>
		/// <returns>Returns true.</returns>
		/// <inheritdoc/>
		public bool CanExecute(object parameter)
		{
			return true;
		}

        /// <summary>
        /// Removes the currently selected ViewModelTaggedValue from the 
        /// Expected parameter is type object[3]
        /// Source / Type of parameter[0]: ListBox.Source (ObservableCollection)
        /// Source / Type of parameter[1]: ListBox.SelectedIndex (int)
        /// Source / Type of parameter[2]: DialogViewModel
        /// </summary>
        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            if (!(parameter is object[] values) || values.Length != 3) return;
            var list = (ObservableCollection<ViewModelTaggedValue>)values[0];
            var index = (int)values[1];
            List<ViewModelTaggedValue> deleteTaggedValues = null;
            switch (values[2])
            {
                case DialogViewModel viewModel:
                    deleteTaggedValues = viewModel.DeleteTaggedValues;
                    break;
            }

            Debug.Write(index);
            if (index == -1 || index == 0) return;
            Debug.Write("success 2");
            var dtv = list.ElementAt(index);
            deleteTaggedValues?.Add(dtv);
            list.RemoveAt(index);
        }
    }

    /// <summary>
    /// Converter used to return a clone of the <c>MultiBinding</c> values used in <c>RemoveCommand</c>
    /// </summary>
    /// <inheritdoc />
    public class RemoveCommandConverter : IMultiValueConverter
	{
		/// <inheritdoc />
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
            int id = (int)values[1];
            //Debugger.Break();
            return values.Clone();
		}

		/// <inheritdoc />
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("ConvertBack is not supported");
		}
	}
}