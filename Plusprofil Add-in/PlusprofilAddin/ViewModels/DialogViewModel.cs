using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels.Commands;

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

		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> DanishViewmodelTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> EnglishViewmodelTaggedValues { get; set; }
		public List<ViewmodelTaggedValue> DeleteTaggedValues { get; set; }

		public abstract void Initialize();

		protected List<dynamic> RetrieveTaggedValues(List<dynamic> taggedValueList, string taggedValueName)
		{
			var result = new List<dynamic>();
			if (taggedValueList.Count == 0) throw new ArgumentException("Object has no tagged values");

			if (taggedValueList.First().ObjectType == (short) ObjectType.otRoleTag)
			{
				foreach (RoleTag rt in taggedValueList)
					if (rt.Tag == taggedValueName)
					{
						result.Add(rt);
						break;
					}
			}
			else
			{
				foreach (dynamic tv in taggedValueList)
					if (tv.Name == taggedValueName)
					{
						result.Add(tv);
						break;
					}
			}

			return result.Count != 0
				? result
				: throw new ArgumentException($"No tagged value with name \"{taggedValueName}\" was found");
		}
		public override string ToString()
		{
			return $"Type: {GetType()}";
		}

		public void AddTaggedValuesToViewmodelTaggedValues(List<PlusprofilTaggedValue> toAddList, List<dynamic> taggedValuesList, ObservableCollection<ObservableCollection<ViewmodelTaggedValue>> viewmodelTaggedValues)
		{
			foreach (PlusprofilTaggedValue ptv in toAddList)
			{
				try
				{
					List<dynamic> result = RetrieveTaggedValues(taggedValuesList, ptv.Name);
					var resultList = new ObservableCollection<ViewmodelTaggedValue>();
					foreach (dynamic tv in result)
					{
						var vtv = new ViewmodelTaggedValue(tv)
						{
							ResourceDictionary = ResourceDictionary,
							Key = ptv.Key
						};
						vtv.Initialize();
						resultList.Add(vtv);
					}
					viewmodelTaggedValues.Add(resultList);
				}
				catch (ArgumentException e)
				{
					// No tagged values with name ptv.Name is found, thus the list is not added (do nothing)
					// TODO: Warn the user in a non-intrusive manner (i.e. do not use a MessageBox)
				}
			}
		}
	}
}