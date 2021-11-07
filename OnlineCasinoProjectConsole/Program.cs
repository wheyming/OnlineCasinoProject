using OnlineCasinoProjectConsole.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using CasinoWebAPI.Common;
using CasinoWebAPI.Controllers;
using CasinoWebAPI.Interfaces;

namespace OnlineCasinoProjectConsole
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            bool startupBool = true;
            IConfigurationManager config = new ConfigurationController();
            IAuthenticationManager ua = new AuthenticationController();
            IReportManager fr = new ReportController();
            IGamblingManager gambling = new GamblingController(config, fr);
            fr.ReportListInitialize();
            do
            {
                try
                {
                    Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    Console.WriteLine("Welcome, would you like to\n" +
                        "1) Sign up\n" +
                        "2) Login?\n" +
                        "3) End Program?");
                    int input = Convert.ToInt32(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
                            {
                                bool insideMenu = true;
                                string input11;
                                do
                                {
                                    Console.WriteLine("Please input username.");
                                    input11 = Console.ReadLine();
                                    UserNameResultType result = ua.CheckUserName(input11);
                                    switch (result)
                                    {
                                        case UserNameResultType.None:
                                            insideMenu = false;
                                            break;
                                        case UserNameResultType.DuplicateUser:
                                            Console.WriteLine("Duplicate Username.");
                                            break;
                                        case UserNameResultType.UnhandledUserError:
                                            Console.WriteLine("Unexpected Error.");
                                            break;
                                        case UserNameResultType.UserNameContainsSpace:
                                            Console.WriteLine("Please Create A Username Without Space.");
                                            break;
                                        case UserNameResultType.UserNameDataAccessError:
                                            Console.WriteLine("Unable to find file.");
                                            break;
                                        case UserNameResultType.UserNameLengthIncorrect:
                                            Console.WriteLine("Please create a username between 6 to 24 characters.");
                                            break;
                                    }
                                } while (insideMenu);
                                insideMenu = true;
                                string input12;
                                do
                                {
                                    Console.WriteLine("Please input id number.");
                                    input12 = Console.ReadLine();
                                    IdResultType result = ua.CheckId(input12);
                                    switch (result)
                                    {
                                        case IdResultType.None:
                                            {
                                                insideMenu = false;
                                                break;
                                            }
                                        case IdResultType.DuplicateId:
                                            {
                                                Console.WriteLine("Duplicate idNumber.");
                                                break;
                                            }
                                        case IdResultType.IdIncorrect:
                                            {
                                                Console.WriteLine("Invalid ID Number");
                                                break;
                                            }
                                        case IdResultType.IdDataAccessError:
                                            {
                                                Console.WriteLine("Unable to find file.");
                                                break;
                                            }
                                        case IdResultType.UnhandledIdError:
                                            {
                                                Console.WriteLine("Unexpected Error.");
                                                break;
                                            }
                                    }
                                } while (insideMenu);
                                insideMenu = true;
                                string input13;
                                do
                                {
                                    Console.WriteLine("Please input phone number.");
                                    input13 = Console.ReadLine();
                                    PhoneNumberResultType result = ua.CheckPhoneNumber(input13);
                                    switch (result)
                                    {
                                        case PhoneNumberResultType.None:
                                            {
                                                insideMenu = false;
                                                break;
                                            }
                                        case PhoneNumberResultType.DuplicatePhoneNumber:
                                            {
                                                Console.WriteLine("Duplicate Phone Number.");
                                                break;
                                            }
                                        case PhoneNumberResultType.PhoneNumberIncorrect:
                                            {
                                                Console.WriteLine("Invalid Phone Number");
                                                break;
                                            }
                                        case PhoneNumberResultType.PhoneNumberDataAccessError:
                                            {
                                                Console.WriteLine("Unable to find file.");
                                                break;
                                            }
                                        case PhoneNumberResultType.UnhandledPhoneNumberError:
                                            {
                                                Console.WriteLine("Unexpected Error.");
                                                break;
                                            }
                                    }
                                } while (insideMenu);
                                insideMenu = true;
                                string input14;
                                do
                                {
                                    Console.WriteLine("Please input password.");
                                    input14 = Console.ReadLine();
                                    PasswordResultType result = ua.CheckPassword(input14);
                                    if (Equals(result & PasswordResultType.PasswordNoUpperCaseLetter, PasswordResultType.PasswordNoUpperCaseLetter))
                                    {
                                        Console.WriteLine("Please choose a password with at least one uppercase letter.");
                                    }
                                    if (Equals(result & PasswordResultType.PasswordNoLowerCaseLetter, PasswordResultType.PasswordNoLowerCaseLetter))
                                    {
                                        Console.WriteLine("Please choose a password with at least one lowercase letter.");
                                    }
                                    if (Equals(result & PasswordResultType.PasswordNoSpecialCharacter, PasswordResultType.PasswordNoSpecialCharacter))
                                    {
                                        Console.WriteLine("Please choose a password with at least one special character.");
                                    }
                                    if (Equals(result & PasswordResultType.PasswordNoDigits, PasswordResultType.PasswordNoDigits))
                                    {
                                        Console.WriteLine("Please choose a password with at least one digit.");
                                    }
                                    if (Equals(result & PasswordResultType.PasswordThreeRepeatedCharacters, PasswordResultType.PasswordThreeRepeatedCharacters))
                                    {
                                        Console.WriteLine("Please choose a password with at most two continuous repeated characters.");
                                    }
                                    if (Equals(result & PasswordResultType.IncorrectPasswordLength, PasswordResultType.IncorrectPasswordLength))
                                    {
                                        Console.WriteLine("Please choose a password between 6 to 24 characters.");
                                    }
                                    if (Equals(result & PasswordResultType.UnhandledPasswordError, PasswordResultType.UnhandledPasswordError))
                                    {
                                        Console.WriteLine("Please contact administrator.");
                                    }
                                    if (Equals(result & PasswordResultType.None, PasswordResultType.None))
                                    {
                                        insideMenu = false;
                                    }
                                } while (insideMenu);
                                ua.SignUp(input11, input12, input13, input14);
                                Console.WriteLine("New account has been registered.");
                                break;
                            }
                        case 2:
                            {
                                try
                                {
                                    Console.WriteLine("Username: ");
                                    string input21 = Console.ReadLine();
                                    Console.WriteLine("Password: ");
                                    string input22 = Console.ReadLine();
                                    if (ua.Login(input21, input22))
                                    {
                                        bool loginBool = true;
                                        do
                                        {
                                            if (ua.CurrentUser.IsOwner)
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
                                                    int input33 = Convert.ToInt32(Console.ReadLine());
                                                    switch (input33)
                                                    {
                                                        case 1:
                                                            {
                                                                Console.WriteLine("Would you like to" +
                                                                    "\n1.) Activate prize giving module?" +
                                                                    "\n2.) Deactivate prize giving module?");
                                                                bool input34 = Convert.ToBoolean(Console.ReadLine());
                                                                config.SetPrizeModuleStatus(input34);
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Console.WriteLine("Which day would you like to view the financial report for?");
                                                                string input35 = Console.ReadLine();
                                                                DateTime tempDateDay = DateConverter.InputDayConvert(input35);
                                                                if (tempDateDay != DateTime.MinValue)
                                                                {
                                                                    Console.WriteLine($"\nEarnings for {tempDateDay:dd MMMMM yyyy}: ${fr.GenerateFinancialReportDay(tempDateDay)}");
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
                                                                string input36 = Console.ReadLine();
                                                                DateTime tempDateMonth = DateConverter.InputMonthConvert(input36);
                                                                if (tempDateMonth != DateTime.MinValue)
                                                                {
                                                                    List<double> monthValueList = fr.GenerateFinancialReportMonth(tempDateMonth);
                                                                    Console.WriteLine("\n");
                                                                    for (int i = 1; i < 32; i++)
                                                                    {
                                                                        if (!(monthValueList[i] == 0.0))
                                                                        {
                                                                            Console.WriteLine($"{i} {tempDateMonth:MMMM yyyy}: ${monthValueList[i]}");
                                                                        }
                                                                    }
                                                                    Console.WriteLine($"Earnings for {tempDateMonth:MMMMM yyyy}: ${monthValueList[0]}");
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
                                                                string input37 = Console.ReadLine();
                                                                DateTime tempDateYear = DateConverter.InputYearConvert(input37);
                                                                if (tempDateYear != DateTime.MinValue)
                                                                {
                                                                    List<double> yearValueList = fr.GenerateFinancialReportYear(tempDateYear);
                                                                    Console.WriteLine("\n");
                                                                    for (int i = 1; i < 13; i++)
                                                                    {
                                                                        if (yearValueList[i] != 0.0)
                                                                        {
                                                                            Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)} {tempDateYear:yyyy}: ${yearValueList[i]}");
                                                                        }
                                                                    }
                                                                    Console.WriteLine($"Earnings for {tempDateYear:yyyy}: ${yearValueList[0]}");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Incorrect date format.");
                                                                }
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                ua.Logout();
                                                                loginBool = false;
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
                                                    Console.WriteLine("Invalid input A");
                                                }
                                                catch (ArgumentOutOfRangeException)
                                                {
                                                    Console.WriteLine("Invalid input B");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Welcome {input21}");
                                                Console.WriteLine("Would you like to" +
                                                    "\n1.) Play Slots?" +
                                                    "\n2.) Logout?");
                                                try
                                                {
                                                    int input23 = Convert.ToInt32(Console.ReadLine());
                                                    switch (input23)
                                                    {
                                                        case 1:
                                                            {
                                                                Console.WriteLine("How much money would you like to bet?");
                                                                int input24 = Convert.ToInt32(Console.ReadLine());

                                                                (int[], double, SlotsResultType) playSlotTuple = gambling.PlaySlot(input24, input21);
                                                                Console.Write(playSlotTuple.Item1[0]);
                                                                Thread.Sleep(500);
                                                                Console.Write('.');
                                                                Thread.Sleep(500);
                                                                Console.Write(playSlotTuple.Item1[1]);
                                                                Thread.Sleep(500);
                                                                Console.Write('.');
                                                                Thread.Sleep(500);
                                                                Console.Write(playSlotTuple.Item1[2]);
                                                                switch (playSlotTuple.Item3)
                                                                {
                                                                    case SlotsResultType.None:
                                                                        {
                                                                            Console.WriteLine($"\nUnfortunately, you did not win anything. Thank you for playing.");
                                                                            break;
                                                                        }
                                                                    case SlotsResultType.Double:
                                                                        {
                                                                            Console.WriteLine($"\nDOUBLE!! Congratulations. Your winnings are: {playSlotTuple.Item2}");
                                                                            break;
                                                                        }
                                                                    case SlotsResultType.Triple:
                                                                        {
                                                                            Console.WriteLine($"\nTRIPLE!! Congratulations. Your winnings are: {playSlotTuple.Item2}");
                                                                            break;
                                                                        }
                                                                    case SlotsResultType.JackPot:
                                                                        {
                                                                            Console.WriteLine($"\nJACKPOT!! Congratulations. Your winnings are: {playSlotTuple.Item2}");
                                                                            break;
                                                                        }
                                                                }
                                                                break;
                                                            }
                                                        case 2:
                                                            {

                                                                ua.Logout();
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
                                            }
                                        } while (loginBool);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect username or password.");
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
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input.");
                }
            } while (startupBool);
        }
    }
}
