using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    /**
     * The Program class manages all userinput and calls all the relevant classes required to run the application
     */
    class Program
    {
        static string[] results = new string[50];
        static char key;
        static Tuple<string, string> names;
        static ConsolePrinter printer = new ConsolePrinter();

        /**
		* Constructor that manages the users input and will loop through untill the user exits
		*/
        static void Main(string[] args)
        {
            Boolean continueJokes = true;
            //Added welcome message, removed inital menu
            printer.Value("--== Welcome to the Joke Generator ==--").ToString();
            while (continueJokes)
                {
                

                printer.Value("\nPress c to get categories").ToString();
                    printer.Value("Press r to get random jokes").ToString();
                    GetEnteredKey(Console.ReadKey());
                if (key == 'c')
                {
                    getCategories();
                    PrintResults();
                }
                else if (key == 'r')
                {
                    printer.Value("\nWant to use a random name? y/n").ToString();
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'y')
                    {
                        GetNames();
                    }
                    else
                    {
                        GetUserNames();
                    }

                    printer.Value("\nWant to specify a category? y/n").ToString();
                    //missing selection for user input
                    GetEnteredKey(Console.ReadKey());

                    if (key == 'y')
                    {

                        int n = getJokeAmount();

                        printer.Value("Enter a category;").ToString();
                        GetRandomJokes(Console.ReadLine(), n);
                        PrintResults();
                    }
                    else
                    {

                        int n = getJokeAmount();
                        GetRandomJokes(null, n);
                        PrintResults();
                    }
                }
                else
                {
                    printer.Value("\nYou make an invalid selection, please try again.").ToString();
                }

                
                    names = null;

                    //ask if the use wants to see another joke or exit the application
                    printer.Value("\nDo you want to see another joke? y/n").ToString();
                    GetEnteredKey(Console.ReadKey());

                        if (key == 'n')
                        {
                         continueJokes = false;
                        }
                    }

        }

        /**
        * Asks the user how many jokes they want to hear
        *
        * @return         amount of jokes
        */
        public static int getJokeAmount() {

            printer.Value("\nHow many jokes do you want? (1-9)").ToString();

            bool success = false;
            int number;

            //continue to loop though untill the correct data type is entered
            while (!success)
            {
              
                success = Int32.TryParse(Console.ReadLine(), out number);
                if (success)
                {

                    if (number < 1) {
                        printer.Value("You selected a value less than the minumum, we modified your selection to 1 joke").ToString();
                        return 1;
                    }
                    else if (number > 9) {
                        printer.Value("You selected a value larger than the maximum, we modified your selection to 9 jokes").ToString();
                        return 9;
                    }
                    else {
                        return number;
                    }
                    
                }
                else
                {
                    printer.Value("ERROR, int required, \nHow many jokes do you want? (1-9)").ToString();
                }
            }

            //program should never get to this point
            return -1;
        }

        /**
        * Outputs the results in a formated string
        */
        private static void PrintResults()
        {
            printer.Value("[" + string.Join(", ", results) + "]").ToString();
        }

        /**
        * Reads the user inputed key and assigns it to the key variable.
        *
        * @param  consoleKeyInfo  the key the user pressed
        */
        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                case ConsoleKey.N:
                    key = 'n';
                    break;
                default:
                    key = ' ';
                    break;
            }
        }


        /**
        * Calls a method that calls the Joke api and stores the data in the results vaiable
        *
        * @param  category   the joke category
        * @param  number   the amount of jokes
        */
        private static void GetRandomJokes(string category, int number)
        {

            new JsonFeed("https://api.chucknorris.io", number);
            results = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
  
        }

        /**
        * Calls a method that calls an API to get the coke categories and stores them in the results variable
        *
        */
        private static void getCategories()
        {
            new JsonFeed("https://api.chucknorris.io/jokes/categories", 0);
            results = JsonFeed.GetCategories();

        }

        /**
        * Calls a method that calls an API name server and stores the results in a tuple called names
        *
        */
        private static void GetNames()
        {
            new JsonFeed("https://www.names.privserv.com/api/", 0);
            dynamic result = JsonFeed.Getnames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }


        /**
        * Ask the user for their own name to use within the jokes, then store the results within a tuple called names
        *
        */
        private static void GetUserNames()
        {
            printer.Value("\nPlease enter in your first name").ToString();
            string firstName = Console.ReadLine();

            printer.Value("Please enter in your last name").ToString();
            string lastName = Console.ReadLine();

            names = Tuple.Create(firstName.ToString(), lastName.ToString());
        }
    }
}
