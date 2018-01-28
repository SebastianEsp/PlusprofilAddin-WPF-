using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace PlusprofilAddin.ViewModels.Commands
{
	/// <summary>
	/// Command used to remove an <c>ViewmodelTaggedValue</c> from the <c>ItemsSource</c> of a View element and add it to the 
	/// </summary>
	public class RemoveCommand : ICommand
	{

#pragma warning disable 0067
		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;
#pragma warning restore 0067


		public bool CanExecute(object parameter)
		{
			return true;
		}

		// Expected parameter: object[2]
		// values[0]: ListBox.Source (ObservableCollection<>)
		// values[1]: ListBox.SelectedIndex (int)
		// values[2]: DialogViewModel
		public void Execute(object parameter)
		{
			if (parameter is object[] values && values.Length == 3)
			{
				ObservableCollection<ViewmodelTaggedValue> list = (ObservableCollection<ViewmodelTaggedValue>) values[0];
				int index = (int) values[1];
				List<ViewmodelTaggedValue> deleteTaggedValues = null;
				switch (values[2])
				{
					case DialogViewModel viewModel:
						deleteTaggedValues = viewModel.DeleteTaggedValues;
						break;
				}

				if (index != -1 && index != 0)
				{
					ViewmodelTaggedValue dtv = list.ElementAt(index);
					deleteTaggedValues?.Add(dtv);
					list.RemoveAt(index);
				}
			}
		}
	}

	public class RemoveCommandConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			return values.Clone();
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("ConvertBack is not supported");
		}
	}
}