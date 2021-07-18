using System;
using System.Collections.Generic;
using System.Text;

namespace GamesAndPuzzles.Classes
{
    public class UserMenu
    {
            public void StartUp()
            {

                bool madeSelection = false;
                string userInput = "";
                Console.WriteLine("Welcome to GamesAndPuzzles.");
                Console.WriteLine("The name is pretty self-explanatory;");
                while (!madeSelection)
                {
                    Console.WriteLine("\nPlease select one of the following options:" +
                                      "\n(1) Petals Around the Rose" +
                                      "\n(2) Deck of Cards" +
                                      "\n(3) Mortal Tombat!" +
                                      "\n(4) Exit" );
                    userInput = Console.ReadLine();
                if (userInput == "1" || userInput == "(1)")
                {
                    PetalsAroundTheRose patr = new PetalsAroundTheRose();
                    madeSelection = true;
                }
                else if (userInput == "2" || userInput == "(2)")
                {
                    Console.WriteLine();
                    Console.WriteLine("##################################");
                    Console.WriteLine("Shuffled Deck");
                    Console.WriteLine("##################################");
                    Console.WriteLine();
                    Deck ourDeck = new Deck();
                    ourDeck.Shuffle();
                    for (int i = 0; i < ourDeck.NumberOfCardsInDeck; i++)
                    {
                        Card dealtCard = ourDeck.Draw();
                        Console.WriteLine($"The card is the {dealtCard.FaceValue} of {dealtCard.Suit}");
                    }
                    madeSelection = true;
                }
                else if (userInput == "3")
                {
                    //Mortal Tombat!

                    Tombat battle = new Tombat();
                    battle.Fight();
                    Console.WriteLine("############################################################");
                    Console.WriteLine("                     MORTAL TOMBAT!                         ");
                    Console.WriteLine("############################################################");
                    Console.WriteLine();
                    madeSelection = true;
                }
                else if (userInput == "4" || userInput == "(4)")
                {
                    // get outta here
                    madeSelection = true;
                    return;
                }

                else
                {
                    // check input for mistake and repeat loop
                    Console.WriteLine("I'm sorry, I don't have that option. Please try again.\n");
                }
                    madeSelection = false;
                }
            }

        
    }
}
