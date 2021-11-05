using System;

namespace OnlineCasinoProjectConsole
{
    [Flags]
    public enum PasswordResultType
    {
        IncorrectPasswordLength = 1,
        PasswordNoUpperCaseLetter = 2,
        PasswordNoLowerCaseLetter = 4,
        PasswordNoDigits = 8,
        PasswordNoSpecialCharacter = 16,
        PasswordThreeRepeatedCharacters = 32,
        UnhandledPasswordError = 64,
        None = 128
    }
}
