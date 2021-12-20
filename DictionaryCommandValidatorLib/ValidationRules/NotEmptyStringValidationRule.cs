using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.ValidationRules
{
    class NotEmptyStringValidationRule : BaseValidationRule
    {
        public override bool IsValid(Dictionary<string, object> dict, string propName, out string failMessage)
        {
            failMessage = null;

            if (dict != null && dict.ContainsKey(propName))
            {
                var propValue = dict[propName] as string;
                if (propValue == null)
                {
                    failMessage = $"{propName} is not string";
                    return false;
                }

                if (String.IsNullOrEmpty(propValue))
                {
                    failMessage = $"{propName} is empty";
                    return false;
                }
            }

            return true;
        }
    }
}
