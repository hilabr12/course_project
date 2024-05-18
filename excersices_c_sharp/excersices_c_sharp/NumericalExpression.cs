using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;


namespace excersices_c_sharp
{
    public class NumericalExpression
    {
        private long Number { get; set; } 

        public NumericalExpression(long number) 
        {
            Number = number;
        }
        public override string ToString()
        {
            ///<summary>
            /// overrides the default ToString method to return the number as words
            ///</summary>
            /// <returns>A string representation of the number in words.</returns>
            return Number.ToWords(new CultureInfo("en-US"));
        }

        public long GetValue()
        {
            ///<summary>
            /// gets the numeric value represented by the expression
            ///</summary>
            /// <returns>The numeric value.</returns>
            return Number;
        }

        public static long SumLetters(int number)
        {
            ///<summary>
            /// Calculates the total number of letters used to write out the numbers from 1 to the given number
            ///</summary>
            /// <param name="number">The upper limit of the range.</param>
            /// <returns>The total number of letters used.</returns>
            long counterOfLetters = 0;
            for (long currentNumber = 1; currentNumber <= number; currentNumber++)
            {
                NumericalExpression expression = new NumericalExpression(currentNumber);
                string currentNumberAsWord = expression.ToString().Trim();
                counterOfLetters += currentNumberAsWord.Length;
            }
            return counterOfLetters;
        }

        // Method Overloading
        public static long SumLetters(NumericalExpression expression)
        {
            ///<summary>
            /// Calculates the total number of letters used to write out the numbers from 1 to the numeric value of the given expression
            ///</summary>
            /// <param name="expression">The numerical expression.</param>
            /// <returns>The total number of letters used.</returns>
            long counterOfLetters = 0;
            for (long currentNumber = 1; currentNumber <= expression.GetValue(); currentNumber++)
            {
                NumericalExpression currentExpression = new NumericalExpression(currentNumber);
                string currentNumberAsWord = currentExpression.ToString().Trim();
                counterOfLetters += currentNumberAsWord.Length;
            }
            return counterOfLetters;
        }





    }
}
