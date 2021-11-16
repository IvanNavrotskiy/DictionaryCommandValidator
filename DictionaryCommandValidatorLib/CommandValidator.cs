using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidatorLib
{
    public class CommandValidator
    {
        public List<PropertyValidateSettings> ValidateSettings { get; set; }

        public CommandValidator AddSettings(PropertyValidateSettings settings)
        {
            ValidateSettings = ValidateSettings ?? new List<PropertyValidateSettings>();
            ValidateSettings.Add(settings);
            return this;
        }

        private static bool IsPropertyValid(PropertyValidateSettings settings, Dictionary<string, object> dict, out string error)
        {
            //
            error = null;
            //
            if (settings.IsPropertyRequired && !dict.ContainsKey(settings.PropertyName))
            {
                error = $"{settings.PropertyName} is not exist";
                return false;
            }

            //
            if (dict.TryGetValue(settings.PropertyName, out object value))
            {
                //
                var valueDict = value as Dictionary<string, object>;
                if (valueDict != null)
                    return IsPropertyValid(settings, valueDict, out error);

                //
                var valueArray = value as object[];
                if (valueArray != null)
                {
                    string innerError = null;
                    var res = valueArray.All(v => IsPropertyValid(settings.InnerProperty, v as Dictionary<string, object>, out innerError));
                    error = innerError;
                    return res;
                }

                //
                if (settings.InnerProperty != null && settings.InnerProperty?.ValidationFunc(dict) == false)
                {
                    error = settings.InvalidPropertyMessage;
                    return false;
                }

                //
                if (!settings?.ValidationFunc(dict) ?? false)
                {
                    error = settings.InvalidPropertyMessage;
                    return false;
                }
            }

            return true;
        }

        public bool Validate(Dictionary<string, object> dict, out string error)
        {
            string innerError = null;
            var res =  ValidateSettings.All(s => IsPropertyValid(s, dict, out innerError));
            error = innerError;

            return res;
        }
    }

}
