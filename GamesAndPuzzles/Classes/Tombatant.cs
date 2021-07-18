using System;
using System.Collections.Generic;
using System.Text;

namespace GamesAndPuzzles.Classes
{
    public class Tombatant
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get; }
        public int Health { get; private set; } = 10;

        public Tombatant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            FullName = firstName + " " + LastName;
        }

        public void TakeDamage(int damage)
        {
            if (Health >= damage)
            {
                Health -= damage;
            }
            else
            {
                Health = 0;
            }
        }

        public int DealDamage()
        {
            Random r = new Random();
            int damage = r.Next(1, 10);
            return damage;
        }
    }
}
