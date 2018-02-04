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
	/// ViewModel used to update View (Window) state and retrieve user input to update Model (Sparx Systems Enterprise Architect) state.<para/>
	/// ViewModel superclass containing functionality shared by every type of subclass ViewModels.
	/// </summary>
	public abstract class DialogViewModel
	{
		/// <summary><c>SaveCommand</c> used to register changes.</summary>
		public SaveCommand SaveCommand { get; set; }
		
		/// <summary><c>CancelCommand</c> used to close the window without making changes.</summary>
		public CancelCommand CancelCommand { get; set; }
		
		/// <summary><c>AddCommand</c> used to add new <c>ViewModelTaggedValue</c>s to the ViewModel.</summary>
		public AddCommand AddCommand { get; set; }
		
		/// <summary><c>RemoveCommand</c> used to move <c>ViewModelTaggedValue</c>s to <c>DeleteTaggedValues</c>.</summary>
		public RemoveCommand RemoveCommand { get; set; }
		
		/// <summary><c>EA.Repository</c> used to communicate with Sparx Systems Enterprise Architect through the Enterprise Architect Automation Interface.</summary>
		public Repository Repository { get; set; }
		
		/// <summary><c>ResourceDictionary</c> used to localize strings.</summary>
		public ResourceDictionary ResourceDictionary { get; set; }

		/// <summary>Collection of collection of <c>ViewModelTaggedValues</c> that should be grouped in a single UI element.</summary>
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> DanishViewmodelTaggedValues { get; set; }
		
		/// <summary>Collection of collection of <c>ViewModelTaggedValues</c> that should be grouped in a single UI element.</summary>
		public ObservableCollection<ObservableCollection<ViewModelTaggedValue>> EnglishViewmodelTaggedValues { get; set; }
		
		/// <summary><c>List</c> of <c>ViewModelTaggedValue</c>s that should be deleted from Sparx Systems Enterprise Architect if <c>SaveCommand.Execute()</c> is called.</summary>
		public List<ViewModelTaggedValue> DeleteTaggedValues { get; set; }
		
		/// <summary>
		/// Sets properties where parameters are not available at object creation.
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		/// Given a <c>List</c> of tagged values and the name of the tagged value to retrieve, returns a sublist with tagged values that satisfy <c>TaggedValue.Name == taggedValueName</c>.<para/>
		/// Due to the varying field names of tagged values, <c>EA.TaggedValue</c> and <c>EA.AttributeTag</c> are handled differently than <c>EA.RoleTag</c>.
		/// </summary>
		/// <param name="taggedValueList"><c>List</c> containing every tagged value of the object selected when the add-in was created.</param>
		/// <param name="taggedValueName">Name of the tagged value to retrieve as it is represented in Sparx Systems Enterprise Architect.</param>
		/// <returns>Returns a <c>List</c> of every tagged values with name equal to <c>taggedValueName</c>.</returns>
		/// <exception cref="ArgumentException">Thrown if the selected object has no tagged values or no tagged values with name equal to <c>taggedValueName</c>.</exception>
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