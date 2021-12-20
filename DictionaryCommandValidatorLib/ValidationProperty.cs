using DictionaryCommandValidator.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DictionaryCommandValidator
{
    public class ValidationProperty : ICloneable
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

        public bool IsValid(Dictionary<string, object> dict, out string message)
        {
            var fail = Rules?.FirstOrDefault(c => !c.IsValid(dict, String.Join(".", PathArray), out _Message));
            message = _Message;
            return fail == null;
        }

        public object Clone()
        {
            return (ValidationProperty)this.MemberwiseClone();
        }

        public void ShiftPath()
        {
            PathArray = PathArray.Skip(1).ToArray();
        }

        public static ValidationProperty Exist(string prop) => 
            new ValidationProperty(prop) { Rules = new BaseValidationRule[] { new  PropertyExistValidationRule()} };

        public static ValidationProperty NotNull(string prop) => 
            new ValidationProperty(prop) { Rules = new BaseValidationRule[] { new  NotNullValidationRule()} };

        public static ValidationProperty NotEmptyString(string prop) => 
            new ValidationProperty(prop) { Rules = new BaseValidationRule[] { new NotEmptyStringValidationRule() } };

        public ValidationProperty[] ToArray() => new ValidationProperty[] { this };

    }
}
