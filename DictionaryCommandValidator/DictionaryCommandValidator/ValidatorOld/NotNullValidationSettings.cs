using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.ValidatorOld
{
    class NotNullValidationSettings : BaseValidationSettings
    {
        //public NotNullValidationSettings(string prop) : base(prop)
        //{

        //}

        public override string GetMessage(string path) => $"{path} can not be null";

        public override bool IsConditionSatisfied(object obj, string path)
        {
            var dict = obj as Dictionary<string, object>;
            // проверяем только на null, проверка наличия атроибута в другом объекте
            if (dict != null && dict.ContainsKey(path))
                return dict[path] != null;

            return true;
        }
    }
}
