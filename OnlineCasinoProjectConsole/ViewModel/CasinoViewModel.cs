using Casino.Common;
using OnlineCasinoProjectConsole.Interfaces;
using OnlineCasinoProjectConsole.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace OnlineCasinoProjectConsole.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class CasinoViewModel : ICasinoViewModel
    {
        private readonly HttpClient _casinoClient;
        private readonly IDateConverter _dateConverter;
        /// <summary>
        /// 
        /// </summary>
        public CasinoViewModel(HttpClient httpClient, IDateConverter dateConverter)
        {
            _dateConverter = dateConverter;
            _casinoClient = httpClient;
#if DEBUG
            _casinoClient.BaseAddress = new Uri("https://localhost:44353/");
#else
            _casinoClient.BaseAddress = new Uri("http://mycasino.me");
#endif
            var responseTask = _casinoClient.GetAsync("api");
            responseTask.Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string CheckUserName(string userName)
        {
            string output = string.Empty;
            UserNameResultType checkUserNameResult = UserNameResultType.UnhandledUserError;
            var responseTask = _casinoClient.GetAsync("api/Authentication/checkusername?username=" + userName);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<UserNameResultType>();
                readTask.Wait();
                checkUserNameResult = readTask.Result;
            }
            switch (checkUserNameResult)
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
                    output = "Please create a Username without Space.";
                    break;
                case UserNameResultType.UserNameNullError:
                    output = "Input cannot be Null.";
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
            IdResultType checkIdResult = IdResultType.UnhandledIdError;
            var responseTask = _casinoClient.GetAsync("api/Authentication/checkid?idNumber=" + idNumber);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IdResultType>();
                readTask.Wait();
                checkIdResult = readTask.Result;
            }
            switch (checkIdResult)
            {
                case IdResultType.None:
                    break;
                case IdResultType.DuplicateId:
                    output = "Duplicate idNumber.";
                    break;
                case IdResultType.IdIncorrect:
                    output = "Invalid ID Number";
                    break;
                case IdResultType.IdNullError:
                    output = "Input cannot be Null.";
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
            PhoneNumberResultType checkPhoneNumberResult = PhoneNumberResultType.UnhandledPhoneNumberError;
            var responseTask = _casinoClient.GetAsync("api/Authentication/checkphonenumber?phoneNumber=" + phoneNumber);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<PhoneNumberResultType>();
                readTask.Wait();
                checkPhoneNumberResult = readTask.Result;
            }
            switch (checkPhoneNumberResult)
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
                case PhoneNumberResultType.PhoneNumberNullError:
                    {
                        output = "Input cannot be Null.";
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
            PasswordResultType checkPasswordResult = PasswordResultType.UnhandledPasswordError;
            var responseTask = _casinoClient.GetAsync("api/Authentication/checkpassword?passWord=" + passWord);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<PasswordResultType>();
                readTask.Wait();
                checkPasswordResult = readTask.Result;
            }
            if (Equals(checkPasswordResult & PasswordResultType.PasswordNullError, PasswordResultType.PasswordNullError))
            {
                outputList.Add("Please do not leave password blank.");
            }
            if (Equals(checkPasswordResult & PasswordResultType.PasswordNoUpperCaseLetter, PasswordResultType.PasswordNoUpperCaseLetter))
            {
                outputList.Add("Please choose a password with at least one uppercase letter.");
            }
            if (Equals(checkPasswordResult & PasswordResultType.PasswordNoLowerCaseLetter, PasswordResultType.PasswordNoLowerCaseLetter))
            {
                outputList.Add("Please choose a password with at least one lowercase letter.");
            }
            if (Equals(checkPasswordResult & PasswordResultType.PasswordNoSpecialCharacter, PasswordResultType.PasswordNoSpecialCharacter))
            {
                outputList.Add("Please choose a password with at least one special character.");
            }
            if (Equals(checkPasswordResult & PasswordResultType.PasswordNoDigits, PasswordResultType.PasswordNoDigits))
            {
                outputList.Add("Please choose a password with at least one digit.");
            }
            if (Equals(checkPasswordResult & PasswordResultType.PasswordThreeRepeatedCharacters, PasswordResultType.PasswordThreeRepeatedCharacters))
            {
                outputList.Add("Please choose a password with at most two continuous repeated characters.");
            }
            if (Equals(checkPasswordResult & PasswordResultType.IncorrectPasswordLength, PasswordResultType.IncorrectPasswordLength))
            {
                outputList.Add("Please choose a password between 6 to 24 characters.");
            }
            if (Equals(checkPasswordResult & PasswordResultType.UnhandledPasswordError, PasswordResultType.UnhandledPasswordError))
            {
                outputList.Add("Please contact administrator.");
            }
            if (Equals(checkPasswordResult & PasswordResultType.None, PasswordResultType.None))
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
            string output = string.Empty;
            var responseTask = _casinoClient.GetAsync("api/Authentication/signup?username=" + userName +
                "&idNumber=" + idNumber +
                "&phoneNumber=" + phoneNumber +
                "&password=" + passWord);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<bool>();
                readTask.Wait();
                bool signUpResult = readTask.Result;

                if (signUpResult)
                {
                    output = "New account has been registered.";
                }
            }
            else
            {
                output = "Error has occured.";
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public (string, bool?) CheckLogin(string userName, string passWord)
        {
            string output = string.Empty;
            (bool, bool?) isLoginSuccess = (false, null);
            var responseTask = _casinoClient.GetAsync("api/Authentication/login?username=" + userName + "&password=" + passWord);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<(bool, bool?)>();
                readTask.Wait();
                isLoginSuccess = readTask.Result;
            }
            if (isLoginSuccess.Item1 == true)
            {
                if (isLoginSuccess.Item2 == true)
                {
                    output = "\nWelcome Owner." +
                        "\nWould you like to" +
                        "\n1.) Set prize module?" +
                        "\n2.) View financial report for a certain day?" +
                        "\n3.) View financial report for a certain month?" +
                        "\n4.) View financial report for a certain year?" +
                        "\n5.) Log out.";
                }
                else if (isLoginSuccess.Item2 == false)
                {
                    output = $"Welcome {userName}." +
                        "\nWould you like to" +
                        "\n1.) Play Slots?" +
                        "\n2.) Logout?";
                }
            }
            return (output, isLoginSuccess.Item2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPrizeSetting"></param>
        public string SetPrizeModuleStatus(int inputPrizeSetting)
        {
            var responseTask = _casinoClient.GetAsync("api/Configuration/setprizemodule?inputPrizeSetting=" + inputPrizeSetting);
            responseTask.Wait();
            var result = responseTask.Result;
            string output = string.Empty;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<string>();
                readTask.Wait();
                output = readTask.Result;
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDay"></param>
        /// <returns></returns>
        public string SeeDayFinancialReport(string inputDay)
        {
            DateTime inputDateTime = _dateConverter.InputDayConvert(inputDay);
            double financialReportDay;
            var responseTask = _casinoClient.GetAsync("api/FinancialReport/day?date=" + inputDateTime);
            responseTask.Wait();
            var result = responseTask.Result;
            string output;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<double>();
                readTask.Wait();
                financialReportDay = readTask.Result;
                output = inputDateTime != DateTime.MinValue ?
                $"\nEarnings for {inputDateTime:dd MMMMM yyyy}: ${financialReportDay}" : "Incorrect date format.";
            }
            else
            {
                output = "Unexpected Error Occured.";
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputMonth"></param>
        /// <returns></returns>
        public IList<string> SeeMonthFinancialReport(string inputMonth)
        {
            DateTime inputDateTime = _dateConverter.InputMonthConvert(inputMonth);
            IList<string> monthFinancialReport = new List<string>();
            IList<double> monthValueList = new List<double>();
            if (inputDateTime != DateTime.MinValue)
            {
                var responseTask = _casinoClient.GetAsync("api/FinancialReport/month?monthYear=" + inputDateTime);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<double>>();
                    readTask.Wait();
                    monthValueList = readTask.Result;
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
                    monthFinancialReport.Add("\nUnexpected Error Occured.");
                }
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
            DateTime inputDateTime = _dateConverter.InputYearConvert(inputYear);
            IList<string> yearFinancialReport = new List<string>();
            IList<double> yearValueList = new List<double>();
            if (inputDateTime != DateTime.MinValue)
            {
                var responseTask = _casinoClient.GetAsync("api/FinancialReport/year?year=" + inputDateTime);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<double>>();
                    readTask.Wait();
                    yearValueList = readTask.Result;
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
                    yearFinancialReport.Add("\nUnexpected Error Occured.");
                }
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
        public bool LogOut()
        {
            bool value = true;
            var responseTask = _casinoClient.GetAsync("api/Authentication/logout");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<bool>();
                readTask.Wait();
                value = readTask.Result;
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="betAmount"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public (IList<int>, string) PlaySlot(double betAmount)
        {
            (IList<int>, double, SlotsResultType) playSlotTuple;
            (IList<int>, string) output;
            var responseTask = _casinoClient.GetAsync("api/Gambling/playSlot?betamount=" + betAmount);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<(IList<int>, double, SlotsResultType)>();
                readTask.Wait();
                playSlotTuple = readTask.Result;
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
                            output.Item2 = $"\nDOUBLE!! Congratulations. Your winnings are: ${playSlotTuple.Item2}";
                            break;
                        }
                    case SlotsResultType.Triple:
                        {
                            output.Item2 = $"\nTRIPLE!! Congratulations. Your winnings are: ${playSlotTuple.Item2}";
                            break;
                        }
                    case SlotsResultType.JackPot:
                        {
                            output.Item2 = $"\nJACKPOT!! Congratulations. Your winnings are: ${playSlotTuple.Item2}";
                            break;
                        }
                }
            }
            else
            {
                output = (null, "An Error has Occured.");
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public void ParseInputStringInt(string input, out int? value)
        {
            try
            {
                value = Convert.ToInt32(input);
            }
            catch (FormatException)
            {
                value = null;
            }
            catch (OverflowException)
            {
                value = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        public void ParseInputStringDouble(string input, out double value)
        {
            try
            {
                value = Convert.ToDouble(input);
            }
            catch (FormatException)
            {
                value = 0;
            }
            catch (OverflowException)
            {
                value = 0;
            }
        }
    }
}
