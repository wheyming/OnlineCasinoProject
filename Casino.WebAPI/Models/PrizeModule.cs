using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Casino.WebAPI.Models
{
    public class PrizeModule
    {
        public bool IsPrizeEnabled { get; private set; }
        public PrizeModule(bool isPrizeEnabled)
        {
            IsPrizeEnabled = isPrizeEnabled;
        }
    }
}