using DictionaryCommandValidator.Validation;
using System;
using System.Collections.Generic;

namespace Api10CommandDataValidator
{
    class Program
    {
        static void Main(string[] args)
        {
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

            ValidationProperty ex = ValidationProperty.Exist("pollQuestions.answerOptions.notexist");
            ValidationProperty nn = ValidationProperty.NotNull("pollQuestions.innerNUll");
            ValidationProperty nn1 = ValidationProperty.NotNull("dict.thiIsNotNull");

            var res = Validator.Do(data, new ValidationProperty[] { ex }, out string mess);

            if (res)
                Console.WriteLine("command is valid");
            else
                Console.WriteLine(mess);

            Console.ReadKey();
        }
    }
}