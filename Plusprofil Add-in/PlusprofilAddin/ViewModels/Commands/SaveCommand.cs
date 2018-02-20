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
	/// Command used to register changes made in the add-in in Sparx Systems Enterprise Architect.
	/// </summary>
	/// <inheritdoc />
	public class SaveCommand : ICommand
	{
		/// <inheritdoc/>
		public event EventHandler CanExecuteChanged;

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
		/// Source / Type of parameter[0]: ViewModel / DialogViewModel
		/// Source / Type of parameter[1]: ListBox.SelectedIndex / int
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

					// Set the lists of ViewModelTaggedValues to update
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

					// Repeat save process for TargetConnectorEnd
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
			//window.Close();
		}

		/// <summary>
		/// Iterates through every <c>ViewModelTaggedValue</c> in the parameter <c>list</c>, calling <c>ViewModelTaggedValue.AddTaggedValue()</c> 
		/// if <c>ViewModelTaggedValue.TaggedValue</c> has not been set, i.e. the <c>ViewModelTaggedValue</c> has been added by the add-in, 
		/// or <c>ViewModelTaggedValue.UpdateTaggedValue()</c> if <c>ViewModelTaggedValue.TaggedValue</c> has been set, i.e. the <c>ViewModelTaggedValue</c> 
		/// represents a previously existing tagged value.
		/// </summary>
		/// <param name="taggedValues">The <c>EA.Collection</c> to update, i.e. the tagged values of the object selected when the add-in was created.</param>
		/// <param name="list">Every <c>ViewModelTaggedValue</c> to update.</param>
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

		/// <summary>
		/// Iterates through every <c>ViewModelTaggedValue</c> in the parameter <c>list</c>, calling <c>ViewModelTaggedValue.DeleteTaggedValue()</c> 
		/// on each <c>ViewModelTaggedValue</c>, deleting the <c>ViewModelTaggedValue.TaggedValue</c> from the tagged values of the 
		/// Sparx Systems Enterprise Architect object selected when the add-in was created.
		/// </summary>
		/// <param name="taggedValues">The <c>EA.Collection</c> to delete tagged values from, i.e. the tagged values of the object selected when the add-in was created.</param>
		/// <param name="list">A <c>List</c> of <c>ViewModelTaggedValue</c> to delete.</param>
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