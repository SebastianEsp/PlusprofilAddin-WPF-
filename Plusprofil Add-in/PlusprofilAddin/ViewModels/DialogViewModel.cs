using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EA;
using PlusprofilAddin.Commands;
using PDefinitions = PlusprofilAddin.PlusprofilTaggedValueDefinitions;

namespace PlusprofilAddin.ViewModels
{
	public abstract class DialogViewModel
	{
		public SaveCommand SaveCommand { get; set; }
		public CancelCommand CancelCommand { get; set; }
		public AddCommand AddCommand { get; set; }
		public RemoveCommand RemoveCommand { get; set; }
		public Repository Repository { get; set; }
		public ResourceDictionary ResourceDictionary { get; set; }

		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> DanishTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> EnglishTaggedValues { get; set; }
		public List<ViewmodelTaggedValue> DeleteTaggedValues { get; set; }

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
		public override string ToString()
		{
			return $"Type: {GetType()}";
		}
	}
}