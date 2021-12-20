using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.ValidationRules
{
    internal class NotNullValidationRule : BaseValidationRule
    {
        public override bool IsValid(Dictionary<string, object> dict, string propName, out string failMessage)
        {
            failMessage = null;
            if (dict != null && dict.ContainsKey(propName) && dict[propName] == null)
            {
                failMessage = $"{propName} is null";
                return false;
            }

            return true;
        }
    }
}
