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
	 * The JsonFeed class calls the required api's then returns the relevated information
	 */
	class JsonFeed
    {
        static string _url = "";
		static int jokeCount = 1;

		public JsonFeed() { }

		/**
		* Constructor to setup Joke API and Amount of jokes
		*
		* @param  endpoint   API URL endpoing
		* @param  results   Amount of jokes to return
		*/
		public JsonFeed(string endpoint, int results)
        {
            _url = endpoint;
			jokeCount = results;

		}

		/**
		* Return a joke that has been concatinated with custom information
		*
		* @param  firstname   the firstname to be used in the joke
		* @param  lastname   the lastname to be used in the joke
		* @param  category   the category that the joke will be from
		* @return         final Jokes
		*/
		public static string[] GetRandomJokes(string firstname, string lastname, string category)
		{

			try
			{
				//set our array length for the jokes
				string[] jokeResult = new string[jokeCount];

			for (int i = 0; i < jokeCount; i++)
			{

				HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			string url = "jokes/random";
			if (category != null)
			{
				//Wrapped if else statement in curly brackets
				if (url.Contains('?'))
				{
					url += "&";
				}
				else
				{
					//cleaned up concatenation on the URL variable
					url += "?" + "category=" + category;
				}
			}

            string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

            if (firstname != null && lastname != null)
            {
                int index = joke.IndexOf("Chuck Norris");
                string firstPart = joke.Substring(0, index);
                string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
				joke = firstPart + " " + firstname + " " + lastname + secondPart;
            }

				//add each joke into our result array
				jokeResult[i] = ("Joke: " + i + " - "+ JsonConvert.DeserializeObject<dynamic>(joke).value + "\n");
			}

			return jokeResult;

		}
			catch (Exception)
			{
				Console.WriteLine("ERROR, connection to the Joke Generator API failed");
				throw;
			}

}

		/**
		* Calls a random name api and returns a first and last name
		*
		* @return         random first and last name
		*/
		public static dynamic Getnames()
		{
			try
			{
				HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			var result = client.GetStringAsync("").Result;
			return JsonConvert.DeserializeObject<dynamic>(result);
			}
			catch (Exception)
			{
				Console.WriteLine("ERROR, connection to the Name API failed");
				throw;
			}
		}

		/**
		* Calls a the category list from the joke API
		*
		* @return         joke categories
		*/
		public static string[] GetCategories()
		{
			try
			{
				HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			//formated returned result
			return JsonConvert.DeserializeObject<string[]>(Task.FromResult(client.GetStringAsync("categories").Result).Result);
			}
			catch (Exception)
			{
				Console.WriteLine("ERROR, connection to the Joke Category API failed");
				throw;
			}
		}
    }
}
