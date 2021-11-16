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
            Console.ReadKey();
        }
    }
}