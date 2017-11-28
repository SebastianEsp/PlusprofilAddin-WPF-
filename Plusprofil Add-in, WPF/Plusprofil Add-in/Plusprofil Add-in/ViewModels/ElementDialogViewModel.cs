using PlusprofilAddin.Model;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.ViewModels;
using System.Windows;

namespace PlusprofilAddin
{
    class ElementDialogViewModel : DialogViewModel
    {
        public Element Element { get; set; }
        public Collection TaggedValues { get; set; }

        public string URIValue { get; set; }
        public string UMLValue { get; set; }
        public string AliasValue { get; set; }

        public ObservableCollection<PlusprofilTaggedValue> DanishTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> EnglishTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> ProvenanceTaggedValues { get; set; }
        public ObservableCollection<PlusprofilTaggedValue> StereotypeTaggedValues { get; set; }

        public ElementDialogViewModel()
        {
            DanishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            EnglishTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            ProvenanceTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();
            StereotypeTaggedValues = new ObservableCollection<PlusprofilTaggedValue>();

            URIValue = "";
            UMLValue = "";
            AliasValue = "";
        }

        public override void Initialize()
        {
            Element = Repository.GetContextObject();
            UMLValue = Element.Name;
            AliasValue = Element.Alias;
            TaggedValues = Element.TaggedValues;
            //TODO: Retrieve all tagged values in an efficient manner (i.e. do not use Collection.GetAt), store in a List<TaggedValue>

            TaggedValue tv = TaggedValues.GetByName("URI");

            URIValue = tv.Value;

            //Add all danish tagged values to list
            DanishTaggedValues.Add(new PlusprofilTaggedValue(tv.Name, tv.Value));

            //Add all english tagged values to list
            EnglishTaggedValues.Add(new PlusprofilTaggedValue(tv.Name, tv.Value));

            //Add all provenance tagged values to list
            ProvenanceTaggedValues.Add(new PlusprofilTaggedValue(tv.Name, tv.Value));

            //Add all stereotype-specific tagged values to list
            StereotypeTaggedValues.Add(new PlusprofilTaggedValue(tv.Name, tv.Value));
        }
    }
}