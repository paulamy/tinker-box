using System;
using System.Collections.Generic;
using System.Text;

namespace GamesAndPuzzles.Classes
{
    public class PetalsAroundTheRose
    {
        public PetalsAroundTheRose()
        {
            Console.WriteLine("#########################################################################################################");
            Console.WriteLine("Let's play a game called \"Petals Around The Rose.\"");
            Console.WriteLine("The name of the game is significant.");
            Console.WriteLine("On each turn I will roll five dice, then ask you for the score.");
            Console.WriteLine("After you guess the score, I will tell you if you are right, or tell you the correct score.");
            Console.WriteLine("The game ends when you prove that you know the secret by guessing the score correctly six times in a row.");
            Console.WriteLine("#########################################################################################################");
            Console.WriteLine();
            int correctGuesses = 0;
            int score = 0;
            const int hintsAvailable = 3;
            int hintsUsed = 0;
            while (correctGuesses < 6)
            {
                Console.Write("The current roll is: ");
                int[] rolls = new int[5];
                for (int i = 0; i < rolls.Length; i++)
                {
                    Random r = new Random();
                    int value = r.Next(1, 6);
                    rolls[i] = value;
                    Console.Write($"{rolls[i]}  ");
                    if (rolls[i] == 3)
                    {
                        score += 2;
                    }
                    else if (rolls[i] == 5)
                    {
                        score += 4;
                    }
                }
                Console.Write("\nWhat is the current score? ");
                int input = int.Parse(Console.ReadLine());
                if (input == score)
                {
                    Console.WriteLine("Correct!");
                    correctGuesses++;
                    Console.WriteLine($"Current Streak: {correctGuesses}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"The correct score is {score}.");
                    correctGuesses = 0;
                    Console.WriteLine($"Current Streak: {correctGuesses}");
                    Console.WriteLine();
                }
                score = 0;

                if (hintsUsed < 3)
                {
                    Console.Write($"Need a Hint (Y/N)? ");
                    string giveHint = Console.ReadLine().ToString().ToLower();
                    if (giveHint == "y" && hintsUsed == 0)
                    {
                        hintsUsed++;
                        Console.WriteLine("HINT: THE SCORE WILL ALWAYS BE EVEN OR 0.");
                        Console.WriteLine($"Hints Used: {hintsUsed}/{hintsAvailable}");
                        Console.WriteLine();
                    }
                    else if (giveHint == "y" && hintsUsed == 1)
                    {
                        hintsUsed++;
                        Console.WriteLine("HINT: THE ORDER OF THE DICE IS IRRELEVANT");
                        Console.WriteLine($"Hints Used: {hintsUsed}/{hintsAvailable}");
                        Console.WriteLine();
                    }
                    else if (giveHint == "y" && hintsUsed == 2)
                    {
                        hintsUsed++;
                        Console.WriteLine("HINT: LOOK AT REAL DICE OR PICTURE THEM FOR EACH ROLL.");
                        Console.WriteLine($"Hints Used: {hintsUsed}/{hintsAvailable}");
                        Console.WriteLine();
                    }
                }

            }
            Console.WriteLine("#########################################################################################################");
            Console.WriteLine("Congratulations! You are now a member of the Secret Society of the Petals Around The Rose.");
            Console.WriteLine();
            Console.WriteLine("You must pledge never to reveal the secret to anyone!");
            Console.WriteLine("#########################################################################################################");
        }
    }
}
