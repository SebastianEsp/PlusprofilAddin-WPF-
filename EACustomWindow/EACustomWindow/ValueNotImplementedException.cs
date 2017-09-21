using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EACustomWindow
{
    [Serializable]
    class ValueNotFoundException : Exception
    {
        public ValueNotFoundException()
        {
        }

        public ValueNotFoundException(string message) : base(message)
        {
        }

        public ValueNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
