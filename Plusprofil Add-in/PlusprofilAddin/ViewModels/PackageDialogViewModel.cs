using System.Collections.Generic;
using System.Collections.ObjectModel;
using EA;
using PlusprofilAddin.ViewModels.Commands;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	public class PackageDialogViewModel : DialogViewModel
	{
		private readonly List<PlusprofilTaggedValue> _toAddDanishTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "LabelDa"),
			Definitions.Find(ptv => ptv.Key == "CommentDa")
		};

		private readonly List<PlusprofilTaggedValue> _toAddEnglishTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "LabelEn"),
			Definitions.Find(ptv => ptv.Key == "CommentEn")
		};

		private readonly List<PlusprofilTaggedValue> _toAddModelMetadataTaggedValues = new List<PlusprofilTaggedValue>
		{
			Definitions.Find(ptv => ptv.Key == "Namespace"),
			Definitions.Find(ptv => ptv.Key == "NamespacePrefix"),
			Definitions.Find(ptv => ptv.Key == "Publisher"),
			Definitions.Find(ptv => ptv.Key == "Theme"),
			Definitions.Find(ptv => ptv.Key == "Modified"),
			Definitions.Find(ptv => ptv.Key == "VersionInfo"),
			Definitions.Find(ptv => ptv.Key == "ModelStatus"),
			Definitions.Find(ptv => ptv.Key == "ApprovalStatus"),
			Definitions.Find(ptv => ptv.Key == "LegalSourcePackage"),
			Definitions.Find(ptv => ptv.Key == "SourcePackage")
		};

		public Element PackageElement { get; set; }
		public Collection TaggedValues { get; set; }

		public string UMLNameValue { get; set; }
		public string AliasValue { get; set; }

		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> ModelMetadataViewmodelTaggedValues { get; set; }
		public List<dynamic> TaggedValuesList;

		public PackageDialogViewModel()
		{
			DanishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			EnglishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			ModelMetadataViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewmodelTaggedValue>>();
			TaggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<ViewmodelTaggedValue>();
			
			UMLNameValue = "";
			AliasValue = "";
		}

		public override void Initialize()
		{
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
					_toAddModelMetadataTaggedValues.Add(Definitions.Find(ptv => ptv.Key == "WasDerivedFrom"));
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

			// Add tagged values to list of ViewmodelTaggedValues
			AddTaggedValuesToViewmodelTaggedValues(_toAddDanishTaggedValues, TaggedValuesList, DanishViewmodelTaggedValues);
			AddTaggedValuesToViewmodelTaggedValues(_toAddEnglishTaggedValues, TaggedValuesList, EnglishViewmodelTaggedValues);
			AddTaggedValuesToViewmodelTaggedValues(_toAddModelMetadataTaggedValues, TaggedValuesList, ModelMetadataViewmodelTaggedValues);
		}
	}
}