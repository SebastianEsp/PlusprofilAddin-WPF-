using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlusprofilAddin.ViewModels
{
	public class ConnectorUserControlViewModel : DialogViewModel
	{
		public string UMLNameValue { get; set; }
		public string URIValue { get; set; }
		public string AliasValue { get; set; }
		public string MultiplicityValue { get; set; }

		public ConnectorUserControlViewModel()
		{
			UMLNameValue = "Placeholder1";
			URIValue = "Placeholder2";
			AliasValue = "Placeholder3";
			MultiplicityValue = "Placeholder4";
		}

		public override void Initialize()
		{
			
		}
	}
}
