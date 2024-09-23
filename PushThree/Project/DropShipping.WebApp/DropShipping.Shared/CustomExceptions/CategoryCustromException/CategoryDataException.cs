using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropShipping.Shared.CustomExceptions.CategoryCustromException
{
    public class CategoryDataException : Exception
    {
        public CategoryDataException(string message) : base(message)
        {

        }
    }
}
