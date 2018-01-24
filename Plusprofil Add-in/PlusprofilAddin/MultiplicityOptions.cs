using System.Collections.ObjectModel;

namespace PlusprofilAddin
{
	internal class MultiplicityOptions : ObservableCollection<string>
	{
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