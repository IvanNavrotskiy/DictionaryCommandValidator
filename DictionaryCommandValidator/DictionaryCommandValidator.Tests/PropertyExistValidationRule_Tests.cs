using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace DictionaryCommandValidator.Tests
{
    [TestFixture]
    class PropertyExistValidationRule_Tests
    {
        [Test]
        public void FirstNestingLevel_ValidationSuccess()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["name"] = "insertCommand"
            };

            var exProperty = ValidationProperty.Exist("name");
            ValidationResult.ValidationSuccess_SingleProperty(command, exProperty);
        }

        [Test]
        public void FirstNestingLevel_ValidationFailure()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1
            };

            var exProperty = ValidationProperty.Exist("name");
            ValidationResult.ValidationFailure_SingleProperty(command, exProperty, "name not exist");
        }

        [Test]
        public void SecondNestingLevel_ValidationSuccess()
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
            ValidationResult.ValidationSuccess_SingleProperty(command, exProperty);
        }

        [Test]
        public void SecondNestingLevel_ValidationFailure()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
            };

            var exProperty = ValidationProperty.Exist("properties.name");
            ValidationResult.ValidationFailure_SingleProperty(command, exProperty, "properties.name not exist");
        }

        [Test]
        public void SecondNestingLevel_Array_ValidationSuccess()
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
            ValidationResult.ValidationSuccess_SingleProperty(command, exProperty);
        }

        [Test]
        public void SecondNestingLevel_Array_ValidationFailure()
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
            ValidationResult.ValidationFailure_SingleProperty(command, exProperty, "properties.name not exist");
        }

        [Test]
        public void FiveNestingLevel_Array_ValidationSuccess()
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
            ValidationResult.ValidationSuccess_SingleProperty(command, exProperty);
        }

        [Test]
        public void FiveNestingLevel_Array_ValidationFailure()
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
            ValidationResult.ValidationFailure_SingleProperty(command, exProperty, "properties.second.third.fourth.fifth.name not exist");
        }
    }
}
