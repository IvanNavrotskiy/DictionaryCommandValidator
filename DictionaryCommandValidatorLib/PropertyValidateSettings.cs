using System;
using System.Collections.Generic;
using System.Linq;

namespace DictionaryCommandValidatorLib
{
    public class PropertyValidateSettings
    {
        public PropertyValidateSettings(string name) : this(name, false)
        {

        }

        public PropertyValidateSettings(string name, bool isRequired)
        {
            PropertyName = name; IsPropertyRequired = isRequired;
        }

        public static PropertyValidateSettings Required(string name) => new PropertyValidateSettings(name, true);

        public static PropertyValidateSettings RequiredDigitsOnly(string name) => 
            new PropertyValidateSettings(name, true)
            {
                InvalidPropertyMessage = $"{name} must contain numeric simbols only",
                ValidationFunc = (dict) => dict[name]?.ToString().All(Char.IsDigit) ?? false
            };

        public static PropertyValidateSettings RequiredNotNull(string name) => 
            new PropertyValidateSettings(name, true) 
            { 
                InvalidPropertyMessage = $"{name} is null", 
                ValidationFunc = (dict) => dict[name] != null 
            };

        public static PropertyValidateSettings RequiredNotEmptyString(string name) =>
            new PropertyValidateSettings(name, true)
            {
                InvalidPropertyMessage = $"{name} is empty",
                ValidationFunc = (dict) => !String.IsNullOrEmpty(dict[name]?.ToString())
            };

        public bool IsPropertyRequired;
        public string PropertyName;
        public Func<Dictionary<string, object>, bool> ValidationFunc;
        public string InvalidPropertyMessage;
        public PropertyValidateSettings InnerProperty;
    }

}
