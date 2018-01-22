using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
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
			if (parameter is ElementDialogViewModel viewModel)
			{
				var toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<DisplayedTaggedValue>>>
				{
					viewModel.DanishTaggedValues,
					viewModel.EnglishTaggedValues,
					viewModel.ProvenanceTaggedValues,
					viewModel.StereotypeTaggedValues
				};

				foreach (var toUpdateCollection in toUpdateCollectionsList)
					foreach (var collection in toUpdateCollection)
						foreach (DisplayedTaggedValue dtv in collection)
							dtv.UpdateTaggedValueValue();
			}
		}
	}
}