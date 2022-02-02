using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSearch
{

    class Program
    {
        private const int CountPrintNumber = 3;

        /// <summary>
        /// Метод.
        /// </summary>
        /// <param name="args">Аргументы метода.</param>
        static void Main(string[] args)
        {


            var numbers = GetValidInput("Введите целое число", IsValidRange)
                    .Split(",")
                    .Select(i => Math.Abs(int.Parse(i)))
                    .OrderBy(i => i)
                    .ToList();

            var direction = GetValidInput("Веедите направленость \n0. Прямое направление \n1. Обратное направление", (template) => template.Trim().Equals("0") || template.Trim().Equals("1"));

            var asc = direction.Equals("0");

            var simpleNumbers = GetCollectionSimple(numbers, asc);

            if (simpleNumbers.Count() < 3)
            {
                Console.WriteLine($"Нашли всего {simpleNumbers.Count()}");
                Console.WriteLine($"Поробуйте другое число");
            }
            else
            {
                Print(simpleNumbers, asc);
            }

        }

        static string GetValidInput(string question, Func<string, bool> validate)
        {
            while (true)
            {
                Console.WriteLine(question);

                var userInput = Console.ReadLine();
                if (validate(userInput))
                {
                    return userInput;
                }

                Console.WriteLine("Неверный формат ввода");
            }
        }

        private static bool IsValidRange(string template)
        {
            if (String.IsNullOrWhiteSpace(template)) return false;

            var numbers = template.Split(",");

            if (numbers.Length > 2) return false;

            foreach (var number in numbers)
            {
                if (!int.TryParse(number, out _))
                {
                    return false;
                }

            }
            return true;
        }

        static bool IsSimpleNumber(int number)
        {
            if (number == 1) return false;

            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static IEnumerable<int> GetCollectionSimpleNumberByNumber(int number, bool direction)
        {
            int count = 0;
            List<int> resultCollection = new List<int>();

            while (count < CountPrintNumber)
            {
                if (!direction)
                {
                    number--;
                }
                else
                {
                    number++;
                }
                if (IsSimpleNumber(number))
                {
                    resultCollection.Add(number);
                    count++;
                }
            }
            return resultCollection;
        }

        static IEnumerable<int> GetCollectionSimpleNumberByNumber(int minNumber, int maxNumber, bool direction)
        {
            int count = 0;
            List<int> resultCollection = new List<int>();

            var number = direction ? minNumber : maxNumber;

            while (count < CountPrintNumber && number >= minNumber && number <= maxNumber)
            {
                if (!direction)
                {
                    number--;
                }
                else
                {
                    number++;
                }
                if (IsSimpleNumber(number))
                {

                    resultCollection.Add(number);
                    count++;
                }
            }
            return resultCollection;
        }

        static IEnumerable<int> GetCollectionSimple(List<int> numbers, bool directio)
        {
            if (numbers.Count() > 1)
            {
                return GetCollectionSimpleNumberByNumber(numbers[0], numbers[1], directio);
            }
            else
            {
                return GetCollectionSimpleNumberByNumber(numbers.ElementAt(0), directio);
            }
        }

        static void Print(IEnumerable<int> collectionSimpelNumber, bool directio)
        {
            for (int i = 0; i < collectionSimpelNumber.Count(); i++)
            {
                if (i == collectionSimpelNumber.Count() - 1)
                {
                    Console.Write($"{collectionSimpelNumber.ElementAt(i)}\n ");

                }
                else
                {
                    Console.Write($"{collectionSimpelNumber.ElementAt(i)}, ");
                }
            }
        }

    }
}
