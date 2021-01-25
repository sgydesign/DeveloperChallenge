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
     * The ConsolePrinter class outputs information to the console
     */
    public class ConsolePrinter
    {
        public static object PrintValue;

        /**
		* Takes the inputed value and assignes it to the PrintValue variable
		*
		* @param  value   the value that will be dsipalyed to the console
		* @return         the inputed value
		*/
        public ConsolePrinter Value(string value)
        {
            PrintValue = value;
            return this;
        }

        /**
		* Reads from PrintValue and outputs it to the console
		*
		* @return         null
		*/
        public override string ToString()
        {
            Console.WriteLine(PrintValue);
            return null;
        }
    }
}
