using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.ValidatorOld
{
    abstract class BaseValidationSettings
    {
        //protected string _PropertyName;
        //public BaseValidationSettings(string prop)
        //{
        //    _PropertyName = prop;
        //}

        public abstract bool IsConditionSatisfied(object obj, string path);
        public abstract string GetMessage(string path);

    }
}
