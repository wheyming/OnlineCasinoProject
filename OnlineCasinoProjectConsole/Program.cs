using CasinoWebAPI.Common;
using CasinoWebAPI.Controllers;
using CasinoWebAPI.Interfaces;
using OnlineCasinoProjectConsole.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

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
            ICasinoViewModel _mv = new CasinoViewModel();
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
                                    Console.WriteLine("\nPlease input username.");
                                    input11 = Console.ReadLine();
                                    string output = _mv.CheckUserName(input11);
                                    if (!string.IsNullOrWhiteSpace(output))
                                        Console.WriteLine(output);
                                    else
                                        insideMenu = false;
                                } while (insideMenu);
                                insideMenu = true;
                                string input12;
                                do
                                {
                                    Console.WriteLine("\nPlease input id number.");
                                    input12 = Console.ReadLine();
                                    string output = _mv.CheckIDNumber(input12);
                                    if (!string.IsNullOrWhiteSpace(output))
                                        Console.WriteLine(output);
                                    else
                                        insideMenu = false;
                                } while (insideMenu);
                                insideMenu = true;
                                string input13;
                                do
                                {
                                    Console.WriteLine("\nPlease input phone number.");
                                    input13 = Console.ReadLine();
                                    string output = _mv.CheckPhoneNumber(input13);
                                    if (!string.IsNullOrWhiteSpace(output))
                                        Console.WriteLine(output);
                                    else
                                        insideMenu = false;
                                } while (insideMenu);
                                insideMenu = true;
                                string input14;
                                do
                                {
                                    Console.WriteLine("\nPlease input password.");
                                    input14 = Console.ReadLine();
                                    IList<string> outputList = _mv.CheckPassword(input14);
                                    if (!(outputList.Count == 0))
                                        foreach (string output in outputList)
                                            Console.WriteLine(output);
                                    else
                                        insideMenu = false;
                                } while (insideMenu);
                                string signUpOutput = _mv.SignUp(input11, input12, input13, input14);
                                Console.WriteLine(signUpOutput);
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
                                    (string, bool) checkLoginOutput = _mv.CheckLogin(input21, input22);
                                    if (!string.IsNullOrWhiteSpace(checkLoginOutput.Item1))
                                    {
                                        bool loginBool = true;
                                        do
                                        {
                                            Console.WriteLine(checkLoginOutput.Item1);
                                            if (checkLoginOutput.Item2)
                                            {
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
                                                                int input34 = Convert.ToInt32(Console.ReadLine());
                                                                _mv.SetPrizeModuleStatus(input34);
                                                                break;
                                                            }
                                                        case 2:
                                                            {
                                                                Console.WriteLine("Which day would you like to view the financial report for?");
                                                                string input35 = Console.ReadLine();
                                                                DateTime tempDateDay = DateConverter.InputDayConvert(input35);
                                                                string dayOutput = _mv.SeeDayFinancialReport(tempDateDay);
                                                                Console.WriteLine(dayOutput);
                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                Console.WriteLine("Which month of the year would you like to view the financial report for?");
                                                                string input36 = Console.ReadLine();
                                                                DateTime tempDateMonth = DateConverter.InputMonthConvert(input36);
                                                                IList<string> monthOutput = _mv.SeeMonthFinancialReport(tempDateMonth);
                                                                foreach(string report in monthOutput)
                                                                {
                                                                    Console.WriteLine(report);
                                                                }
                                                                break;
                                                            }
                                                        case 4:
                                                            {
                                                                Console.WriteLine("Which year would you like to view the financial report for?");
                                                                string input37 = Console.ReadLine();
                                                                DateTime tempDateYear = DateConverter.InputYearConvert(input37);
                                                                IList<string> yearOutput = _mv.SeeYearFinancialReport(tempDateYear);
                                                                foreach(string report in yearOutput)
                                                                {
                                                                    Console.WriteLine(report);
                                                                }
                                                                break;
                                                            }
                                                        case 5:
                                                            {
                                                                _mv.LogOut();
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
                                                try
                                                {
                                                    int input23 = Convert.ToInt32(Console.ReadLine());
                                                    switch (input23)
                                                    {
                                                        case 1:
                                                            {
                                                                Console.WriteLine("How much money would you like to bet?");
                                                                int input24 = Convert.ToInt32(Console.ReadLine());

                                                                (IList<int>, string) playSlotTuple = _mv.playSlot(input24, input21);
                                                                Console.Write(playSlotTuple.Item1[0]);
                                                                Thread.Sleep(500);
                                                                Console.Write('.');
                                                                Thread.Sleep(500);
                                                                Console.Write(playSlotTuple.Item1[1]);
                                                                Thread.Sleep(500);
                                                                Console.Write('.');
                                                                Thread.Sleep(500);
                                                                Console.Write(playSlotTuple.Item1[2]);
                                                                Console.WriteLine(playSlotTuple.Item2);
                                                                break;
                                                            }
                                                        case 2:
                                                            {

                                                                _mv.LogOut();
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
