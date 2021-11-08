using System;
using System.Collections.Generic;
using System.Globalization;
using CasinoWebAPI.Common;
using CasinoWebAPI.Controllers;
using CasinoWebAPI.Interfaces;
using OnlineCasinoProjectConsole.Interfaces;
using OnlineCasinoProjectConsole.Utility;

namespace OnlineCasinoProjectConsole.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    internal class CasinoViewModel : ICasinoViewModel
    {
        private readonly IConfigurationManager _config;
        private readonly IAuthenticationManager _ua;
        private readonly IReportManager _fr;
        private readonly IGamblingManager _gambling;
        /// <summary>
        /// 
        /// </summary>
        internal CasinoViewModel()
        {
            _config = new ConfigurationController();
            _ua = new AuthenticationController();
            _fr = new ReportController();
            _gambling = new GamblingController(_config, _fr);
            _fr.ReportListInitialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CheckUserName(string userName)
        {
            string output = string.Empty;
            UserNameResultType result = _ua.CheckUserName(userName);
            switch (result)
            {
                case UserNameResultType.None:
                    break;
                case UserNameResultType.DuplicateUser:
                    output = "Duplicate Username.";
                    break;
                case UserNameResultType.UnhandledUserError:
                    output = "Unexpected Error.";
                    break;
                case UserNameResultType.UserNameContainsSpace:
                    output = "Please Create A Username Without Space.";
                    break;
                case UserNameResultType.UserNameDataAccessError:
                    output = "Unable to find file.";
                    break;
                case UserNameResultType.UserNameLengthIncorrect:
                    output = "Please create a username between 6 to 24 characters.";
                    break;
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public string CheckIdNumber(string idNumber)
        {
            string output = string.Empty;
            IdResultType result = _ua.CheckId(idNumber);
            switch (result)
            {
                case IdResultType.None:
                    break;
                case IdResultType.DuplicateId:
                    output = "Duplicate idNumber.";
                    break;
                case IdResultType.IdIncorrect:
                    output = "Invalid ID Number";
                    break;
                case IdResultType.IdDataAccessError:
                    output = "Unable to find file.";
                    break;
                case IdResultType.UnhandledIdError:
                    output = "Unexpected Error.";
                    break;
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public string CheckPhoneNumber(string phoneNumber)
        {
            string output = string.Empty;
            PhoneNumberResultType result = _ua.CheckPhoneNumber(phoneNumber);
            switch (result)
            {
                case PhoneNumberResultType.None:
                    {
                        break;
                    }
                case PhoneNumberResultType.DuplicatePhoneNumber:
                    {
                        output = "Duplicate Phone Number.";
                        break;
                    }
                case PhoneNumberResultType.PhoneNumberIncorrect:
                    {
                        output = "Invalid Phone Number";
                        break;
                    }
                case PhoneNumberResultType.PhoneNumberDataAccessError:
                    {
                        output = "Unable to find file.";
                        break;
                    }
                case PhoneNumberResultType.UnhandledPhoneNumberError:
                    {
                        output = "Unexpected Error.";
                        break;
                    }
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public IList<string> CheckPassword(string passWord)
        {
            IList<string> outputList = new List<string>();
            PasswordResultType result = _ua.CheckPassword(passWord);
            if (Equals(result & PasswordResultType.PasswordNoUpperCaseLetter, PasswordResultType.PasswordNoUpperCaseLetter))
            {
                outputList.Add("Please choose a password with at least one uppercase letter.");
            }
            if (Equals(result & PasswordResultType.PasswordNoLowerCaseLetter, PasswordResultType.PasswordNoLowerCaseLetter))
            {
                outputList.Add("Please choose a password with at least one lowercase letter.");
            }
            if (Equals(result & PasswordResultType.PasswordNoSpecialCharacter, PasswordResultType.PasswordNoSpecialCharacter))
            {
                outputList.Add("Please choose a password with at least one special character.");
            }
            if (Equals(result & PasswordResultType.PasswordNoDigits, PasswordResultType.PasswordNoDigits))
            {
                outputList.Add("Please choose a password with at least one digit.");
            }
            if (Equals(result & PasswordResultType.PasswordThreeRepeatedCharacters, PasswordResultType.PasswordThreeRepeatedCharacters))
            {
                outputList.Add("Please choose a password with at most two continuous repeated characters.");
            }
            if (Equals(result & PasswordResultType.IncorrectPasswordLength, PasswordResultType.IncorrectPasswordLength))
            {
                outputList.Add("Please choose a password between 6 to 24 characters.");
            }
            if (Equals(result & PasswordResultType.UnhandledPasswordError, PasswordResultType.UnhandledPasswordError))
            {
                outputList.Add("Please contact administrator.");
            }
            if (Equals(result & PasswordResultType.None, PasswordResultType.None))
            {
            }
            return outputList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="idNumber"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public string SignUp(string userName, string idNumber, string phoneNumber, string passWord)
        {
            _ua.SignUp(userName, idNumber, phoneNumber, passWord);
            string output = "New account has been registered.";
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public (string, bool) CheckLogin(string userName, string passWord)
        {
            string output = string.Empty;
            bool isOwner = false;
            if (_ua.Login(userName, passWord))
            {
                if (_ua.CurrentUser.IsOwner)
                {
                    output = ("\nWelcome Owner." +
                        "\nWould you like to" +
                        "\n1.) Set prize module?" +
                        "\n2.) View financial report for a certain day?" +
                        "\n3.) View financial report for a certain month?" +
                        "\n4.) View financial report for a certain year?" +
                        "\n5.) Log out.");
                    isOwner = true;
                }
                else
                {
                    output = ($"Welcome {userName}." +
                        "\nWould you like to" +
                        "\n1.) Play Slots?" +
                        "\n2.) Logout?");
                }
            }
            return (output, isOwner);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPrizeSetting"></param>
        public void SetPrizeModuleStatus(int inputPrizeSetting)
        {
            _config.SetPrizeModuleStatus(inputPrizeSetting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDay"></param>
        /// <returns></returns>
        public string SeeDayFinancialReport(string inputDay)
        {
            DateTime inputDateTime = DateConverter.InputDayConvert(inputDay);
            var output = inputDateTime != DateTime.MinValue ? 
                $"\nEarnings for {inputDateTime:dd MMMMM yyyy}: ${_fr.GenerateFinancialReportDay(inputDateTime)}" : "Incorrect date format.";
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMonth"></param>
        /// <returns></returns>
        public IList<string> SeeMonthFinancialReport(string inputMonth)
        {
            DateTime inputDateTime = DateConverter.InputDayConvert(inputMonth);
            IList<string> monthFinancialReport = new List<string>();
            if (inputDateTime != DateTime.MinValue)
            {
                List<double> monthValueList = _fr.GenerateFinancialReportMonth(inputDateTime);
                monthFinancialReport.Add("\n");
                for (int i = 1; i < 32; i++)
                {
                    if (monthValueList[i] != 0.0)
                    {
                        monthFinancialReport.Add($"{i} {inputDateTime:MMMM yyyy}: ${monthValueList[i]}");
                    }
                }
                monthFinancialReport.Add($"\nEarnings for {inputDateTime:MMMMM yyyy}: ${monthValueList[0]}");
            }
            else
            {
                monthFinancialReport.Add("\nIncorrect date format.");
            }
            return monthFinancialReport;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputYear"></param>
        /// <returns></returns>
        public IList<string> SeeYearFinancialReport(string inputYear)
        {
            DateTime inputDateTime = DateConverter.InputDayConvert(inputYear);
            IList<string> yearFinancialReport = new List<string>();
            if (inputDateTime != DateTime.MinValue)
            {
                List<double> yearValueList = _fr.GenerateFinancialReportYear(inputDateTime);
                yearFinancialReport.Add("\n");
                for (int i = 1; i < 13; i++)
                {
                    if (yearValueList[i] != 0.0)
                    {
                        yearFinancialReport.Add($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)} {inputDateTime:yyyy}: ${yearValueList[i]}");
                    }
                }
                yearFinancialReport.Add($"\nEarnings for {inputDateTime:yyyy}: ${yearValueList[0]}");
            }
            else
            {
                yearFinancialReport.Add("\nIncorrect date format.");
            }
            return yearFinancialReport;
        }

        /// <summary>
        /// 
        /// </summary>
        public void LogOut()
        {
            _ua.Logout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public (IList<int>, string) PlaySlot(int betAmount, string userName)
        {
            (IList<int>, double, SlotsResultType) playSlotTuple = _gambling.PlaySlot(betAmount, userName);
            (IList<int>, string) output;
            output.Item1 = playSlotTuple.Item1;
            output.Item2 = string.Empty;
            switch (playSlotTuple.Item3)
            {
                case SlotsResultType.None:
                    {
                        output.Item2 = "\nUnfortunately, you did not win anything. Thank you for playing.";
                        break;
                    }
                case SlotsResultType.Double:
                    {
                        output.Item2 = $"\nDOUBLE!! Congratulations. Your winnings are: {playSlotTuple.Item2}";
                        break;
                    }
                case SlotsResultType.Triple:
                    {
                        output.Item2 = $"\nTRIPLE!! Congratulations. Your winnings are: {playSlotTuple.Item2}";
                        break;
                    }
                case SlotsResultType.JackPot:
                    {
                        output.Item2 = $"\nJACKPOT!! Congratulations. Your winnings are: {playSlotTuple.Item2}";
                        break;
                    }
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public void ParseInputString(string input, out int? value)
        {
            try
            {
                value = Convert.ToInt32(input);
            }
            catch (FormatException)
            {
                value = null;
            }
            catch (ArgumentOutOfRangeException)
            {
                value = null;
            }
        }
    }
}
