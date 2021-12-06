using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.ValidatorOld
{
    class PropertyExistValidationSettings : BaseValidationSettings
    {
        public PropertyExistValidationSettings(string  prop)
        {

        }

        public override string GetMessage(string path) => $"{path} is not exist";

        public override bool IsConditionSatisfied(object obj, string path)
        {
            var dict = obj as Dictionary<string, object>;

            if (dict != null)
                return dict.ContainsKey(path);

            return true;
        }
    }
}
