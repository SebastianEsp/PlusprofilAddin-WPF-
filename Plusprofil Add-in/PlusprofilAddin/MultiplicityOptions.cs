using System.Collections.ObjectModel;

namespace PlusprofilAddin
{
	/// <summary>
	/// Class used as <c>ItemsSource</c> for <c>ComboBox</c> used for value <c>Cardinality</c> when editing attributes
	/// </summary>
	/// <inheritdoc />
	public class MultiplicityOptions : ObservableCollection<string>
	{
		/// <summary>
		/// Constructor adding the default multiplicities of the Plusprofil.
		/// </summary>
		/// <inheritdoc />
		/// TODO: Alternative solution would be to load the multiplicity options from an external file, e.g. the Plusprofil MDG, to increase flexibility.
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