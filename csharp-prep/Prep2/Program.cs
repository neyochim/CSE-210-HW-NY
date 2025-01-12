using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string userInput = Console.ReadLine();
        int numberGrade = int.Parse(userInput);

        string letterGrade = "";

        if (numberGrade >= 90)
        {
            letterGrade = "A";
        }
        else if (numberGrade >= 80)
        {
            letterGrade = "B";
        }
        else if (numberGrade >= 70)
        {
            letterGrade = "C";
        }
        else if (numberGrade >= 60)
        {
            letterGrade = "D";
        }
        else
        {
            letterGrade = "F";
        }
        Console.WriteLine($"Your letter grade is {letterGrade}.");
        if (numberGrade >= 70)
        {
            Console.WriteLine("Congradulations! You have a passing grade!");
        }
        else
        {
            Console.WriteLine("You do not have a passing grade. You did not pass the class. Better luck next time!");
        }
    }
}