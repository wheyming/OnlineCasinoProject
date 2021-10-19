using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCasinoProjectConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            UserAuthentication UA = new UserAuthentication();
            Console.WriteLine("Would you like to 1)Login or 2)Signup?");
            int input = Convert.ToInt32(Console.ReadLine());
        }

    }
}
