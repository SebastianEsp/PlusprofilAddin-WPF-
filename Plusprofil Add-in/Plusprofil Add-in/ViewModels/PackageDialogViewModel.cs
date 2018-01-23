using System.Collections.Generic;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.Commands;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	public class PackageDialogViewModel : DialogViewModel
	{
		private readonly List<PlusprofilTaggedValue> _toAddDanishTaggedValues = new List<PlusprofilTaggedValue>
		{
			LabelDa,
			CommentDa
		};

		private readonly List<PlusprofilTaggedValue> _toAddEnglishTaggedValues = new List<PlusprofilTaggedValue>
		{
			LabelDa,
			CommentDa
		};

		private readonly List<PlusprofilTaggedValue> _toAddModelMetadataTaggedValues = new List<PlusprofilTaggedValue>
		{
			Namespace,
			NamespacePrefix,
			Publisher,
			Theme,
			Modified,
			VersionInfo,
			ModelStatus,
			ApprovalStatus,
			LegalSource,
			Source
		};

		public Element PackageElement { get; set; }
		public Collection TaggedValues { get; set; }

		public string UMLNameValue { get; set; }
		public string AliasValue { get; set; }

		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> DanishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> EnglishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<DisplayedTaggedValue>> ModelMetadataTaggedValues { get; set; }
		public List<dynamic> TaggedValuesList;
		public List<DisplayedTaggedValue> DeleteTaggedValues { get; set; }

		public PackageDialogViewModel()
		{
			DanishTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			EnglishTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			ModelMetadataTaggedValues = new ObservableCollection<ObservableCollection<DisplayedTaggedValue>>();
			TaggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<DisplayedTaggedValue>();
			
			UMLNameValue = "";
			AliasValue = "";
		}

		public override void Initialize()
		{
			// TODO: LegalSource and Source should be treated as if having a <memo> field
			// TODO: when dealing with packages
			SaveCommand = new SaveCommand();
			CancelCommand = new CancelCommand();
			AddCommand = new AddCommand();
			RemoveCommand = new RemoveCommand();
			PackageElement = Repository.GetContextObject().Element;
			UMLNameValue = PackageElement.Name;
			AliasValue = PackageElement.Alias;
			TaggedValues = PackageElement.TaggedValues;

			//Finalize list of stereotype tags to add
			switch (PackageElement.Stereotype)
			{
				case "LogicalModel":
				case "CoreModel":
				case "ApplicationModel":
				case "Vocabulary":
				case "ApplicationProfile":
					_toAddModelMetadataTaggedValues.Add(WasDerivedFrom);
					break;
			}

			//Retrieve all tagged values and store them in a list
			//Tagged values are stored in a list to avoid iterating Collections multiple times, which is very costly
			//In a future iteration of the addin, avoid iterating the collection even once, instead using Repository.SQLQuery to retrieve
			//an XML-formatted list of every Tagged Value where the owner ID is PackageElement.ElementID
			for (short i = 0; i < TaggedValues.Count; i++)
			{
				dynamic tv = TaggedValues.GetAt(i);
				TaggedValuesList.Add(tv);
			}

			//Declare List to hold result of list lookups
			List<dynamic> result;

			//Add all Danish tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddDanishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (TaggedValue tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				DanishTaggedValues.Add(resultList);
			}

			//Add all English tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddEnglishTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (TaggedValue tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				EnglishTaggedValues.Add(resultList);
			}

			//Add all model metadata-specific tagged values to list
			foreach (PlusprofilTaggedValue ptv in _toAddModelMetadataTaggedValues)
			{
				result = RetrieveTaggedValues(TaggedValuesList, ptv.Name);
				var resultList = new ObservableCollection<DisplayedTaggedValue>();
				foreach (TaggedValue tv in result) resultList.Add(new DisplayedTaggedValue(tv));
				ModelMetadataTaggedValues.Add(resultList);
			}
		}
	}
}