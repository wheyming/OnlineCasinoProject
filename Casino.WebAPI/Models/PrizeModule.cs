using System.ComponentModel.DataAnnotations;

namespace Casino.WebAPI.Models
{
    public class PrizeModule
    {
        [Key]
        public int TimesChanged { get; set; }

        public int Identifier { get; set; }

        public bool IsPrizeEnabled { get; private set; }

        public PrizeModule()
        { }

        public PrizeModule(bool isPrizeEnabled)
        {
            TimesChanged.GetType();
            Identifier = 1;
            IsPrizeEnabled = isPrizeEnabled;
        }
    }
}