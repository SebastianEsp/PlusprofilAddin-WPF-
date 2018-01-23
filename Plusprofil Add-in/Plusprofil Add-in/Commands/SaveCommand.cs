using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EA;
using PlusprofilAddin.ViewModels;
using Attribute = EA.Attribute;

namespace PlusprofilAddin.Commands
{
	
	// TODO: Implement saving of name, alias and URI

	public class SaveCommand : ICommand
	{
		
#pragma warning disable 0067
		public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

		public bool CanExecute(object parameter)
		{
			return true;
		}

		// TODO: Add MultiBinding and converter to SaveCommand such that
		// TODO: parameter[0] is DialogViewModel viewModel and
		// TODO: parameter[1] is Window window

		// Expected parameter: DialogViewModel
		public void Execute(object parameter)
		{
			List<ObservableCollection<ObservableCollection<DisplayedTaggedValue>>> toUpdateCollectionsList;
			switch (parameter)
			{
				case ElementDialogViewModel viewModel:
					Element element = viewModel.Element;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<DisplayedTaggedValue>>>
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

					break;
				
				case PackageDialogViewModel viewModel:
					Element packageElement = viewModel.PackageElement;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<DisplayedTaggedValue>>>
					{
						viewModel.DanishTaggedValues,
						viewModel.EnglishTaggedValues,
						viewModel.ModelMetadataTaggedValues
					};
					foreach (var toUpdateCollection in toUpdateCollectionsList)
					foreach (var collection in toUpdateCollection)
					foreach (DisplayedTaggedValue dtv in collection)
					{
						if (dtv.TaggedValue == null) dtv.AddTaggedValue(packageElement.TaggedValues);
						dtv.UpdateTaggedValueValue();
					}
					foreach (var dtv in viewModel.DeleteTaggedValues)
					{
						dtv.DeleteTaggedValue(packageElement.TaggedValues);
					}

					break;
				
				case AttributeDialogViewModel viewModel:
					Attribute attribute = viewModel.Attribute;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<DisplayedTaggedValue>>>
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
						if (dtv.TaggedValue == null) dtv.AddTaggedValue(attribute.TaggedValues);
						dtv.UpdateTaggedValueValue();
					}
					foreach (var dtv in viewModel.DeleteTaggedValues)
					{
						dtv.DeleteTaggedValue(attribute.TaggedValues);
					}

					break;
				
				case ConnectorDialogViewModel viewModel:
					throw new NotImplementedException("Saving connectors is not implemented");
					break;
			}
		}
	}
}