using System;
using System.Collections.Generic;
using System.Linq;
using EA;
using PlusprofilAddin.Commands;
using PDefinitions = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	internal abstract class DialogViewModel
	{
		protected List<PlusprofilTaggedValue> ToAddDanishTaggedValues = new List<PlusprofilTaggedValue>
		{
			PDefinitions.PrefLabelDa,
			PDefinitions.AltLabelDa,
			PDefinitions.DeprecatedLabelDa,
			PDefinitions.DefinitionDa,
			PDefinitions.ExampleDa,
			PDefinitions.CommentDa,
			PDefinitions.ApplicationNoteDa
		};

		protected List<PlusprofilTaggedValue> ToAddEnglishTaggedValues = new List<PlusprofilTaggedValue>
		{
			PDefinitions.PrefLabelEn,
			PDefinitions.AltLabelEn,
			PDefinitions.DeprecatedLabelEn,
			PDefinitions.DefinitionEn,
			PDefinitions.ExampleEn,
			PDefinitions.CommentEn,
			PDefinitions.ApplicationNoteEn
		};

		protected List<PlusprofilTaggedValue> ToAddProvenanceTaggedValues = new List<PlusprofilTaggedValue>
		{
			PDefinitions.LegalSource,
			PDefinitions.Source,
			PDefinitions.IsDefinedBy,
			PDefinitions.WasDerivedFrom
		};

		public SaveCommand SaveCommand { get; set; }
		public CancelCommand CancelCommand { get; set; }
		public AddCommand AddCommand { get; set; }
		public RemoveCommand RemoveCommand { get; set; }

		public Repository Repository { get; set; }
		public abstract void Initialize();

		protected List<dynamic> RetrieveTaggedValues(List<dynamic> taggedValueList, string taggedValueName)
		{
			List<dynamic> result = new List<dynamic>();
			if (taggedValueList.Count == 0) throw new ArgumentException("Object has no tagged values");

			if (taggedValueList.First().ObjectType == (short) ObjectType.otRoleTag)
			{
				foreach (RoleTag rt in taggedValueList)
				{
					if (rt.Tag == taggedValueName) result.Add(rt);
				}
			}
			else
			{
				foreach (dynamic tv in taggedValueList)
				{
						if (tv.Name == taggedValueName) result.Add(tv);
				}
			}
			if (result.Count == 0) throw new ArgumentException($"No tagged value with name \"{taggedValueName}\" was found");
			return result;
		}
	}
}