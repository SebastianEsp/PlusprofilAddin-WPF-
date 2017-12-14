using PlusprofilAddin.Model;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.ViewModels;
using System.Collections.Generic;
using PDefinitions = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin
{
    class PackageDialogViewModel : DialogViewModel
    {
		protected List<PlusprofilTaggedValue> ToAddStereotypeTaggedValues = new List<PlusprofilTaggedValue>
		{
			PDefinitions.namespace_,
			PDefinitions.namespacePrefix,
			PDefinitions.publisher,
			PDefinitions.theme,
			PDefinitions.modified,
			PDefinitions.versionInfo,
			PDefinitions.modelStatus,
			PDefinitions.approvalStatus,
			PDefinitions.legalSource,
			PDefinitions.source
		};

		public Package Package { get; set; }

        public string URIText { get; set; }
        public string UMLText { get; set; }
        public string URIValue { get; set; }
        public string UMLValue { get; set; }

        public ObservableCollection<PlusprofilTaggedValue> TaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> DanishTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> EnglishTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> ProvenanceTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> StereotypeTaggedValues { get; set; }

        public PackageDialogViewModel()
        {
            DanishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            EnglishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            ProvenanceTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            StereotypeTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();

            URIText = "URI";
            UMLText = "UML";
            URIValue = "";
            UMLValue = "";
        }

        public override void Initialize()
        {
            Package = Repository.GetContextObject() as Package;
            UMLValue = Package.Element.Name;

			//Finalize list of stereotype tags to add
			if (Package.Element.Stereotype == "LogicalModel" 
			|| Package.Element.Stereotype == "CoreModel"
			|| Package.Element.Stereotype == "Vocabulary"
			|| Package.Element.Stereotype == "ApplicationModel"
			|| Package.Element.Stereotype == "ApplicationProfile")
			{
				ToAddStereotypeTaggedValues.Add(PDefinitions.wasDerivedFrom);
			}

			//Do remaining stuff
		}
    }
}