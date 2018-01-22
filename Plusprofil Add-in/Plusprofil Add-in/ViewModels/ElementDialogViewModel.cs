using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EA;
using PlusprofilAddin.Commands;
using PDefinitions = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	internal class ElementDialogViewModel : DialogViewModel
	{
		public List<dynamic> TaggedValuesList;

		protected List<PlusprofilTaggedValue> ToAddStereotypeTaggedValues = new List<PlusprofilTaggedValue>
		{
			
		};

		public ElementDialogViewModel()
		{
			DanishTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			EnglishTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			ProvenanceTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			StereotypeTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			TaggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<DisplayedTaggedValue>();

			URIValue = "";
			UMLNameValue = "";
			AliasValue = "";
		}

		public Element Element { get; set; }
		public Collection TaggedValues { get; set; }


		public string URIValue { get; set; }
		public string UMLNameValue { get; set; }
		public string AliasValue { get; set; }

		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> DanishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> EnglishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> ProvenanceTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> StereotypeTaggedValues { get; set; }
		public List<DisplayedTaggedValue> DeleteTaggedValues { get; set; }

		public override void Initialize()
		{
			SaveCommand = new SaveCommand();
			CancelCommand = new CancelCommand();
			AddCommand = new AddCommand();
			RemoveCommand = new RemoveCommand();
			Element = Repository.GetContextObject();
			UMLNameValue = Element.Name;
			AliasValue = Element.Alias;
			TaggedValues = Element.TaggedValues;

			//Finalize list of stereotype tags to add
			switch (Element.Stereotype)
			{
				case "OwlClass":
				case "RdfsClass":
					ToAddStereotypeTaggedValues.Add(PDefinitions.EquivalentClass);
					ToAddStereotypeTaggedValues.Add(PDefinitions.SubClassOf);
					break;
				case "Individual":
					ToAddStereotypeTaggedValues.Add(PDefinitions.SameAs);
					ToAddStereotypeTaggedValues.Add(PDefinitions.Type);
					break;
			}

			//Retrieve all tagged values and store them in a list
			//Tagged values are stored in a list to avoid iterating Collections multiple times, which is very costly
			//In a future iteration of the addin, avoid iterating the collection even once, instead using Repository.SQLQuery to retrieve
			//an XML-formatted list of every Tagged Value where the owner ID is Element.ElementID
			for (int i = 0; i < TaggedValues.Count; i++)
			{
				dynamic tv = TaggedValues.GetAt((short) i);
				TaggedValuesList.Add(tv);
			}

			//Declare List to hold result of list lookups
			List<dynamic> result;

			try
			{
				result = RetrieveTaggedValues(TaggedValuesList, "URI");
				URIValue = result.First().Value;
			}
			catch (ArgumentException e)
			{
				MessageBox.Show(e.Message);
			}

			//Add all danish tagged values to list
			foreach (PlusprofilTaggedValue ptv in ToAddDanishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				ObservableCollection<DisplayedTaggedValue> resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (dynamic tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				DanishTaggedValues.Add(resultList);
			}

			//Add all english tagged values to list
			foreach (PlusprofilTaggedValue ptv in ToAddEnglishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				ObservableCollection<DisplayedTaggedValue> resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (dynamic tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				EnglishTaggedValues.Add(resultList);
			}

			//Add all provenance tagged values to list
			foreach (PlusprofilTaggedValue ptv in ToAddProvenanceTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				ObservableCollection<DisplayedTaggedValue> resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (dynamic tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				ProvenanceTaggedValues.Add(resultList);
			}

			//Add all stereotype-specific tagged values to list
			foreach (PlusprofilTaggedValue ptv in ToAddStereotypeTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				ObservableCollection<DisplayedTaggedValue> resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (dynamic tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				StereotypeTaggedValues.Add(resultList);
			}
		}
	}
}