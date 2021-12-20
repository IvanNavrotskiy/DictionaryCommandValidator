using DictionaryCommandValidator;
using System;
using System.Collections.Generic;

namespace Api10CommandDataValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = new Dictionary<string, object>()
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

            ValidationProperty nn1 = ValidationProperty.NotNull("properties.second.third.fourth.fifth.name");

            var res = Validator.Do(command, new ValidationProperty[] { nn1 }, out string mess);

            if (res)
                Console.WriteLine("command is valid");
            else
                Console.WriteLine(mess);

            Console.ReadKey();
        }
    }
}