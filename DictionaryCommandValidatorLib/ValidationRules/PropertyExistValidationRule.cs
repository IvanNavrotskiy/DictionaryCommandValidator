using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.ValidationRules
{
    internal class PropertyExistValidationRule : BaseValidationRule
    {
        public override bool IsValid(Dictionary<string, object> dict, string propName, out string failMessage)
        {
            failMessage = null;
            if (dict != null && !dict.ContainsKey(propName))
            {
                failMessage = $"{propName} not exist";
                return false;
            }

            return true;
        }
    }
}
