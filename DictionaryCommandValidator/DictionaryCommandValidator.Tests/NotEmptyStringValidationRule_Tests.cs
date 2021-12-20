using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DictionaryCommandValidator.Tests
{
    [TestFixture]
    public class NotEmptyStringValidationRule_Tests
    {
        [Test]
        public void FiveNestingLevel_Array_ValidationFailure_NotString()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
                {
                    ["second"] = new Dictionary<string, object>()
                    {
                        // array
                        ["third"] = new Dictionary<string, object>[]
                        {
                            new Dictionary<string, object>()
                            {
                                ["fourth"] = new Dictionary<string, object>() 
                                {
                                    ["fifth"] = new Dictionary<string, object>() 
                                    {
                                        ["name"] = 123
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var exProperty = ValidationProperty.NotEmptyString("properties.second.third.fourth.fifth.name");
            ValidationResult.ValidationFailure_SingleProperty(command, exProperty, "properties.second.third.fourth.fifth.name is not string");
        }

        [Test]
        public void FiveNestingLevel_Array_ValidationFailure_Null()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
                {
                    ["second"] = new Dictionary<string, object>()
                    {
                        // array
                        ["third"] = new Dictionary<string, object>[]
                        {
                            new Dictionary<string, object>()
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
                }
            };

            var exProperty = ValidationProperty.NotEmptyString("properties.second.third.fourth.fifth.name");
            ValidationResult.ValidationFailure_SingleProperty(command, exProperty, "properties.second.third.fourth.fifth.name is not string");
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
                        // array
                        ["third"] = new Dictionary<string, object>[]
                        {
                            new Dictionary<string, object>()
                            {
                                ["fourth"] = new Dictionary<string, object>()
                                {
                                    ["fifth"] = new Dictionary<string, object>()
                                    {
                                        ["name"] = "commandName"
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var property = ValidationProperty.NotEmptyString("properties.second.third.fourth.fifth.name");
            ValidationResult.ValidationSuccess_SingleProperty(command, property);
        }

        [Test]
        public void FiveNestingLevel_InnerArray_ValidationSuccess()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
                {
                    ["second"] = new Dictionary<string, object>()
                    {
                        // array
                        ["third"] = new Dictionary<string, object>[]
                        {
                            new Dictionary<string, object>()
                            {
                                ["fourth"] = new Dictionary<string,object>[]
                                {
                                    new Dictionary<string, object>() 
                                    {
                                        ["fifth"] = new Dictionary<string, object>()
                                        {
                                            ["name"] = "commandName"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var property = ValidationProperty.NotEmptyString("properties.second.third.fourth.fifth.name");
            ValidationResult.ValidationSuccess_SingleProperty(command, property);
        }

        [Test]
        public void FiveNestingLevel_InnerArray_ValidationFailure_NotString()
        {
            var command = new Dictionary<string, object>()
            {
                ["user"] = "admin",
                ["controllerId"] = 1,
                ["properties"] = new Dictionary<string, object>()
                {
                    ["second"] = new Dictionary<string, object>()
                    {
                        // array
                        ["third"] = new Dictionary<string, object>[]
                        {
                            new Dictionary<string, object>()
                            {
                                ["fourth"] = new Dictionary<string,object>[]
                                {
                                    new Dictionary<string, object>()
                                    {
                                        ["fifth"] = new Dictionary<string, object>()
                                        {
                                            ["name"] = 123
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var property = ValidationProperty.NotEmptyString("properties.second.third.fourth.fifth.name");
            ValidationResult.ValidationFailure_SingleProperty(command, property, "properties.second.third.fourth.fifth.name is not string");
        }
    }
}
