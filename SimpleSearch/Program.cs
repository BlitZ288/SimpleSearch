using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSearch
{
    class Program
    {
        const int countPrintNumber = 3;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Веедите целлое число");
                try
                {
                    string[] userChoice = Console.ReadLine().Trim().Split(",");

                    if (userChoice.Length > 2 || Convert.ToInt32(userChoice.ElementAt(0)) > Convert.ToInt32(userChoice.ElementAt(userChoice.Length - 1))) throw new ArgumentException();

                    List<int> rangeNumber = userChoice.Select(c => Convert.ToInt32(c)).ToList();

                    Console.WriteLine("Веедите направленость");
                    Console.WriteLine("0. Прямое направление  ");
                    Console.WriteLine("1. Обратное направление");

                    int choicedirectio = Convert.ToInt32(Console.ReadLine());

                    if (choicedirectio != 0 && choicedirectio != 1)
                    {
                        throw new FormatException();
                    }

                    bool directio = choicedirectio != 0;

                    IEnumerable<int> collectionSimpelNuber = new List<int>();


                    collectionSimpelNuber = GetCollectionSimpleNumbers(rangeNumber, directio);


                    int sizeCollection = collectionSimpelNuber.Count();

                    if (sizeCollection < countPrintNumber)
                    {
                        Console.WriteLine($"Найдено всего {sizeCollection}  \n");
                        Console.WriteLine($"Поробуйте ввести другое число");
                        continue;
                    }

                    Print(collectionSimpelNuber, directio);

                }
                catch (FormatException)
                {
                    Console.WriteLine("Неверный формат ввода \n");
                    continue;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Неверный формат ввода убедитись , что вы вели два числа через ',' и первое число меньше второго \n");
                    continue;
                }
            }
        }

        /*Обычный перебор */
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
        /*Решето Эратосфена для массива*/
        static IEnumerable<int> SieveEratosthenes(List<int> collection)
        {
            var numbers = new List<int>();
            //заполнение списка числами от 2 до n-1
            for (var i = 2; i < collection.Count() - 1; i++)
            {
                numbers.Add(i);
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2; j <= collection.Count(); j++)
                {
                    //удаляем кратные числа из списка
                    collection.Remove(numbers[i] * j);
                }
            }

            return collection;
        }

        static IEnumerable<int> GetCollectionSimpleNumberByNumber(int number, bool direction)
        {
            int count = 0;
            List<int> resultCollection = new List<int>();

            while (count < countPrintNumber)
            {
                if (direction)
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

        static IEnumerable<int> GetCollectionSimpleNumbers(List<int> numbers, bool directio)
        {
            if (numbers.Count() > 1)
            {
                int max = numbers.ElementAt(numbers.Count() - 1);

                for (int i = numbers.ElementAt(0); i < max; i++)
                {
                    numbers.Add(i);
                }

                return SieveEratosthenes(numbers);
            }
            else
            {
                return GetCollectionSimpleNumberByNumber(numbers.ElementAt(0), directio);
            }
        }

        static void Print(IEnumerable<int> collectionSimpelNumber, bool directio)
        {
            if (!directio)
            {
                for (int i = 0; i < countPrintNumber; i++)
                {
                    if (i == collectionSimpelNumber.Count() - 1)
                    {
                        Console.Write($"{collectionSimpelNumber.ElementAt(i)} \n");
                    }
                    else
                    {
                        Console.Write($"{collectionSimpelNumber.ElementAt(i)}, ");
                    }

                }
            }
            else
            {
                int j = 0;
                for (int i = collectionSimpelNumber.Count() - 1; j < countPrintNumber; i--)
                {
                    if (j == countPrintNumber)
                    {
                        Console.Write($"{collectionSimpelNumber.ElementAt(i)} \n");
                    }
                    else
                    {
                        Console.Write($"{collectionSimpelNumber.ElementAt(i)}, ");
                    }
                    j++;
                }
            }
        }

    }
}
