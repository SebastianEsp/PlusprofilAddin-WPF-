using System.Collections.ObjectModel;

namespace PlusprofilAddin
{
	internal class MultiplicityOptions : ObservableCollection<string>
	{
		public MultiplicityOptions()
		{
			Add("0..0");
			Add("0..1");
			Add("1..1");
			Add("0..*");
			Add("1..*");
		}
	}
}