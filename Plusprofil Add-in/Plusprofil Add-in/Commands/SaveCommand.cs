using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EA;
using PlusprofilAddin.ViewModels;

// TODO: Implement saving of name, alias and URI

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

		// Expected parameter: DialogViewModel
		public void Execute(object parameter)
		{
			if (parameter is ElementDialogViewModel viewModel)
			{
				Element element = viewModel.Element;
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
						{
							if (dtv.TaggedValue == null) dtv.AddTaggedValue(element.TaggedValues);
							dtv.UpdateTaggedValueValue();
						}
				foreach (var dtv in viewModel.DeleteTaggedValues)
				{
					dtv.DeleteTaggedValue(element.TaggedValues);
				}
			}
		}
	}
}