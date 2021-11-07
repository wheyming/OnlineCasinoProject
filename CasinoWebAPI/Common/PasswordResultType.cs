using System;

namespace CasinoWebAPI.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    internal enum PasswordResultType
    {
        /// <summary>
        /// 
        /// </summary>
        IncorrectPasswordLength = 1,
        /// <summary>
        /// 
        /// </summary>
        PasswordNoUpperCaseLetter = 2,
        /// <summary>
        /// 
        /// </summary>
        PasswordNoLowerCaseLetter = 4,
        /// <summary>
        /// 
        /// </summary>
        PasswordNoDigits = 8,
        /// <summary>
        /// 
        /// </summary>
        PasswordNoSpecialCharacter = 16,
        /// <summary>
        /// 
        /// </summary>
        PasswordThreeRepeatedCharacters = 32,
        /// <summary>
        /// 
        /// </summary>
        UnhandledPasswordError = 64,
        /// <summary>
        /// 
        /// </summary>
        None = 128
    }
}
