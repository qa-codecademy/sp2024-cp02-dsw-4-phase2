using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropShipping.Shared.CustomExceptions.ProductCutsomException
{
    public class ProductDataException : Exception
    {
        public ProductDataException(string message) : base(message)
        {

        }
    }
}
