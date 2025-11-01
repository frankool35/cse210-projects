using System;

class Program
{
    static void Main(string[] args)
    {
        
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        int gradePercentage = int.Parse(Console.ReadLine());

        string letter = "";
        string sign = "";

        // Determine the letter grade
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine the sign (+ or -)
        int lastDigit = gradePercentage % 10;

        if (letter != "A" && letter != "F") // Only apply signs to B, C, D
        {
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }
        else if (letter == "A" && gradePercentage < 93)
        {
            sign = "-"; // A- case
        }

        // Display the full letter grade
        Console.WriteLine($"\nYour grade is: {letter}{sign}");

        // Determine pass or fail
        if (gradePercentage >= 70)
        {
            Console.WriteLine("🎉 Congratulations! You passed the class!");
        }
        else
        {
            Console.WriteLine("Keep trying! You’ll improve next time.");
        }
    }
}

    
