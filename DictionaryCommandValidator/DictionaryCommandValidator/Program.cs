using System;
using System.Collections.Generic;
using DictionaryCommandValidatorLib;

namespace Api10CommandDataValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandValidator validator = new();
            validator
                //.AddSettings(PropertyValidateSettings.RequiredNotNull("nullable"))
                .AddSettings(PropertyValidateSettings.RequiredNotEmptyString("emptyString"));

            var data = new Dictionary<string, object>()
            {
                ["id"] = 111,
                ["title"] = "Testing poll",
                ["nullable"] = null,
                ["emptyString"] = String.Empty,
                ["pollQuestions"] = new Dictionary<string, object>[]
                {
                    new Dictionary<string, object>()
                    {
                        ["text"] = "question1",
                        ["order"] = 0,
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


            validator.Validate(data, out string mess);
            Console.WriteLine(mess);

            Console.ReadKey();
        }
    }
}