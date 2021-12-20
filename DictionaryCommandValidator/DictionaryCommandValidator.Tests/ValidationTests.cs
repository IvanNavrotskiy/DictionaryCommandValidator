using NUnit.Framework;
using System;
using System.Collections.Generic;
using DictionaryCommandValidator;

namespace DictionaryCommandValidator.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void Validator_ValidInput()
        {
            Assert.IsTrue(Validator.Do(new object(), new ValidationProperty[0], out string message));
        }

        [Test]
        public void Validator_InvalidInput_Command()
        {
            Assert.Throws<ArgumentNullException>(() => Validator.Do(null, new ValidationProperty[0], out string message));
        }

        [Test]
        public void Validator_InvalidInput_Properties()
        {
            Assert.Throws<ArgumentNullException>(() => Validator.Do(new Dictionary<string, object>(), null, out string message));
        }

        [Test]
        public void PropertyExistValidationRule_FirstNestingLevel_ValidationSuccess()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["name"] = "insertCommand"
            };

            var exProperty = ValidationProperty.Exist("name");
            Assert.IsTrue(Validator.Do(command, exProperty.ToArray(), out string message));            
        }

        [Test]
        public void PropertyExistValidationRule_FirstNestingLevel_ValidationFailure()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1
            };

            var exProperty = ValidationProperty.Exist("name");
            Assert.IsFalse(Validator.Do(command, exProperty.ToArray(), out string message));
            Assert.AreEqual(message, "name not exist");
        }

        [Test]
        public void PropertyExistValidationRule_SecondNestingLevel_ValidationSuccess()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
                {
                    ["name"] = "insertCommand"
                }
            };

            var exProperty = ValidationProperty.Exist("properties.name");
            Assert.IsTrue(Validator.Do(command, exProperty.ToArray(), out string message));
            Assert.IsNull(message);
        }

        [Test]
        public void PropertyExistValidationRule_SecondNestingLevel_ValidationFailure()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
            };

            var exProperty = ValidationProperty.Exist("properties.name");
            Assert.IsFalse(Validator.Do(command, exProperty.ToArray(), out string message));
            Assert.AreEqual(message, "properties.name not exist");
        }

        [Test]
        public void PropertyExistValidationRule_SecondNestingLevel_Array_ValidationSuccess()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>[]
                {
                    new Dictionary<string, object>()
                    {
                        ["name"] = "insertCommand"
                    }
                }
            };

            var exProperty = ValidationProperty.Exist("properties.name");
            Assert.IsTrue(Validator.Do(command, exProperty.ToArray(), out string message));
            Assert.IsNull(message);
        }

        [Test]
        public void PropertyExistValidationRule_SecondNestingLevel_Array_ValidationFailure()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>[]
                {
                    new Dictionary<string, object>()
                }
            };

            var exProperty = ValidationProperty.Exist("properties.name");
            Assert.IsFalse(Validator.Do(command, exProperty.ToArray(), out string message));
            Assert.AreEqual(message, "properties.name not exist");
        }

        [Test]
        public void PropertyExistValidationRule_FiveNestingLevel_Array_ValidationSuccess()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
                {
                    ["second"] = new Dictionary<string, object>()
                    {
                        ["third"] = new Dictionary<string, object>()
                        {
                            ["fourth"] = new Dictionary<string, object>()
                            {
                                ["fifth"] = new Dictionary<string, object>() { ["name"] = "insertCommand" }
                            }
                        }
                    }
                }
            };

            var exProperty = ValidationProperty.Exist("properties.second.third.fourth.fifth.name");
            Assert.IsTrue(Validator.Do(command, exProperty.ToArray(), out string message));
            Assert.IsNull(message);
        }

        [Test]
        public void PropertyExistValidationRule_FiveNestingLevel_Array_ValidationFailure()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
                {
                    ["second"] = new Dictionary<string, object>()
                    {
                        ["third"] = new Dictionary<string, object>()
                        {
                            ["fourth"] = new Dictionary<string, object>()
                            {
                                ["fifth"] = new Dictionary<string, object>()
                            }
                        }
                    }
                }
            };

            var exProperty = ValidationProperty.Exist("properties.second.third.fourth.fifth.name");
            Assert.IsFalse(Validator.Do(command, exProperty.ToArray(), out string message));
            Assert.AreEqual(message, "properties.second.third.fourth.fifth.name not exist");
        }
    }
}
