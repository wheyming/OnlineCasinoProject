using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            bool startupBool = true;
            do
            {
                Console.WriteLine(DateTime.Now.ToString());
                string input1_1;
                string input1_2;
                string input1_3;
                string input1_4;
                FileHandling FH = new FileHandling();
                UserAuthentication UA = new UserAuthentication(FH);
                try
                {
                    Console.WriteLine("Welcome, would you like to\n1) Signup\n2) Login?\n3) End Program?");
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
                                    input1_3 = Console.ReadLine();
                                } while (UA.checkPhoneNumber(input1_3) == true);

                                Console.WriteLine("Please input password.");
                                input1_4 = Console.ReadLine();
                                UA.signup(input1_1, input1_2, input1_3, input1_4);
                                Console.WriteLine("New account has been registered.");
                                break;
                            }
                        case 2:
                            {
                                try
                                {
                                    Console.WriteLine("Username: ");
                                    string input2_1 = Console.ReadLine();
                                    Console.WriteLine("Password: ");
                                    string input2_2 = Console.ReadLine();
                                    if (UA.login(input2_1, input2_2) == true)
                                    {
                                        bool loginBool = true;
                                        do
                                        {
                                            Console.WriteLine($"Welcome {input2_1}");
                                            Console.WriteLine("Would you like to" +
                                                "\n1.) Play slots?" +
                                                "\n2.) Logout?");
                                            try
                                            {
                                                int input2_3 = Convert.ToInt32(Console.ReadLine());
                                                switch (input2_3)
                                                {
                                                    case 1:
                                                        {
                                                            Console.WriteLine("How much money would you like to bet?");
                                                            int input2_4 = Convert.ToInt32(Console.ReadLine());
                                                            CustomRandom CR = new CustomRandom();
                                                            Gambling gambling = new Gambling(FH, CR);
                                                            gambling.playSlot(input2_4, input2_1, Owner.prizeModuleBool);
                                                            break;
                                                        }
                                                    case 2:
                                                        {
                                                            loginBool = false;
                                                            Console.WriteLine("Good bye.");
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Invalid input.\n");
                                                            break;
                                                        }
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Invalid input.\n");
                                            }
                                            catch (OverflowException)
                                            {
                                                Console.WriteLine("Invalid input.\n");
                                            }

                                        } while (loginBool == true);
                                    }
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Incorrect username or password.");
                                    break;
                                }
                            }
                        case 3:
                            {
                                startupBool = false;
                                break;
                            }
                        case 000000000:
                            {
                                try
                                {
                                    Console.WriteLine("O Username: ");
                                    string input3_1 = Console.ReadLine();
                                    Console.WriteLine("O Password: ");
                                    string input3_2 = Console.ReadLine();
                                    Owner owner = new Owner(FH);
                                    FinancialReport FR = new FinancialReport(FH);
                                    DateConverter DC = new DateConverter();
                                    if (owner.ownerLogin(input3_1, input3_2) == true)
                                    {
                                        bool ownerLogin = true;
                                        do
                                        {
                                            Console.WriteLine("\nWelcome Owner.");
                                            Console.WriteLine("Would you like to" +
                                                "\n1.) Set prize module?" +
                                                "\n2.) View financial report for a certain day?" +
                                                "\n3.) View financial report for a certain month?" +
                                                "\n4.) View financial report for a certain year?" +
                                                "\n5.) Log out.");
                                            try
                                            {
                                                int input3_3 = Convert.ToInt32(Console.ReadLine());
                                                switch (input3_3)
                                                {
                                                    case 1:
                                                        {
                                                            Console.WriteLine("Would you like to" +
                                                                "\n1.) Activate prize giving module?" +
                                                                "\n2.) Deactivate prize giving module?");
                                                            int input3_4 = Convert.ToInt32(Console.ReadLine());
                                                            owner.setPrizeModule(input3_4);
                                                            break;
                                                        }
                                                    case 2:
                                                        {
                                                            Console.WriteLine("Which day would you like to view the financial report for?");
                                                            string input3_5 = Console.ReadLine();
                                                            if (DC.inputDayConvert(input3_5) != DateTime.MinValue)
                                                            {
                                                               Console.WriteLine(input3_5);
                                                               FR.generateFinancialReportDay(DC.inputDayConvert(input3_5)); 
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Incorrect date format.");
                                                            }
                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            Console.WriteLine("Which month of the year would you like to view the financial report for?");
                                                            string input3_6 = Console.ReadLine();
                                                            if (DC.inputMonthConvert(input3_6) != DateTime.MinValue)
                                                            {
                                                                FR.generateFinancialReportMonth(DC.inputMonthConvert(input3_6));
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Incorrect date format.");
                                                            }
                                                            break;
                                                        }
                                                    case 4:
                                                        {
                                                            Console.WriteLine("Which year would you like to view the financial report for?");
                                                            string input3_7 = Console.ReadLine();
                                                            if (DC.inputYearConvert(input3_7) != DateTime.MinValue)
                                                            {
                                                                FR.generateFinancialReportYear(DC.inputYearConvert(input3_7));
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Incorrect date format.");
                                                            }
                                                            break;
                                                        }
                                                    case 5:
                                                        {
                                                            ownerLogin = false;
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Invalid input. Please only input 1 to 4");
                                                            break;
                                                        }
                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Invalid input");
                                            }
                                            catch (ArgumentOutOfRangeException)
                                            {
                                                Console.WriteLine("Invalid input");
                                            }

                                        } while (ownerLogin == true);

                                    }


                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Incorrect username or password.");
                                    break;
                                }
                            }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input.");
                }
            } while (startupBool == true);
        }

    }
}
