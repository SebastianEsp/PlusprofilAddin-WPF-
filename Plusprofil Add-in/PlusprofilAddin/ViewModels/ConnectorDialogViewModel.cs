using System.Collections.Generic;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels.Commands;

namespace PlusprofilAddin.ViewModels
{
	public class ConnectorDialogViewModel : DialogViewModel
	{
		public ConnectorUserControlViewModel SourceViewModel { get; set; }
		public ConnectorUserControlViewModel TargetViewModel { get; set; }
		public string Text { get; set; }

		public Connector Connector { get; set; }
		public ConnectorEnd SourceEnd { get; set; }
		public ConnectorEnd TargetEnd { get; set; }

		public Visibility ShowSourceEnd { get; set; }
		public Visibility ShowTargetEnd { get; set; }

		public ConnectorDialogViewModel()
		{
			DeleteTaggedValues = new List<ViewmodelTaggedValue>();
		}

		public override void Initialize()
		{
			SaveCommand = new SaveCommand();
			CancelCommand = new CancelCommand();
			AddCommand = new AddCommand();
			RemoveCommand = new RemoveCommand();

			Connector = Repository.GetContextObject();
			SourceEnd = Connector.SupplierEnd;
			TargetEnd = Connector.ClientEnd;
			
			ShowSourceEnd = SourceEnd.Stereotype == "ObjectProperty" ? Visibility.Visible : Visibility.Collapsed;
			ShowTargetEnd = TargetEnd.Stereotype == "ObjectProperty" ? Visibility.Visible : Visibility.Collapsed;

			SourceViewModel = new ConnectorUserControlViewModel {ConnectorEnd = SourceEnd, ElementNameValue = Repository.GetElementByID(Connector.SupplierID).Name};
			TargetViewModel = new ConnectorUserControlViewModel {ConnectorEnd = TargetEnd, ElementNameValue = Repository.GetElementByID(Connector.ClientID).Name};
			SourceViewModel.ResourceDictionary = ResourceDictionary;
			TargetViewModel.ResourceDictionary = ResourceDictionary;

			SourceViewModel.Initialize();
			TargetViewModel.Initialize();
		}
	}
}