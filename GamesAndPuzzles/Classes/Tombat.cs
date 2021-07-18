using System;
using System.Collections.Generic;
using System.Text;

namespace GamesAndPuzzles.Classes
{
    public class Tombat
    {
        Tombatant[] Tombatants = new Tombatant[2];
        int preventDuplicate = 0;

        public Tombat()
        {
            Dictionary<int, Tombatant> fighters = new Dictionary<int, Tombatant>();
            Tombatant tomA = new Tombatant("Tom", "Anderson");
            Tombatant tomM = new Tombatant("Tom", "Medvitz");
            Tombatant tomC = new Tombatant("Tom", "Cruise");
            Tombatant tomH = new Tombatant("Tom", "Hanks");
            Tombatant tomJ = new Tombatant("Tom", "Jones");
            Tombatant tomS = new Tombatant("Tom", "Selleck");
            Tombatant tomK = new Tombatant("Tom", "Kenny");
            Tombatant tomB = new Tombatant("Tom", "Brady");
            Tombatant tomG = new Tombatant("Tom", "Green");
            Tombatant tomHl = new Tombatant("Tom", "Holland");
            Tombatant tomHd = new Tombatant("Tom", "Hiddleston");
            Tombatant tomHy = new Tombatant("Tom", "Hardy");
            Tombatant tomBw = new Tombatant("Tom", "Brokaw");
            Tombatant tomBl = new Tombatant("Tom", "Bombadil");
            Tombatant tomFM = new Tombatant("Tom", "From Myspace");

            fighters.Add(1, tomA);
            fighters.Add(2, tomM);
            fighters.Add(3, tomC);
            fighters.Add(4, tomH);
            fighters.Add(5, tomJ);
            fighters.Add(6, tomS);
            fighters.Add(7, tomK);
            fighters.Add(8, tomB);
            fighters.Add(9, tomG);
            fighters.Add(10, tomHl);
            fighters.Add(11, tomHd);
            fighters.Add(12, tomHy);
            fighters.Add(13, tomBw);
            fighters.Add(14, tomBl);
            fighters.Add(15, tomFM);

            Console.WriteLine("\nChoose First Tombatant (1-15): ");
            foreach (KeyValuePair<int, Tombatant> fighter in fighters)
            {
                if (fighter.Key <= 7)
                {
                    Console.Write($" { fighter.Key}) {fighter.Value.FullName} | ");
                }
                else if (fighter.Key <= 13)
                {
                    Console.Write($" { fighter.Key}) {fighter.Value.FullName}  |  ");
                }
                else if (fighter.Key > 13)
                {
                    Console.Write($" { fighter.Key}) {fighter.Value.FullName} | ");
                }
            }
            Console.WriteLine();
            while (Tombatants[0] == null)
            {
                try
                {
                    preventDuplicate = int.Parse(Console.ReadLine());
                    Tombatants[0] = fighters[preventDuplicate];
                    fighters.Remove(preventDuplicate);
                    Console.WriteLine($"\nTombatant 1: {Tombatants[0].FullName}");
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Please select a Tombatant from the list (1-15).");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
            Console.WriteLine("\nChoose Second Tombatant (1-15): ");
            foreach (KeyValuePair<int, Tombatant> fighter in fighters)
            {
                if (fighter.Key <= 7)
                {
                    Console.Write($" { fighter.Key}) {fighter.Value.FullName} | ");
                }
                else if (fighter.Key <= 13)
                {
                    Console.Write($" { fighter.Key}) {fighter.Value.FullName}  |  ");
                }
                else if (fighter.Key > 13)
                {
                    Console.Write($" { fighter.Key}) {fighter.Value.FullName} | ");
                }
            }
            Console.WriteLine();
            while (Tombatants[1] == null)
            {
                try
                {
                    Tombatants[1] = fighters[int.Parse(Console.ReadLine())];
                    Console.WriteLine($"\nTombatant 2: {Tombatants[1].FullName}");
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine("Please select a valid remaining Tombatant (1-15).");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
            Console.WriteLine();
        }

        public void Fight()
        {
            Console.WriteLine("############################################################");
            Console.WriteLine("                     MORTAL TOMBAT!                         ");
            Console.WriteLine("############################################################");
            Console.WriteLine();
            Console.WriteLine($"{Tombatants[0].FullName} Health: {Tombatants[0].Health}/10");
            Console.WriteLine($"{Tombatants[1].FullName} Health: {Tombatants[1].Health}/10");
            Console.WriteLine();
            int round = 1;
            while (Tombatants[0].Health > 0 && Tombatants[1].Health > 0)
            {

                Console.WriteLine($"Round {round}: Fight!");
                int tombatant1Damage = Tombatants[0].DealDamage();
                int tombatant2Damage = Tombatants[1].DealDamage();
                Console.WriteLine($"{Tombatants[0].FullName} Damage Dealt: {tombatant1Damage}");
                Console.WriteLine($"{Tombatants[1].FullName} Damage Dealt: {tombatant2Damage}");
                if (tombatant1Damage >= Tombatants[1].Health && tombatant2Damage >= Tombatants[0].Health)
                {
                    Tombatants[1].TakeDamage(tombatant1Damage);
                    Tombatants[0].TakeDamage(tombatant2Damage);
                    Console.WriteLine($"{Tombatants[0].FullName} Health: {Tombatants[0].Health}/10");
                    Console.WriteLine($"{Tombatants[1].FullName} Health: {Tombatants[1].Health}/10");
                    Console.WriteLine();
                    Console.WriteLine("Mututal Fatality!");
                    return;
                }
                else if (tombatant1Damage >= Tombatants[1].Health || tombatant2Damage >= Tombatants[0].Health)
                {
                    Tombatants[1].TakeDamage(tombatant1Damage);
                    Tombatants[0].TakeDamage(tombatant2Damage);
                    Console.WriteLine($"{Tombatants[0].FullName} Health: {Tombatants[0].Health}/10");
                    Console.WriteLine($"{Tombatants[1].FullName} Health: {Tombatants[1].Health}/10");
                    Console.WriteLine();
                    Console.WriteLine("Fatality!");
                    Console.WriteLine($"Tombatant {(Tombatants[0].Health > 0 ? Tombatants[0].FullName : Tombatants[1].FullName)} wins!");
                    if (Tombatants[0].Health == 10 || Tombatants[1].Health == 10)
                    {
                        Console.WriteLine("Flawless Victory!");
                    }
                    return;
                }
                else if (Tombatants[0].Health == 0 || Tombatants[1].Health == 0)
                {
                    Tombatants[1].TakeDamage(tombatant1Damage);
                    Tombatants[0].TakeDamage(tombatant2Damage);
                    Console.WriteLine($"{Tombatants[0].FullName} Health: {Tombatants[0].Health}/10");
                    Console.WriteLine($"{Tombatants[1].FullName} Health: {Tombatants[1].Health}/10");
                    Console.WriteLine($"Tombatant {(Tombatants[0].Health > 0 ? Tombatants[0].FullName : Tombatants[1].FullName)} wins!");
                    Console.WriteLine();

                }
                else
                {
                    Tombatants[1].TakeDamage(tombatant1Damage);
                    Tombatants[0].TakeDamage(tombatant2Damage);
                    Console.WriteLine($"{Tombatants[0].FullName} Health: {Tombatants[0].Health}/10");
                    Console.WriteLine($"{Tombatants[1].FullName} Health: {Tombatants[1].Health}/10");
                    Console.WriteLine();
                }
                round++;
            }

        }
    }
}
