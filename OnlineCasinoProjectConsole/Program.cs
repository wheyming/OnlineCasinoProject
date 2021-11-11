using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace OnlineCasinoProjectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool startupBool = true;
            ICasinoConfiguration config = new CasinoConfiguration();
            IFileHandling FH = new FileHandling();
            IUserAuthentication UA = new UserAuthentication(FH);
            IFinancialReport FR = new FinancialReport(FH);
            ICustomRandom CR = new CustomRandom();
            IGambling gambling = new Gambling(FH, CR, config, FR); string input1_1;
            string input1_2;
            string input1_3;
            string input1_4;
            FR.ReportListInitialize();
            do
            {
                try
                {
                    Console.WriteLine(DateTime.Now.ToString());
                    Console.WriteLine("Welcome, would you like to\n" +
                        "1) Signup\n" +
                        "2) Login?\n" +
                        "3) End Program?");
                    int input = Convert.ToInt32(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
                            {
                                bool insideMenu = true;
                                do
                                {
                                    Console.WriteLine("Please input username.");
                                    input1_1 = Console.ReadLine();
                                    UserNameResultType result = UA.CheckUserName(input1_1);
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
                                do
                                {
                                    Console.WriteLine("Please input id number.");
                                    input1_2 = Console.ReadLine();
                                    IdResultType result = UA.CheckId(input1_2);
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
                                do
                                {
                                    Console.WriteLine("Please input phone number.");
                                    input1_3 = Console.ReadLine();
                                    PhoneNumberResultType result = UA.CheckPhoneNumber(input1_3);
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
                                do
                                {
                                    Console.WriteLine("Please input password.");
                                    input1_4 = Console.ReadLine();
                                    PasswordResultType result = UA.CheckPassword(input1_4);
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
                                UA.SignUp(input1_1, input1_2, input1_3, input1_4);
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
                                    if (UA.Login(input2_1, input2_2) == true)
                                    {
                                        bool loginBool = true;
                                        do
                                        {
                                            if (UA.CurrentUser.IsOwner)
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
                                                                bool input3_4 = Convert.ToBoolean(Console.ReadLine());
                                                                config.SetPrizeModuleStatus(input3_4);
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Console.WriteLine("Which day would you like to view the financial report for?");
                                                                string input3_5 = Console.ReadLine();
                                                                DateTime tempDateDay = DateConverter.InputDayConvert(input3_5);
                                                                if (tempDateDay != DateTime.MinValue)
                                                                {
                                                                    Console.WriteLine($"\nEarnings for {tempDateDay:dd MMMMM yyyy}: ${FR.GenerateFinancialReportDay(tempDateDay)}");
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
                                                                DateTime tempDateMonth = DateConverter.InputMonthConvert(input3_6);
                                                                if (tempDateMonth != DateTime.MinValue)
                                                                {
                                                                    List<double> monthValueList = FR.GenerateFinancialReportMonth(tempDateMonth);
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
                                                                string input3_7 = Console.ReadLine();
                                                                DateTime tempDateYear = DateConverter.InputYearConvert(input3_7);
                                                                if (tempDateYear != DateTime.MinValue)
                                                                {
                                                                    List<double> yearValueList = FR.GenerateFinancialReportYear(tempDateYear);
                                                                    Console.WriteLine("\n");
                                                                    for (int i = 1; i < 13; i++)
                                                                    {
                                                                        if (!(yearValueList[i] == 0.0))
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
                                                                UA.Logout();
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
                                                Console.WriteLine($"Welcome {input2_1}");
                                                Console.WriteLine("Would you like to" +
                                                    "\n1.) Play Slots?" +
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

                                                                (int[], double, SlotsResultType) playSlotTuple = gambling.PlaySlot(input2_4, input2_1);
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

                                                                UA.Logout();
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
                                        } while (loginBool == true);
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
            } while (startupBool == true);
        }

    }
}
