using PlusprofilAddin.Model;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.ViewModels;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PlusprofilAddin
{
    class ElementDialogViewModel : DialogViewModel
    {
        public Element Element { get; set; }
        public Collection TaggedValues { get; set; }
        public List<dynamic> TaggedValuesList;


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
            TaggedValuesList = new List<dynamic>();

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
            //Retrieve all tagged values and store them in a list
            //Tagged values are stored in a list to avoid iterating Collections multiple times, which is very costly
            for (int i = 0; i < TaggedValues.Count; i++)
            {
                dynamic tv = TaggedValues.GetAt((short)i);
                TaggedValuesList.Add(tv);
            }
            //Declare List to hold result of list lookups
            List<dynamic> result;
            result = RetrieveTaggedValues(TaggedValuesList, "URI");
            URIValue = result[0].Value; //TODO: Create method to more elegantly retrieve object in single object list

            //Add all danish tagged values to list
            foreach(string s in ToAddDanishTaggedValues)
            {
                result = RetrieveTaggedValues(TaggedValuesList, s);
                foreach (dynamic tv in result) DanishTaggedValues.Add(new PlusprofilTaggedValue(tv.Name, tv.Value));
            }

            //Add all english tagged values to list
            foreach (string s in ToAddEnglishTaggedValues)
            {
                result = RetrieveTaggedValues(TaggedValuesList, s);
                foreach (dynamic tv in result) EnglishTaggedValues.Add(new PlusprofilTaggedValue(tv.Name, tv.Value));
            }
            //Add all provenance tagged values to list
            foreach (string s in ToAddProvenanceTaggedValues)
            {
                result = RetrieveTaggedValues(TaggedValuesList, s);
                foreach (dynamic tv in result) ProvenanceTaggedValues.Add(new PlusprofilTaggedValue(tv.Name, tv.Value));
            }
            //Add all stereotype-specific tagged values to list
            /*
            foreach (string s in ToAddDanishTaggedValues)
            {
                tv = TaggedValuesList[s];
                DanishTaggedValues.Add(new PlusprofilTaggedValue(tv.Name, tv.Value));
            }
            */
        }
    }
}