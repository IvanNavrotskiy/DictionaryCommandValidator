using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace DictionaryCommandValidator.Tests
{
    [TestFixture]
    class PropertyNotNullValidationRule_Tests
    {
        [Test]
        public void PropertyNotNullValidationRule_FiveNestingLevel_Array_ValidationFailure()
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
                                {
                                    ["name"] = null
                                }
                            }
                        }
                    }
                }
            };

            var exProperty = ValidationProperty.NotNull("properties.second.third.fourth.fifth.name");
            ValidationResult.ValidationFailure_SingleProperty(command, exProperty, "properties.second.third.fourth.fifth.name is null");
        }

        [Test]
        public void PropertyNotNullValidationRule_FiveNestingLevel_Array_ValidationSuccess()
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
                                {
                                    ["name"] = String.Empty
                                }
                            }
                        }
                    }
                }
            };

            var exProperty = ValidationProperty.NotNull("properties.second.third.fourth.fifth.name");
            ValidationResult.ValidationSuccess_SingleProperty(command, exProperty);
        }
    }
}
