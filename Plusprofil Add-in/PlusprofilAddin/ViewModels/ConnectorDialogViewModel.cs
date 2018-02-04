using System.Collections.Generic;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels.Commands;

namespace PlusprofilAddin.ViewModels
{
	/// <inheritdoc />
	public class ConnectorDialogViewModel : DialogViewModel
	{
		/// <summary>
		/// 
		/// </summary>
		public ConnectorUserControlViewModel SourceViewModel { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public ConnectorUserControlViewModel TargetViewModel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Connector Connector { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public ConnectorEnd SourceEnd { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public ConnectorEnd TargetEnd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Visibility ShowSourceEnd { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public Visibility ShowTargetEnd { get; set; }


		/// <inheritdoc />
		public ConnectorDialogViewModel()
		{
			DeleteTaggedValues = new List<ViewModelTaggedValue>();
		}

		/// <inheritdoc />
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