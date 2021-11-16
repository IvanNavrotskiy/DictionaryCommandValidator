using System;
using System.Collections.Generic;

namespace DictionaryCommandValidatorLib
{
    public class ProppertyValidateSettings
    {
        public static ProppertyValidateSettings RequiredNotNull(string name) =>
            new ProppertyValidateSettings() { PropertyName = name, InvalidPropertyMessage = name, ValidationFunc = (dict) => { return dict.ContainsKey(name) && dict[name] != null; } };
        public string PropertyName;
        public Func<Dictionary<string, object>, bool> ValidationFunc;
        public string InvalidPropertyMessage;
    }

}
