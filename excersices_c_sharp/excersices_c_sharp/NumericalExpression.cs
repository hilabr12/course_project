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
            return Number.ToWords(new CultureInfo("en-US"));
        }
        public long GetValue()
        {
            return Number;
        }

        public static long SumLetters(int number)
        {
            long counterOfLetters = 0;
            for(long currentNumber = 1; currentNumber <= number; currentNumber++)
            {
                
                NumericalExpression expression = new NumericalExpression(currentNumber);
                string currentNumberAsWord = expression.ToString().Trim();
                counterOfLetters += currentNumberAsWord.Length;
            }
            return counterOfLetters;
        }
        //Method Overloading
        public static long SumLetters(NumericalExpression expression)
        {
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
