using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropShipping.Shared.CustomExceptions
{
    public class NoDataException : Exception
    {
        public NoDataException(string message) : base(message)
        {

        }
    }
}
