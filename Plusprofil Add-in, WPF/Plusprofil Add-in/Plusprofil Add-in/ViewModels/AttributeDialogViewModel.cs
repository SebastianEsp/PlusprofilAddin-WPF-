using PlusprofilAddin.Model;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.ViewModels;

namespace PlusprofilAddin
{
    class AttributeDialogViewModel : DialogViewModel
    {
        public Attribute Attribute { get; set; }

        public string URIText { get; set; }
        public string UMLText { get; set; }
        public string AliasText { get; set; }
        public string DatatypeText { get; set; }
        public string URIValue { get; set; }
        public string UMLValue { get; set; }
        public string AliasValue { get; set; }
        public string DatatypeValue { get; set; }

        public ObservableCollection<PlusprofilTaggedValue> TaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> DanishTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> EnglishTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> ProvenanceTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> StereotypeTaggedValues { get; set; }

        public AttributeDialogViewModel()
        {
            DanishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            EnglishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            ProvenanceTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            StereotypeTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();

            URIText = "URI";
            UMLText = "UML";
            AliasText = "Alias";
            DatatypeText = "";
            URIValue = "";
            UMLValue = "";
            AliasValue = "";
            DatatypeValue = "";
        }

        public override void Initialize()
        {
            Attribute = Repository.GetContextObject() as Attribute;
        }
    }
}