using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Guessing
{
    class Program
    {
        // Длина числа
        private const int NumberLen = 4;

        // Количество попыток
        private const int AttemptsCount = 3;

        private const string BULL = "BULL";
        private const string COW = "COW";

        // Регулярное выражение для проверки введенного числа
        private const string NumberPattern = "^[1-9][0-9][0-9][0-9]$";

        private static Regex Reg = new Regex(NumberPattern);

        // Сгенерированное число
        private static string GeneratedNumber = GenerateNumber();
        static void Main(string[] args)
        {
            Console.WriteLine(GeneratedNumber);
            var counter = AttemptsCount;
            while (counter > 0)
            {
                Console.WriteLine("Enter number. Attempts left: " + counter);
                var inputNumber = Console.ReadLine();

                if (!Reg.IsMatch(inputNumber))
                {
                    Console.WriteLine("Wrong format of number! Use " + NumberPattern);
                    continue;
                }

                if (CheckInputNumber(inputNumber))
                {
                    break;
                }
                else
                {
                    counter--;
                }
            }

            Console.WriteLine("Attempts ended! Try again later.");
        }

        // Метод генерирует число с неповторяющимися цифрами. Возвращает число в виде строки.
        private static string GenerateNumber()
        {
            var listOfNumber = new List<int>();
            var number = new StringBuilder();
            var rnd = new Random();
            var firstNumber = rnd.Next(1, 9);
            listOfNumber.Add(firstNumber);

            for (int i = 0; i < NumberLen - 1; i++)
            {
                var othersNumber = rnd.Next(0, 9);
                while (listOfNumber.Contains(othersNumber))
                {
                    othersNumber = rnd.Next(0, 9);
                }

                listOfNumber.Add(othersNumber);
            }

            listOfNumber.ForEach(n => number.Append(n));
            return number.ToString();
        }

        // Метод проверяет введенное число на соответсвие со сгенерированным. Возвращает true, если цисло совпало, иначе false.
        private static bool CheckInputNumber(string inputNumber)
        {
            if (inputNumber.Equals(GeneratedNumber))
            {
                Console.WriteLine("Successful!");
                return true;
            }

            Console.WriteLine("Entered wrong number!");
            for (int i = 0; i < inputNumber.Length; i++)
            {
                if (inputNumber[i] == GeneratedNumber[i])
                {
                    Console.WriteLine(BULL + " " + inputNumber[i]);
                }
                else if (GeneratedNumber.Contains(inputNumber[i]))
                {
                    Console.WriteLine(COW + " " + inputNumber[i]);
                }
            }

            return false;
        }
    }
}