﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EA;
using PlusprofilAddin.ViewModels.Commands;
using static PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	/// <summary>
	/// ViewModel used to update View (Window) state and retrieve user input to update Model (Sparx Systems Enterprise Architect) state.
	/// </summary>
	/// <inheritdoc />
	public class PackageDialogViewModel : DialogViewModel
	{
		private readonly List<dynamic> _taggedValuesList;

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

		/// <inheritdoc/>
		public PackageDialogViewModel()
		{
			DanishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			EnglishViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			ModelMetadataViewmodelTaggedValues = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			_taggedValuesList = new List<dynamic>();
			DeleteTaggedValues = new List<ViewModelTaggedValue>();
			
			UMLNameValue = "";
			AliasValue = "";
		}

		/// <summary>Sparx Systems Enterprise Architect object representing the package selected when the add-in is opened, i.e. <c>EA.Package.Element</c></summary>
		public Element PackageElement { get; set; }
		
		/// <summary>Sparx Systems Enterprise Architect object holding the tagged values of the package, i.e. <c>EA.Package.Element.TaggedValues</c>.</summary>
		public Collection TaggedValues { get; set; }

		/// <summary>String representing <c>EA.Package.Element.Name</c>.</summary>
		public string UMLNameValue { get; set; }
		
		/// <summary>String representing <c>EA.Package.Element.Alias</c>.</summary>
		public string AliasValue { get; set; }

		/// <summary>Collection of collection of <c>ViewModelTaggedValue</c>s that should be grouped in a single UI element.</summary>
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> ModelMetadataViewmodelTaggedValues { get; set; }

		/// <inheritdoc />
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
				var tv = TaggedValues.GetAt(i);
				_taggedValuesList.Add(tv);
			}

			// Add tagged values to list of ViewmodelTaggedValues
			DanishViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddDanishTaggedValues, _taggedValuesList);
			EnglishViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddEnglishTaggedValues, _taggedValuesList);
			ModelMetadataViewmodelTaggedValues = AddTaggedValuesToViewmodelTaggedValues(_toAddModelMetadataTaggedValues, _taggedValuesList);
		}

        public override void OnWindowClosing(object sender, CancelEventArgs e)
        {
            SaveCommand.Execute(new object[] { this, sender });
        }
    }
}