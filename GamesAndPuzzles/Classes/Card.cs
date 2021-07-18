using System;
using System.Collections.Generic;
using System.Text;

namespace GamesAndPuzzles.Classes
{
public class Card
        {
            //add properties
            //private set means it can only be changed by methods in this object
            public bool isFaceUp { get; private set; }
            //make them set once and unchangeable
            //this requires removal of the set method
            public string Suit { get; }

            public int Number { get; }
            //make FaceValue a derived property of number
            public string FaceValue
            {
                //custom getter
                get
                {
                    if (Number == 11)
                    {
                        return "Jack";
                    }
                    else if (Number == 12)
                    {
                        return "Queen";
                    }
                    else if (Number == 13)
                    {
                        return "King";
                    }
                    else if (Number == 14 || Number == 1)
                    {
                        return "Ace";
                    }
                    else
                    {
                        return Number.ToString();
                    }
                }
            }

            //make Color a derived property of suit
            public string Color
            {
                //custom getter
                get
                {
                    if (Suit == "Hearts" || Suit == "Diamonds")
                    {
                        return "Red";
                    }
                    else
                    {
                        return "Black";
                    }
                }
            }
            //make sure a card always has at least a suit, a number, a facevalue and a color
            //add a custom constructor; like a method, but no return type and the name is the same as the class
            public Card(string suit, int number)
            {
                Suit = suit;
                Number = NumberChecker(number);
            }

            //an overloaded constructor
            public Card(string suit, int number, bool isFaceUp)
            {
                Suit = suit;
                Number = NumberChecker(number);
                this.isFaceUp = isFaceUp;
            }
            public bool Flip()
            {
                if (isFaceUp)
                {
                    isFaceUp = false;
                }
                else
                {
                    isFaceUp = true;
                }
                return isFaceUp;
            }
            //overload the flip method
            public bool Flip(bool shouldBeFaceDown)
            {
                isFaceUp = !shouldBeFaceDown;
                return isFaceUp;
            }
            //helper function
            private int NumberChecker(int num)
            {
                if (num < 1 || num > 14)
                {
                    return 1;
                }
                else
                {
                    return num;
                }
            }
            public static List<string> SuitNames = new List<string>() { "Diamonds", "Hearts", "Clubs", "Spades" };
        }
    
}
