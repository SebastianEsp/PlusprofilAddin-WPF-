using System.Collections.ObjectModel;
using EA;

namespace PlusprofilAddin.ViewModels
{
	internal class ConnectorDialogViewModel : DialogViewModel
	{
		public ConnectorDialogViewModel()
		{
			SourceTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			SourceDanishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			SourceEnglishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			SourceProvenanceTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			SourceStereotypeTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			TargetTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			TargetDanishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			TargetEnglishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			TargetProvenanceTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
			TargetStereotypeTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();

			URIText = "URI";
			UMLText = "UML";
			AliasText = "Alias";
			SourceURIValue = "";
			SourceUMLValue = "";
			SourceAliasValue = "";
			SourceMultiplicityValue = "";
			TargetURIValue = "";
			TargetUMLValue = "";
			TargetAliasValue = "";
			TargetMultiplicityValue = "";
		}

		public Connector Connector { get; set; }

		public string URIText { get; set; }
		public string UMLText { get; set; }
		public string AliasText { get; set; }
		public string MultiplicityText { get; set; }
		public string SourceURIValue { get; set; }
		public string SourceUMLValue { get; set; }
		public string SourceAliasValue { get; set; }
		public string SourceMultiplicityValue { get; set; }
		public string TargetURIValue { get; set; }
		public string TargetUMLValue { get; set; }
		public string TargetAliasValue { get; set; }
		public string TargetMultiplicityValue { get; set; }

		public ObservableCollection<PlusprofilTaggedValue> SourceTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> SourceDanishTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> SourceEnglishTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> SourceProvenanceTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> SourceStereotypeTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> TargetTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> TargetDanishTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> TargetEnglishTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> TargetProvenanceTaggedValues { get; set; }
		public ObservableCollection<PlusprofilTaggedValue> TargetStereotypeTaggedValues { get; set; }

		public override void Initialize()
		{
			Connector = Repository.GetContextObject() as Connector;
		}
	}
}