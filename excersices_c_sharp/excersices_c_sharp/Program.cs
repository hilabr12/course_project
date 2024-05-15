using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excersices_c_sharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            NumericalExpression n = new NumericalExpression(999999999);
            string result = n.ToString();
            Console.WriteLine(result);
        }
    }
}
