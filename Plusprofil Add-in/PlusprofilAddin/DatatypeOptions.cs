using System.Collections.ObjectModel;

namespace PlusprofilAddin
{
	/// <summary>
	/// Class used as <c>ItemsSource</c> for <c>ComboBox</c> used for value <c>Type</c> when editing attributes
	/// </summary>
	internal class DatatypeOptions : ObservableCollection<string>
	{
		/// <summary>
		/// Constructor adding the datatypes supported by the Plusprofil.
		/// TODO: In a future iteration, it may be worthwhile to load these from an external file, e.g. the Plusprofil MDG, to increase flexibility.
		/// </summary>
		internal DatatypeOptions()
		{
			Add("rdfs:Literal");
			Add("rdf:XMLLiteral");
			Add("owl:real");
			Add("owl:rational");
			Add("xsd:decimal");
			Add("xsd:integer");
			Add("xsd:long");
			Add("xsd:int");
			Add("xsd:short");
			Add("xsd:byte");
			Add("xsd:nonNegativeInteger");
			Add("xsd:nonPositiveInteger");
			Add("xsd:positiveInteger");
			Add("xsd:negativeInteger");
			Add("xsd:unsignedLong");
			Add("xsd:unsignedInt");
			Add("xsd:unsignedShort");
			Add("xsd:unsignedByte");
			Add("xsd:double");
			Add("xsd:float");
			Add("rdf:plainLiteral");
			Add("xsd:string");
			Add("rdf:langString");
			Add("xsd:normalizedString");
			Add("xsd:token");
			Add("xsd:language");
			Add("xsd:Name");
			Add("xsd:NCName");
			Add("xsd:NMTOKEN");
			Add("xsd:boolean");
			Add("xsd:hexBinary");
			Add("base64Binary");
			Add("xsd:anyUri");
			Add("xsd:dateTime");
			Add("xsd:dateTimeStamp");
			Add("xsd:date");
			Add("xsd:time");
		}
	}
}