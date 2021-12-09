using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    abstract class BaseValidationRule
    {
        public abstract bool IsValid(object obj, string prop, out string faiMessage);
    }
}
