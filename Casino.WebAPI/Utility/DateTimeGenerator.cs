using Casino.WebAPI.Interfaces;
using System;

namespace Casino.WebAPI.Utility
{
    public class DateTimeGenerator : IDateTimeGenerator
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}