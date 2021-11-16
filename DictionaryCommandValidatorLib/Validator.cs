using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidatorLib
{
    class Validator
    {
        public List<ProppertyValidateSettings> ValidateSettings { get; set; }
        public bool Validate(Dictionary<string, object> dict, out string error)
        {
            error = null;
            foreach (var s in ValidateSettings)
            {
                if (s.ValidationFunc(dict))
                    continue;
                error = $"{s.PropertyName}:{s.InvalidPropertyMessage}";
                return false;
            }
            return true;
        }
    }

}
