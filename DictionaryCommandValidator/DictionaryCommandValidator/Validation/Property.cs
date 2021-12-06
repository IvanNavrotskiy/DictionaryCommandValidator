using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    class Property
    {
        private string _Message;
        public string Path;

        public BaseValidationRule[] Conditions { get; set; }

        public bool IsValid(object obj, out string message)
        {
            var fail = Conditions?.FirstOrDefault(c => !c.ItFollows(obj, Path, out _Message));
            message = _Message;
            return fail != null;         
        }

        public static Property Exist(string prop) => new Property() { Path = prop, Conditions = new BaseValidationRule[] { new  PropertyExistValidationRule()} };
        public static Property NotNull(string prop) => new Property() { Path = prop, Conditions = new BaseValidationRule[] { new  PropertyNotNullValidationRule()} };
        public Property[] ToArray() => new Property[] { this };
    }
}
