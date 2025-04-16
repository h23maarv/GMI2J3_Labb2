using System;
using RomanNumerals;
namespace RomanNumerals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var command = "";
            Console.WriteLine("Choose 'n' to Exit");
            while (command != "n")
            {
                try
                {
                    // integer to Roman
                    Console.WriteLine($"{Environment.NewLine}Enter an integer number: ");
                    string? strNum = Console.ReadLine();
                    int intNum = Int32.Parse(strNum!);

                    // ToString() is automatically called once
                    RomanNumeral rNum = new(intNum);
                    Console.WriteLine($"Integer: {strNum} equals the Roman number: {rNum}");

                    // Roman to integer
                    Console.WriteLine($"{Environment.NewLine}Enter a Roman number:");
                    strNum = Console.ReadLine();
                    intNum = RomanNumeral.ParseRoman(strNum!);
                    Console.WriteLine($"Roman number: {strNum} equals the Integer number: {intNum}");

                    Console.Write($"{Environment.NewLine}Continue? ");
                    command = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex}");
                }
            }
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
