using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using EA;
using Attribute = EA.Attribute;

namespace PlusprofilAddin.ViewModels.Commands
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
			if (!(parameter is object[] values) || values.Length != 2) return;
			switch (values[0])
			{
				case ElementDialogViewModel viewModel:
					Element element = viewModel.Element;

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.DanishViewmodelTaggedValues,
						viewModel.EnglishViewmodelTaggedValues,
						viewModel.ProvenanceViewmodelTaggedValues,
						viewModel.StereotypeViewmodelTaggedValues
					};

					UpdateTaggedValues(element.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(element.TaggedValues, viewModel.DeleteTaggedValues);

					// Special cases
					element.Name = viewModel.UMLNameValue;
					element.Alias = viewModel.AliasValue;
					viewModel.URIViewmodelTaggedValue.Value = viewModel.URIValue;
					viewModel.URIViewmodelTaggedValue.UpdateTaggedValueValue();
					element.Update();
					break;

				case PackageDialogViewModel viewModel:
					Element packageElement = viewModel.PackageElement;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.DanishViewmodelTaggedValues,
						viewModel.EnglishViewmodelTaggedValues,
						viewModel.ModelMetadataViewmodelTaggedValues
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
						viewModel.DanishViewmodelTaggedValues,
						viewModel.EnglishViewmodelTaggedValues,
						viewModel.ProvenanceViewmodelTaggedValues,
						viewModel.StereotypeViewmodelTaggedValues
					};

					UpdateTaggedValues(attribute.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(attribute.TaggedValues, viewModel.DeleteTaggedValues);

					// Special cases
					attribute.Name = viewModel.UMLNameValue;
					attribute.Alias = viewModel.AliasValue;
					attribute.Type = viewModel.DatatypeValue;
					viewModel.URIViewmodelTaggedValue.Value = viewModel.URIValue;
					viewModel.URIViewmodelTaggedValue.UpdateTaggedValueValue();
					attribute.Update();
					break;

				case ConnectorDialogViewModel viewModel:
					ConnectorEnd sourceEnd = viewModel.SourceEnd;
					ConnectorEnd targetEnd = viewModel.TargetEnd;

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.SourceViewModel.DanishViewmodelTaggedValues,
						viewModel.SourceViewModel.EnglishViewmodelTaggedValues,
						viewModel.SourceViewModel.ProvenanceViewmodelTaggedValues,
						viewModel.SourceViewModel.StereotypeViewmodelTaggedValues,

					};

					UpdateTaggedValues(sourceEnd.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(sourceEnd.TaggedValues, viewModel.DeleteTaggedValues);

					// Repeat for TargetConnectorEnd

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>>
					{
						viewModel.TargetViewModel.DanishViewmodelTaggedValues,
						viewModel.TargetViewModel.EnglishViewmodelTaggedValues,
						viewModel.TargetViewModel.ProvenanceViewmodelTaggedValues,
						viewModel.TargetViewModel.StereotypeViewmodelTaggedValues,
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
					viewModel.SourceViewModel.URIViewmodelTaggedValue.Value = viewModel.SourceViewModel.URIValue;
					viewModel.SourceViewModel.URIViewmodelTaggedValue.UpdateTaggedValueValue();
					viewModel.TargetViewModel.URIViewmodelTaggedValue.Value = viewModel.TargetViewModel.URIValue;
					viewModel.TargetViewModel.URIViewmodelTaggedValue.UpdateTaggedValueValue();

					sourceEnd.Update();
					targetEnd.Update();
					break;
			}

			Window window = (Window) values[1];
			window.Close();
		}

		private void UpdateTaggedValues(Collection taggedValues, List<ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>> list)
		{
			foreach (var toUpdateCollection in list)
			foreach (var collection in toUpdateCollection)
			foreach (ViewmodelTaggedValue dtv in collection)
			{
				if (dtv.TaggedValue == null) dtv.AddTaggedValue(taggedValues);
				else dtv.UpdateTaggedValueValue();
			}
		}

		private void DeleteTaggedValues(Collection taggedValues, List<ViewmodelTaggedValue> list)
		{
			foreach(var dtv in list) dtv.DeleteTaggedValue(taggedValues);
		}
	}
	public class SaveCommandConverter : IMultiValueConverter
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