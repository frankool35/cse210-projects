using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for their first name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();
       // Prompt user for last name
        Console.Write("What is your last name? ");
        sring lastName = Console.ReadLine();
       
       // Display formatted name
        Console.WriteLine($"\nYour name is {lastName}, {firsName} {lastName}. ");
    }
}