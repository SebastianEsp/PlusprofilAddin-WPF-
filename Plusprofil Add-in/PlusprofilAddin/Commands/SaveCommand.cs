using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EA;
using PlusprofilAddin.ViewModels;
using Attribute = EA.Attribute;

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
					
					// Special cases
					element.Name = viewModel.UMLNameValue;
					element.Alias = viewModel.AliasValue;
					viewModel.URIDisplayedTaggedValue.UpdateTaggedValueValue();
					element.Update();
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

					// Special cases
					packageElement.Name = viewModel.UMLNameValue;
					packageElement.Alias = viewModel.AliasValue;
					packageElement.Update();
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

					// Special cases
					attribute.Name = viewModel.UMLNameValue;
					attribute.Alias = viewModel.AliasValue;
					attribute.Type = viewModel.DatatypeValue;
					viewModel.URIDisplayedTaggedValue.UpdateTaggedValueValue();
					attribute.Update();
					break;
				
				case ConnectorDialogViewModel viewModel:
					ConnectorEnd sourceEnd = viewModel.SourceEnd;
					ConnectorEnd targetEnd = viewModel.TargetEnd;

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<DisplayedTaggedValue>>>
					{
						viewModel.SourceViewModel.DanishTaggedValues,
						viewModel.SourceViewModel.EnglishTaggedValues,
						viewModel.SourceViewModel.ProvenanceTaggedValues,
						viewModel.SourceViewModel.StereotypeTaggedValues,

					};
					foreach (var toUpdateCollection in toUpdateCollectionsList)
					foreach (var collection in toUpdateCollection)
					foreach (DisplayedTaggedValue dtv in collection)
					{
						if (dtv.TaggedValue == null) dtv.AddTaggedValue(sourceEnd.TaggedValues);
						dtv.UpdateTaggedValueValue();
					}
					foreach (var dtv in viewModel.SourceViewModel.DeleteTaggedValues)
					{
						dtv.DeleteTaggedValue(sourceEnd.TaggedValues);
					}

					// Repeat for TargetConnectorEnd

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<DisplayedTaggedValue>>>
					{
						viewModel.TargetViewModel.DanishTaggedValues,
						viewModel.TargetViewModel.EnglishTaggedValues,
						viewModel.TargetViewModel.ProvenanceTaggedValues,
						viewModel.TargetViewModel.StereotypeTaggedValues,
					};
					foreach (var toUpdateCollection in toUpdateCollectionsList)
					foreach (var collection in toUpdateCollection)
					foreach (DisplayedTaggedValue dtv in collection)
					{
						if (dtv.TaggedValue == null) dtv.AddTaggedValue(targetEnd.TaggedValues);
						dtv.UpdateTaggedValueValue();
					}
					foreach (var dtv in viewModel.TargetViewModel.DeleteTaggedValues)
					{
						dtv.DeleteTaggedValue(targetEnd.TaggedValues);
					}


					// TODO: Implement saving of ConnectorEnd tagged values

					// Special cases
					sourceEnd.Role = viewModel.SourceViewModel.UMLNameValue;
					targetEnd.Role = viewModel.TargetViewModel.UMLNameValue;
					sourceEnd.Alias = viewModel.SourceViewModel.AliasValue;
					targetEnd.Alias = viewModel.TargetViewModel.AliasValue;
					sourceEnd.Cardinality = viewModel.SourceViewModel.MultiplicityValue;
					targetEnd.Cardinality = viewModel.TargetViewModel.MultiplicityValue;
					viewModel.SourceViewModel.URIDisplayedTaggedValue.UpdateTaggedValueValue();
					viewModel.TargetViewModel.URIDisplayedTaggedValue.UpdateTaggedValueValue();
					sourceEnd.Update();
					targetEnd.Update();
					break;
			}
		}
	}
}