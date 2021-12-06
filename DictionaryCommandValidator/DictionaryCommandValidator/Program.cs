using DictionaryCommandValidator.Validation;
using System;
using System.Collections.Generic;
//using DictionaryCommandValidatorLib;

namespace Api10CommandDataValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            //CommandValidator validator = new();
            //validator
                //.AddSettings(PropertyValidateSettings.RequiredNotNull("nullable"))
                //.AddSettings(PropertyValidateSettings.RequiredNotEmptyString("emptyString"));

            var data = new Dictionary<string, object>()
            {
                ["id"] = 111,
                ["title"] = "Testing poll",
                ["nullable"] = null,
                ["emptyString"] = String.Empty,
                ["dict"] = new Dictionary<string, object>()
                {
                    ["thiIsNotNull"] = null
                },
                ["pollQuestions"] = new Dictionary<string, object>[]
                {
                    new Dictionary<string, object>()
                    {
                        ["text"] = "question1",
                        ["order"] = 0,
                        ["innerNUll"] = null,
                        ["boolean"] = true,
                        ["answerOptions"] = new Dictionary<string, object>[]
                        {
                            new Dictionary<string, object>()
                            {
                                ["text"] = "option1",
                                ["order"] = 0
                            },
                            new Dictionary<string, object>()
                            {
                                ["text"] = "option2",
                                ["order"] = 1
                            }
                        }
                    }
                }
            };

            Property ex = Property.Exist("pollQuestions.answerOptions.notexist");
            Property nn = Property.NotNull("pollQuestions.innerNUll");
            Property nn1 = Property.NotNull("dict.thiIsNotNull");
            string mess = null;
            Validator.Do(data, new Property[] { nn1 }, out mess);

            //validator.Validate(data, out string mess);
            Console.WriteLine(mess);

            Console.ReadKey();
        }
    }
}