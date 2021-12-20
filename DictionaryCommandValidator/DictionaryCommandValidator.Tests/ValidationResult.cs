using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Tests
{
    class ValidationResult
    {
        public static void ValidationSuccess_SingleProperty(Dictionary<string, object> command, ValidationProperty property)
        {
            ValidationSuccess_MultiplyProperties(command, property.ToArray());
        }

        public static void ValidationSuccess_MultiplyProperties(Dictionary<string, object> command, ValidationProperty[] properies)
        {
            Assert.IsTrue(Validator.Do(command, properies, out string message));
            Assert.IsNull(message);
        }

        public static void ValidationFailure_SingleProperty(Dictionary<string, object> command, ValidationProperty property, string requiredMessage)
        {
            ValidationFailure_MultiplyProperties(command, property.ToArray(), requiredMessage);
        }

        public static void ValidationFailure_MultiplyProperties(Dictionary<string, object> command, ValidationProperty[] properties, string requiredMessage)
        {
            Assert.IsFalse(Validator.Do(command, properties, out string message));
            Assert.AreEqual(message, requiredMessage);
        }
    }
}
