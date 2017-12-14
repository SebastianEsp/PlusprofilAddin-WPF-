using PlusprofilAddin.Model;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.ViewModels;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System;
using PDefinitions = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin
{
    class ElementDialogViewModel : DialogViewModel
    {
		protected List<PlusprofilTaggedValue> ToAddStereotypeTaggedValues = new List<PlusprofilTaggedValue>
		{
			PDefinitions.equivalentClass,
			PDefinitions.subClassOf
		};

		//TODO Individual: sameAs and type

		public Element Element { get; set; }
        public Collection TaggedValues { get; set; }
        public List<dynamic> TaggedValuesList;


        public string URIValue { get; set; }
        public string UMLValue { get; set; }
        public string AliasValue { get; set; }

        public ObservableCollection<DisplayedTaggedValue> DanishTaggedValues { get; set; }
        public ObservableCollection<DisplayedTaggedValue> EnglishTaggedValues { get; set; }
        public ObservableCollection<DisplayedTaggedValue> ProvenanceTaggedValues { get; set; }
        public ObservableCollection<DisplayedTaggedValue> StereotypeTaggedValues { get; set; }

        public ElementDialogViewModel()
        {
            DanishTaggedValues = new ObservableCollection<DisplayedTaggedValue>();
            EnglishTaggedValues = new ObservableCollection<DisplayedTaggedValue>();
            ProvenanceTaggedValues = new ObservableCollection<DisplayedTaggedValue>();
            StereotypeTaggedValues = new ObservableCollection<DisplayedTaggedValue>();
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
			//Finalize list of stereotype tags to add
			if(Element.Stereotype == "Individual")
			{
				ToAddStereotypeTaggedValues.Add(PDefinitions.sameAs);
				ToAddStereotypeTaggedValues.Add(PDefinitions.type);
			}


            //Retrieve all tagged values and store them in a list
            //Tagged values are stored in a list to avoid iterating Collections multiple times, which is very costly
			//In a future iteration of the addin, avoid iterating the collection even once, instead using Repository.SQLQuery to retrieve
			//an XML-formatted list of every Tagged Value where the owner ID is Element.ElementID
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
            foreach(PlusprofilTaggedValue ptv in ToAddDanishTaggedValues)
            {   
                result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
                foreach (dynamic tv in result) DanishTaggedValues.Add(new DisplayedTaggedValue(tv.Name, tv.Value));
            }
            //Add all english tagged values to list
            foreach (PlusprofilTaggedValue ptv in ToAddEnglishTaggedValues)
            {
                result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
                foreach (dynamic tv in result) EnglishTaggedValues.Add(new DisplayedTaggedValue(tv.Name, tv.Value));
            }
            //Add all provenance tagged values to list
            foreach (PlusprofilTaggedValue ptv in ToAddProvenanceTaggedValues)
            {
                result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
                foreach (dynamic tv in result) ProvenanceTaggedValues.Add(new DisplayedTaggedValue(tv.Name, tv.Value));
            }
            //Add all stereotype-specific tagged values to list
            foreach (PlusprofilTaggedValue ptv in ToAddStereotypeTaggedValues)
            {
                result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				foreach (dynamic tv in result) ProvenanceTaggedValues.Add(new DisplayedTaggedValue(tv.Name, tv.Value));
			}
        }
    }
}