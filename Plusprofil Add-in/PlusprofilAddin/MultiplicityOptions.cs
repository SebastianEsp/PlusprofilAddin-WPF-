using System.Collections.ObjectModel;

namespace PlusprofilAddin
{
	/// <summary>
	/// Class used as <c>ItemsSource</c> for <c>ComboBox</c> used for value <c>Cardinality</c> when editing attributes
	/// </summary>
	public class MultiplicityOptions : ObservableCollection<string>
	{
		/// <summary>
		/// Constructor adding the default multiplicities of the Plusprofil.
		/// TODO: In a future iteration, it may be worthwhile to load these from an external file, e.g. the Plusprofil MDG, to increase flexibility.
		/// </summary>
		public MultiplicityOptions()
		{
			Add("*");
			Add("0");
			Add("0..*");
			Add("0..1");
			Add("1");
			Add("1..");
			Add("1..*");
		}
	}
}