using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "";

            while(true)
            {
                str = Console.ReadLine();

                Boolean ismatch = Regex.IsMatch(str, "^[a-z0-9]{6,14}$");
                Console.WriteLine(ismatch);
            }

            

        }
    }
}
