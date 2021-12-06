using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    abstract class BaseValidationRule
    {
        public abstract bool ItFollows(object obj, string prop, out string faiMessage);
        //public abstract bool IsSatisfy(object obj, string prop);
        //public abstract string FailMessage(string prop);
    }
}
