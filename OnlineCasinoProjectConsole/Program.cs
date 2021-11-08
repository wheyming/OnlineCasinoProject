﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using OnlineCasinoProjectConsole.Interfaces;
using OnlineCasinoProjectConsole.ViewModel;

namespace OnlineCasinoProjectConsole
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            bool startupBool = true;
            ICasinoViewModel mv = new CasinoViewModel();
            do
            {
                Console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                Console.WriteLine("Welcome, would you like to\n" +
                                  "1) Sign up\n" +
                                  "2) Login?\n" +
                                  "3) End Program?");
                string inputStr = Console.ReadLine();
                mv.ParseInputString(inputStr, out var input);
                if (input == null)
                    Console.WriteLine("Invalid input." + Environment.NewLine);
                else
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
                                string output = mv.CheckUserName(input11);
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
                                string output = mv.CheckIdNumber(input12);
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
                                string output = mv.CheckPhoneNumber(input13);
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
                                IList<string> outputList = mv.CheckPassword(input14);
                                if (outputList.Count != 0)
                                    foreach (string output in outputList)
                                        Console.WriteLine(output);
                                else
                                    insideMenu = false;
                            } while (insideMenu);
                            string signUpOutput = mv.SignUp(input11, input12, input13, input14);
                            Console.WriteLine(signUpOutput);
                            break;
                        }
                        case 2:
                        {
                            Console.WriteLine("Username: ");
                            string input21 = Console.ReadLine();
                            Console.WriteLine("Password: ");
                            string input22 = Console.ReadLine();
                            (string, bool) checkLoginOutput = mv.CheckLogin(input21, input22);
                            if (!string.IsNullOrWhiteSpace(checkLoginOutput.Item1))
                            {
                                bool loginBool = true;
                                do
                                {
                                    Console.WriteLine(checkLoginOutput.Item1);
                                    if (checkLoginOutput.Item2)
                                    {
                                        inputStr = Console.ReadLine();
                                        mv.ParseInputString(inputStr, out var input33);
                                        if (input == null)
                                            Console.WriteLine("Invalid input." + Environment.NewLine);
                                        else
                                        {
                                            switch (input33)
                                            {
                                                case 1:
                                                {
                                                    Console.WriteLine("Would you like to" +
                                                                      "\n1.) Activate prize giving module?" +
                                                                      "\n2.) Deactivate prize giving module?");
                                                    int input34 = Convert.ToInt32(Console.ReadLine());
                                                    mv.SetPrizeModuleStatus(input34);
                                                    break;
                                                }
                                                case 2:
                                                {
                                                    Console.WriteLine("Which day would you like to view the financial report for?");
                                                    string input35 = Console.ReadLine();
                                                    string dayOutput = mv.SeeDayFinancialReport(input35);
                                                    Console.WriteLine(dayOutput);
                                                    break;
                                                }
                                                case 3:
                                                {
                                                    Console.WriteLine("Which month of the year would you like to view the financial report for?");
                                                    string input36 = Console.ReadLine();
                                                    IList<string> monthOutput = mv.SeeMonthFinancialReport(input36);
                                                    foreach (string report in monthOutput)
                                                        Console.WriteLine(report);

                                                    break;
                                                }
                                                case 4:
                                                {
                                                    Console.WriteLine("Which year would you like to view the financial report for?");
                                                    string input37 = Console.ReadLine();
                                                    IList<string> yearOutput = mv.SeeYearFinancialReport(input37);
                                                    foreach (string report in yearOutput)
                                                        Console.WriteLine(report);

                                                    break;
                                                }
                                                case 5:
                                                {
                                                    mv.LogOut();
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
                                    }
                                    else
                                    {
                                        inputStr = Console.ReadLine();
                                        mv.ParseInputString(inputStr, out var input23);
                                        if (input == null)
                                            Console.WriteLine("Invalid input." + Environment.NewLine);
                                        else
                                        {
                                            switch (input23)
                                            {
                                                case 1:
                                                {
                                                    Console.WriteLine("How much money would you like to bet?");
                                                    int input24 = Convert.ToInt32(Console.ReadLine());
                                                    (IList<int>, string) playSlotTuple = mv.PlaySlot(input24, input21);
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
                                                    mv.LogOut();
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
                                    }
                                } while (loginBool);
                            }
                            else
                                Console.WriteLine("Incorrect username or password.");
                            break;
                        }
                        case 3:
                        {
                            startupBool = false;
                            break;
                        }
                    }
            } while (startupBool);
        }
    }
}
