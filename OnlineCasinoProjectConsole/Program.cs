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
            Console.WriteLine(DateTime.Now.ToString());
            string input1_1;
            string input1_2;
            string input1_3;
            string input1_4;
            UserAuthentication UA = new UserAuthentication();
            Console.WriteLine("Welcome, would you like to\n1)Signup\n2)Login?");
            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                case 1:
                    {
                        do
                        {
                            Console.WriteLine("Please input username.");
                            input1_1 = Console.ReadLine();
                        } while (UA.checkUsername(input1_1) == true);
                        do
                        {
                            Console.WriteLine("Please input id number.");
                            input1_2 = Console.ReadLine();
                        } while (UA.checkID(input1_2) == true);
                        do
                        {
                            Console.WriteLine("Please input phone number.");
                            input1_3= Console.ReadLine();
                        } while (UA.checkPhoneNumber(input1_3) == true);
                        
                        Console.WriteLine("Please input password.");
                        input1_4 = Console.ReadLine();
                        UA.signup(input1_1, input1_2, input1_3, input1_4);
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Username: ");
                        string input2_1 = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        string input2_2 = Console.ReadLine();
                        if(UA.login(input2_1, input2_2) == true)
                        {
                            Console.WriteLine("How much money would you like to bet?");
                            int input2_3 = Convert.ToInt32(Console.ReadLine());
                            Gambling gambling = new Gambling();
                            gambling.playSlot(input2_3, input2_1);
                        }
                        break;
                    }
                case 000000000:
                    {
                        Console.WriteLine("O Username: ");
                        string input3_1 = Console.ReadLine();
                        Console.WriteLine("O Password: ");
                        string input3_2 = Console.ReadLine();
                        Owner owner = new Owner();
                        if (owner.ownerLogin(input3_1, input3_2) == true)
                        {
                            Console.WriteLine("Welcome Owner.");
                        }


                        break;
                    }
            }
            Console.ReadLine();
        }

    }
}
