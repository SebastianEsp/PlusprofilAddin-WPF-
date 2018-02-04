using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using EA;

namespace PlusprofilAddin.ViewModels.Commands
{
	/// <summary>
	/// 
	/// </summary>
	/// <inheritdoc />
	public class SaveCommand : ICommand
	{
		
#pragma warning disable 0067
		/// <inheritdoc/>
		public event EventHandler CanExecuteChanged;
#pragma warning restore 0067

		/// <summary>
		/// Determines if <c>SaveCommand.Execute(object parameter)</c> can be called.
		/// </summary>
		/// <returns>Returns true.</returns>
		/// <inheritdoc/>
		public bool CanExecute(object parameter)
		{
			return true;
		}

		/// <summary>
		/// Expected parameter is type object[2]
		/// Source / Type of parameter[0]: DialogViewModel
		/// Source / Type of parameter[1]: ListBox.SelectedIndex (int)
		/// </summary>
		/// <inheritdoc/>
		public void Execute(object parameter)
		{
			List<ObservableCollection<ObservableCollection<ViewModelTaggedValue>>> toUpdateCollectionsList;
			if (!(parameter is object[] values) || values.Length != 2) return;
			switch (values[0])
			{
				case ElementDialogViewModel viewModel:
					var element = viewModel.Element;

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewModelTaggedValue>>>
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
					var packageElement = viewModel.PackageElement;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewModelTaggedValue>>>
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
					var attribute = viewModel.Attribute;
					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewModelTaggedValue>>>
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
					var sourceEnd = viewModel.SourceEnd;
					var targetEnd = viewModel.TargetEnd;

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewModelTaggedValue>>>
					{
						viewModel.SourceViewModel.DanishViewmodelTaggedValues,
						viewModel.SourceViewModel.EnglishViewmodelTaggedValues,
						viewModel.SourceViewModel.ProvenanceViewmodelTaggedValues,
						viewModel.SourceViewModel.StereotypeViewmodelTaggedValues

					};

					UpdateTaggedValues(sourceEnd.TaggedValues, toUpdateCollectionsList);
					DeleteTaggedValues(sourceEnd.TaggedValues, viewModel.DeleteTaggedValues);

					// Repeat for TargetConnectorEnd

					toUpdateCollectionsList = new List<ObservableCollection<ObservableCollection<ViewModelTaggedValue>>>
					{
						viewModel.TargetViewModel.DanishViewmodelTaggedValues,
						viewModel.TargetViewModel.EnglishViewmodelTaggedValues,
						viewModel.TargetViewModel.ProvenanceViewmodelTaggedValues,
						viewModel.TargetViewModel.StereotypeViewmodelTaggedValues
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

			var window = (Window) values[1];
			window.Close();
		}

		private static void UpdateTaggedValues(Collection taggedValues, List<ObservableCollection<ObservableCollection<ViewModelTaggedValue>>> list)
		{
			foreach (var toUpdateCollection in list)
			foreach (var collection in toUpdateCollection)
			foreach (var dtv in collection)
			{
				if (dtv.TaggedValue == null) dtv.AddTaggedValue(taggedValues);
				else dtv.UpdateTaggedValueValue();
			}
		}

		private static void DeleteTaggedValues(Collection taggedValues, List<ViewModelTaggedValue> list)
		{
			foreach(var dtv in list) dtv.DeleteTaggedValue(taggedValues);
		}
	}

	/// <summary>
	/// Converter used to return a clone of the <c>MultiBinding</c> values used in <c>SaveCommand</c>
	/// </summary>
	/// <inheritdoc />
	public class SaveCommandConverter : IMultiValueConverter
	{
		/// <inheritdoc />
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			return values.Clone();
		}

		/// <inheritdoc />
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("ConvertBack is not supported");
		}
	}
}