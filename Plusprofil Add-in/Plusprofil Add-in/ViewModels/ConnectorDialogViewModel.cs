using System.Collections.ObjectModel;
using System.Windows;
using EA;
using PlusprofilAddin.Commands;

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

		public ConnectorDialogViewModel()
		{
			SaveCommand = new SaveCommand();
			CancelCommand = new CancelCommand();
			AddCommand = new AddCommand();
			RemoveCommand = new RemoveCommand();
		}

		public override void Initialize()
		{
			// TODO: Check ConnectorEnds for existence and handle cases where they do not exist
			Connector = Repository.GetContextObject();
			SourceEnd = Connector.SupplierEnd;
			TargetEnd = Connector.ClientEnd;

			SourceViewModel = new ConnectorUserControlViewModel {ConnectorEnd = SourceEnd};
			TargetViewModel = new ConnectorUserControlViewModel {ConnectorEnd = TargetEnd};
			
			SourceViewModel.Initialize();
			TargetViewModel.Initialize();
		}
	}
}