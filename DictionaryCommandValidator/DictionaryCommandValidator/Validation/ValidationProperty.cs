using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    class ValidationProperty
    {
        private string _Message;
        public string[] PathArray { get; private set; }
        public int PathLength => PathArray?.Length ?? 0;
        public BaseValidationRule[] Rules { get; set; }

        public ValidationProperty(string[] pathArray)
        {
            PathArray = pathArray ?? new string[0];
        }

        public ValidationProperty(string path) : this(path?.Split("."))
        {

        }

        public ValidationProperty GetChildProperty()
        {
            return new ValidationProperty(PathArray.Skip(1).ToArray()) 
            { 
                Rules = this.Rules
            };
        }

        public bool IsValid(object obj, out string message)
        {
            var fail = Rules?.FirstOrDefault(c => !c.IsValid(obj, String.Join(".", PathArray), out _Message));
            message = _Message;
            return fail == null;
        }

        public static ValidationProperty Exist(string prop) => new ValidationProperty(prop) { Rules = new BaseValidationRule[] { new  PropertyExistValidationRule()} };
        public static ValidationProperty NotNull(string prop) => new ValidationProperty(prop) { Rules = new BaseValidationRule[] { new  PropertyNotNullValidationRule()} };
        public ValidationProperty[] ToArray() => new ValidationProperty[] { this };
    }
}
