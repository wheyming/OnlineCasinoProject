using System;

namespace Casino.Common
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum PasswordResultType
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
        PasswordNullError = 128,
        /// <summary>
        /// 
        /// </summary>
        None = 256
    }
}
