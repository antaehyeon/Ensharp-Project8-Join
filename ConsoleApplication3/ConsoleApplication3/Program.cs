using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {

            


        }


        public string functionname(arg)
        {
            if (condition)
            {
                Page page = HttpContext.Current.CurrentHandler as Page;
                page.ClientScript.RegisterStartupScript(
                    typeof(Page),
                    "Test",
                    "<script type='text/javascript'>functionname1(" + arg1 + ",'" + arg2 + "');</script>");
            }
        }
    }
}
