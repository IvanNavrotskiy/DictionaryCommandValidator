using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    class PropertyNotNullValidationRule : BaseValidationRule
    {
        public override bool IsValid(Dictionary<string, object> dict, string propName, out string faiMessage)
        {
            faiMessage = null;
            if (dict != null && dict.ContainsKey(propName) && dict[propName] == null)
            {
                faiMessage = $"{propName} is null";
                return false;
            }

            return true;
        }
    }
}
