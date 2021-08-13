using CapstoneClient.API_Services;
using CapstoneClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneClient
{
    public class ConsoleService
    {
        private static TriviaService triviaService = new TriviaService();
        private static DatabaseService dataService = new DatabaseService();
        private static CardService cardService = new CardService();
        private static DeckService deckService = new DeckService();
        private static OwlBotService owlBotService = new OwlBotService();
        private static MetMuseumService imgService = new MetMuseumService();

        public void TriviaDeckBuilder()
        {
            TriviaCategoryList apiCategoryList = triviaService.GetCategoryList();
            Console.WriteLine("ID  |  CATEGORY");
            Console.WriteLine("##################################");
            foreach (TriviaCategories category in apiCategoryList.TriviaCategories)
            {
                Console.WriteLine($"{category.ID}  | {category.Name}");
            }

            Console.Write("Please select a category for the deck: ");
            int apiSelectedCategory = int.Parse(Console.ReadLine());

            TriviaQuestionCount apiQuestionCount = triviaService.GetCategoryQuestionCount(apiSelectedCategory);
            Console.WriteLine("Total Category Question Count: ");
            Console.WriteLine($"Easy: {apiQuestionCount.CategoryQuestionCount[0].TotalEasyQuestionCount}");
            Console.WriteLine($"Medium: {apiQuestionCount.CategoryQuestionCount[0].TotalMediumQuestionCount}");
            Console.WriteLine($"Hard: {apiQuestionCount.CategoryQuestionCount[0].TotalHardQuestionCount}");
            Console.WriteLine($"Total: {apiQuestionCount.CategoryQuestionCount[0].TotalQuestionCount}");

            string categoryName = null;
            foreach (TriviaCategories category in apiCategoryList.TriviaCategories)
            {
                if (category.ID == apiSelectedCategory)
                {
                    categoryName = category.Name;
                }
                if (categoryName != null) break;
            }

            string dbSearchName = null;
            if (categoryName.Contains("Entertainment"))
            {
                dbSearchName = "Entertainment";
            }
            else if (categoryName.Contains(':'))
            {
                dbSearchName = categoryName.Substring(categoryName.IndexOf(' '));
            }
            else
            {
                dbSearchName = categoryName;
            }

            DBCategory dBCategory = dataService.GetCategoryByName(dbSearchName.Trim());
            Deck pendingDeck = deckService.GetDeck(dBCategory.CategoryID);
            try
            {
                bool isSuccessful = dataService.CreateDeck(pendingDeck);
                if (isSuccessful)
                {
                    TriviaCardBuilder(apiSelectedCategory, pendingDeck);
                    Console.WriteLine("Deck created!");
                }
                else
                {
                    Console.WriteLine("An error has occurred. Please try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error message: " + e.Message);
            }

        }

        public void TriviaCardBuilder(int categoryID, Deck destinationDeck)
        {
            Deck createdDeck = dataService.GetDeck(destinationDeck.Name);
            Console.Write("Difficulty? 1/3/5: ");
            int input = int.Parse(Console.ReadLine());

            string difficulty = null;
            if (input == 5)
            {
                difficulty = "hard";
            }
            else if (input == 3)
            {
                difficulty = "medium";
            }
            else
            {
                difficulty = "easy";
            }

            Console.Write("Number of Questions? 1-20: ");
            int amount = int.Parse(Console.ReadLine());

            TriviaResult result = triviaService.GetQuestions(amount, categoryID, difficulty);
            List<Card> cards = cardService.GetTriviaCards(result);
            int successCount = 0;
            bool isSuccessful;
            foreach (Card newCard in cards)
            {
                try
                {
                    isSuccessful = dataService.CreateCard(createdDeck.DeckID, newCard);
                    if (isSuccessful)
                    {
                        successCount++;
                    }
                    else
                    {
                        Console.WriteLine("An error has occurred. Please try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error message: " + e.Message);
                }
            }
            Console.WriteLine($"{successCount} cards added successfully.");
        }

        public void DefinitionDeckBuilder()
        {
            List<DBCategory> dbCategoryList = dataService.GetCategories();
            Console.WriteLine("ID  |  CATEGORY");
            Console.WriteLine("##################################");
            foreach (DBCategory category in dbCategoryList)
            {
                Console.WriteLine($"{category.CategoryID}  | {category.CategoryName}");
            }
            Console.Write("Please select a category for the deck: ");
            int selectedCategory = int.Parse(Console.ReadLine());

            Deck pendingDeck = deckService.GetDeck(selectedCategory);
            try
            {
                bool isSuccessful = dataService.CreateDeck(pendingDeck);
                if (isSuccessful)
                {
                    DefinitionCardBuilder(pendingDeck);
                    Console.WriteLine("Deck created!");
                }
                else
                {
                    Console.WriteLine("An error has occurred. Please try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error message: " + e.Message);
            }

        }

        public void DefinitionCardBuilder(Deck destinationDeck)
        {
            Deck createdDeck = dataService.GetDeck(destinationDeck.Name);
            Dictionary<string, OwlBotDefinition> definitions = new Dictionary<string, OwlBotDefinition>();
            Console.Write("Number of cards? 1-20: ");
            int amount = int.Parse(Console.ReadLine());
            for (int i = 0; i < amount; i++)
            {
                bool getNextTerm = false;
                while (!getNextTerm)
                {
                    Console.Write("Term to define: ");
                    string term = Console.ReadLine();
                    OwlBotResult result = owlBotService.GetDefinition(term);
                    Console.WriteLine(term + ": " + result.Definitions[0].Definition);
                    Console.WriteLine();
                    Console.Write("Create card? Y/N: ");
                    string approve = Console.ReadLine().Trim();
                    if (approve == "Y" || approve == "y" || approve == "yes" || approve == "Yes")
                    {
                        definitions[term] = result.Definitions[0];
                        getNextTerm = true;
                    }
                    else
                    {
                        Console.WriteLine("Card skipped. Try new term.");
                    }
                }
            }
            List<Card> cardsToAdd = cardService.GetDefinitionCards(definitions);
            int successCount = 0;
            bool isSuccessful;
            foreach (Card newCard in cardsToAdd)
            {
                try
                {
                    isSuccessful = dataService.CreateCard(createdDeck.DeckID, newCard);
                    if (isSuccessful)
                    {
                        successCount++;
                    }
                    else
                    {
                        Console.WriteLine("An error has occurred. Please try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error message: " + e.Message);
                }
            }
            Console.WriteLine($"{successCount} cards added successfully.");
        }

        public void ImageDeckBuilder()
        {
            MetMuseumDepartments metDepartmentsList = imgService.GetMuseumDepartments();
            Console.WriteLine("ID  |  DEPARTMENT");
            Console.WriteLine("##################################");
            foreach (Department department in metDepartmentsList.Departments)
            {
                Console.WriteLine($"{department.DepartmentID}  | {department.DisplayName}");
            }

            Console.Write("Please select a department: ");
            int selectedDepartment = int.Parse(Console.ReadLine());

            MetMuseumResult objectList = imgService.GetDepartmentObjects(selectedDepartment);
            Console.Write("Total Object Count: " + objectList.objectIDs.Count);
            Console.WriteLine();
            Deck pendingDeck = deckService.GetDeck(11);
            try
            {
                bool isSuccessful = dataService.CreateDeck(pendingDeck);
                if (isSuccessful)
                {
                    ImageCardBuilder(objectList, pendingDeck);
                    Console.WriteLine("Deck created!");
                }
                else
                {
                    Console.WriteLine("An error has occurred. Please try again.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error message: " + e.Message);
            }
        }

        public void ImageCardBuilder(MetMuseumResult objectList, Deck destinationDeck)
        {
            Deck createdDeck = dataService.GetDeck(destinationDeck.Name);
            Console.Write("Number of Questions? 1-20: ");
            int amount = int.Parse(Console.ReadLine());
            List<MetMuseumObject> pendingCardList = new List<MetMuseumObject>();
            for(int i = 0; i < amount; i++)
            {
                MetMuseumObject item = imgService.GetObjectDetail(objectList.objectIDs[i]);
                pendingCardList.Add(item);
            }
            List<Card> cards = cardService.GetImageCards(pendingCardList);
            int successCount = 0;
            bool isSuccessful;
            foreach (Card newCard in cards)
            {
                try
                {
                    isSuccessful = dataService.CreateCard(createdDeck.DeckID, newCard);
                    if (isSuccessful)
                    {
                        successCount++;
                    }
                    else
                    {
                        Console.WriteLine("An error has occurred. Please try again.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error message: " + e.Message);
                }
            }
            Console.WriteLine($"{successCount} cards added successfully.");


        }
    }
}
