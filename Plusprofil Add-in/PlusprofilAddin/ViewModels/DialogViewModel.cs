using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels.Commands;

namespace PlusprofilAddin.ViewModels
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class DialogViewModel
	{
		public SaveCommand SaveCommand { get; set; }
		public CancelCommand CancelCommand { get; set; }
		public AddCommand AddCommand { get; set; }
		public RemoveCommand RemoveCommand { get; set; }
		public Repository Repository { get; set; }
		public ResourceDictionary ResourceDictionary { get; set; }

		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> DanishViewmodelTaggedValues { get; set; }
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> EnglishViewmodelTaggedValues { get; set; }
		public List<ViewModelTaggedValue> DeleteTaggedValues { get; set; }
		
		/// <summary>
		/// Sets properties where parameters are not available at object creation.
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		/// Given a <c>List</c> of tagged values and the name of the tagged value to retrieve, returns a sublist with tagged values that satisfy <c>TaggedValue.Name == taggedValueName</c>
		/// Due to the varying field names of tagged values, <c>EA.TaggedValue</c> and <c>EA.AttributeTag</c> are handled differently than <c>EA.RoleTag</c>.
		/// </summary>
		/// <param name="taggedValueList"></param>
		/// <param name="taggedValueName"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public static List<dynamic> RetrieveTaggedValues(List<dynamic> taggedValueList, string taggedValueName)
		{
			var result = new List<dynamic>();
			if (taggedValueList.Count == 0) throw new ArgumentException("Object has no tagged values");

			if (taggedValueList.First().ObjectType == (short) ObjectType.otRoleTag)
			{
				foreach (RoleTag rt in taggedValueList)
					if (rt.Tag == taggedValueName)
					{
						result.Add(rt);
					}
			}
			else
			{
				foreach (var tv in taggedValueList)
					if (tv.Name == taggedValueName)
					{
						result.Add(tv);
					}
			}
			
			return result.Count != 0
				? result
				: throw new ArgumentException($"No tagged value with name \"{taggedValueName}\" was found");
		}

		/// <summary>
		/// Populates a view ItemsSource that displays lists of <c>ViewmodelTaggedValue</c>.
		/// </summary>
		/// <param name="toAddList">A list defining what type of <c>ViewmodelTaggedValue</c> should be added to the list, based on <c>PlusprofilTaggedValue.Key</c>.</param>
		/// <param name="taggedValuesList">A <c>List</c> containing the tagged values of an object.</param>
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> AddTaggedValuesToViewmodelTaggedValues(List<PlusprofilTaggedValue> toAddList, List<dynamic> taggedValuesList)
		{
			var resultCollectionCollection = new ObservableCollection<ObservableCollection<ViewModelTaggedValue>>();
			foreach (var ptv in toAddList)
			{
				try
				{
					var result = RetrieveTaggedValues(taggedValuesList, ptv.Name);
					var resultCollection = new ObservableCollection<ViewModelTaggedValue>();
					foreach (var tv in result)
					{
						var vtv = new ViewModelTaggedValue(tv)
						{
							ResourceDictionary = ResourceDictionary,
							Key = ptv.Key
						};
						vtv.Initialize();
						resultCollection.Add(vtv);
					}
					resultCollectionCollection.Add(resultCollection);
				}
				catch (ArgumentException)
				{
					// No tagged values with name ptv.Name is found, thus the list is not added (do nothing)
					// TODO: Warn the user in a non-intrusive manner (i.e. do not use a MessageBox)
				}
			}
			return resultCollectionCollection;
		}
	}
}