using System;

class Program
{
    static void Main(string[] args)
    {
        
        string playAgain = "yes";

        while (playAgain.ToLower() == "yes")
        {
            // Generate a random number between 1 and 100
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 101);

            int guess = -1;
            int guessCount = 0;

            Console.WriteLine("\nWelcome to the Guess My Number Game!");
            Console.WriteLine("I have chosen a number between 1 and 100.\n");

            // Keep looping until the correct number is guessed
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"🎉 You guessed it in {guessCount} tries!");
                }
            }

            // Ask if the user wants to play again
            Console.Write("\nDo you want to play again? (yes/no): ");
            playAgain = Console.ReadLine();
        }

        Console.WriteLine("\nThanks for playing! Goodbye!");
    }
}

    
