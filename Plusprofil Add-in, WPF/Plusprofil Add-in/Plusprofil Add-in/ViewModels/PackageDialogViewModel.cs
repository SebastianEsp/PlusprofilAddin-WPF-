using PlusprofilAddin.Model;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.ViewModels;

namespace PlusprofilAddin
{
    class PackageDialogViewModel : DialogViewModel
    {
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
        }
    }
}