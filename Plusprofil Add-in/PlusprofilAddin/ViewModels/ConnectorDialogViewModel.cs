using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels.Commands;

namespace PlusprofilAddin.ViewModels
{
	/// <summary>
	/// ViewModel used to update View (Window) state and retrieve user input to update Model (Sparx Systems Enterprise Architect) state.<para/>
	/// <c>ConnectorDialogViewModel</c> serves as a link between two <c>ConnectorUserControlViewModel</c>s and <c>MainClass</c>.
	/// </summary>
	/// <inheritdoc />
	public class ConnectorDialogViewModel : DialogViewModel
	{
		/// <summary><c>ConnectorUserControlViewModel</c> representing the <c>EA.Connector.ClientEnd</c>, i.e. Source, <c>ConnectorEnd</c>.</summary>
		public ConnectorUserControlViewModel SourceViewModel { get; set; }
		
		/// <summary><c>ConnectorUserControlViewModel</c> representing the <c>EA.Connector.SupplierEnd</c>, i.e. Target, <c>ConnectorEnd</c>.</summary>
		public ConnectorUserControlViewModel TargetViewModel { get; set; }

		/// <summary><c>EA.Connector</c> retrieved from Sparx Systems Enterprise Architect automation interface to retrieve <c>SourceEnd</c> and <c>TargetEnd</c>.</summary>
		public Connector Connector { get; set; }
		
		/// <summary>The <c>EA.Connector.ClientEnd</c>, i.e. Source, <c>ConnectorEnd</c>.</summary>
		public ConnectorEnd SourceEnd { get; set; }
		
		/// <summary>The <c>EA.Connector.SupplierEnd</c>, i.e. Target, <c>ConnectorEnd</c>.</summary>
		public ConnectorEnd TargetEnd { get; set; }

		/// <summary>View property used to determine if the Source-half of the View should be shown or hidden.</summary>
		public Visibility ShowSourceEnd { get; set; }
		
		/// <summary>
		/// View property used to determine if the Target-half of the View should be shown or hidden.
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

            switch (SourceEnd.Stereotype)
            {

                case "ObjectProperty":
                    ShowSourceEnd = SourceEnd.Stereotype == "ObjectProperty" ? Visibility.Visible : Visibility.Collapsed;
                    
                break;

                case "RdfsProperty":
                    ShowSourceEnd = SourceEnd.Stereotype == "RdfsProperty" ? Visibility.Visible : Visibility.Collapsed;
                break;

            }

            switch (TargetEnd.Stereotype)
            {

                case "ObjectProperty":
                    ShowTargetEnd = TargetEnd.Stereotype == "ObjectProperty" ? Visibility.Visible : Visibility.Collapsed;
                    break;

                case "RdfsProperty":
                    ShowTargetEnd = TargetEnd.Stereotype == "RdfsProperty" ? Visibility.Visible : Visibility.Collapsed;
                    break;

            }

            SourceViewModel = new ConnectorUserControlViewModel {ConnectorEnd = SourceEnd, ElementNameValue = Repository.GetElementByID(Connector.SupplierID).Name};
			TargetViewModel = new ConnectorUserControlViewModel {ConnectorEnd = TargetEnd, ElementNameValue = Repository.GetElementByID(Connector.ClientID).Name};

            SourceViewModel.ResourceDictionary = ResourceDictionary;
			TargetViewModel.ResourceDictionary = ResourceDictionary;

			SourceViewModel.Initialize();
			TargetViewModel.Initialize();
		}

        public override void OnWindowClosing(object sender, CancelEventArgs e)
        {
            SaveCommand.Execute(new object[] { this, sender });
        }
    }
}