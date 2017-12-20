using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusprofilAddin
{
    class DisplayedTaggedValue
    {
		public dynamic TaggedValue { get; }
        public string Name { get; set; }
        public string Value { get; set; }

        public DisplayedTaggedValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

		public DisplayedTaggedValue(dynamic taggedValue){
			TaggedValue = taggedValue;
		}

        
        public override String ToString()
        {
            return "DisplayTaggedValue with name: " + Name;
        }
    }
}