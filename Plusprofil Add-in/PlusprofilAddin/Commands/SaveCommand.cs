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
		/// <summary>
		/// Required interface member of ICommand
		/// </summary>
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
			List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>> toUpdateCollectionsList;
			switch (parameter)
			{
				case ElementDialogViewModel viewModel:
					Element element = viewModel.Element;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.DanishTaggedValues,
						viewModel.EnglishTaggedValues,
						viewModel.ProvenanceTaggedValues,
						viewModel.StereotypeTaggedValues
					};

					UpdateTaggedValues(element.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(element.TaggedValues, viewModel.DeleteTaggedValues);
					
					// Special cases
					element.Name = viewModel.UMLNameValue;
					element.Alias = viewModel.AliasValue;
					viewModel.URIViewmodelTaggedValue.UpdateTaggedValueValue();
					element.Update();
					break;
				
				case PackageDialogViewModel viewModel:
					Element packageElement = viewModel.PackageElement;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.DanishTaggedValues,
						viewModel.EnglishTaggedValues,
						viewModel.ModelMetadataTaggedValues
					};
					
					UpdateTaggedValues(packageElement.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(packageElement.TaggedValues, viewModel.DeleteTaggedValues);

					// Special cases
					packageElement.Name = viewModel.UMLNameValue;
					packageElement.Alias = viewModel.AliasValue;
					packageElement.Update();
					break;
				
				case AttributeDialogViewModel viewModel:
					Attribute attribute = viewModel.Attribute;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.DanishTaggedValues,
						viewModel.EnglishTaggedValues,
						viewModel.ProvenanceTaggedValues,
						viewModel.StereotypeTaggedValues
					};
					
					UpdateTaggedValues(attribute.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(attribute.TaggedValues, viewModel.DeleteTaggedValues);

					// Special cases
					attribute.Name = viewModel.UMLNameValue;
					attribute.Alias = viewModel.AliasValue;
					attribute.Type = viewModel.DatatypeValue;
					viewModel.URIViewmodelTaggedValue.UpdateTaggedValueValue();
					attribute.Update();
					break;
				
				case ConnectorDialogViewModel viewModel:
					ConnectorEnd sourceEnd = viewModel.SourceEnd;
					ConnectorEnd targetEnd = viewModel.TargetEnd;

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.SourceViewModel.DanishTaggedValues,
						viewModel.SourceViewModel.EnglishTaggedValues,
						viewModel.SourceViewModel.ProvenanceTaggedValues,
						viewModel.SourceViewModel.StereotypeTaggedValues,

					};

					UpdateTaggedValues(sourceEnd.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(sourceEnd.TaggedValues, viewModel.DeleteTaggedValues);

					// Repeat for TargetConnectorEnd

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.TargetViewModel.DanishTaggedValues,
						viewModel.TargetViewModel.EnglishTaggedValues,
						viewModel.TargetViewModel.ProvenanceTaggedValues,
						viewModel.TargetViewModel.StereotypeTaggedValues,
					};

					UpdateTaggedValues(targetEnd.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(targetEnd.TaggedValues, viewModel.DeleteTaggedValues);

					// Special cases
					sourceEnd.Role = viewModel.SourceViewModel.UMLNameValue;
					targetEnd.Role = viewModel.TargetViewModel.UMLNameValue;
					sourceEnd.Alias = viewModel.SourceViewModel.AliasValue;
					targetEnd.Alias = viewModel.TargetViewModel.AliasValue;
					sourceEnd.Cardinality = viewModel.SourceViewModel.MultiplicityValue;
					targetEnd.Cardinality = viewModel.TargetViewModel.MultiplicityValue;
					viewModel.SourceViewModel.URIViewmodelTaggedValue.UpdateTaggedValueValue();
					viewModel.TargetViewModel.URIViewmodelTaggedValue.UpdateTaggedValueValue();
					sourceEnd.Update();
					targetEnd.Update();
					break;
			}
		}

		private void UpdateTaggedValues(Collection taggedValues, List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>> list)
		{
			foreach (var toUpdateCollection in list)
			foreach (var collection in toUpdateCollection)
			foreach (ViewmodelTaggedValue dtv in collection)
			{
				if (dtv.TaggedValue == null) dtv.AddTaggedValue(taggedValues);
				dtv.UpdateTaggedValueValue();
			}
		}

		private void DeleteTaggedValues(Collection taggedValues, List<ViewmodelTaggedValue> list)
		{
			foreach(var dtv in list) dtv.DeleteTaggedValue(taggedValues);
		}
	}
}