using System.Collections.ObjectModel;
using System.Windows;
using EA;

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
			SourceViewModel = new ConnectorUserControlViewModel();
			TargetViewModel = new ConnectorUserControlViewModel();
		}

		public override void Initialize()
		{
			Connector = Repository.GetContextObject();
			SourceEnd = Connector.SupplierEnd;
			TargetEnd = Connector.ClientEnd;

			// TODO: Check ConnectorEnds for existence and handle cases where they do not exist
			SourceViewModel.Initialize();
			TargetViewModel.Initialize();
		}
	}
}