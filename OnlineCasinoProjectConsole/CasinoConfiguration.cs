namespace OnlineCasinoProjectConsole
{
    public class CasinoConfiguration : ICasinoConfiguration
    {
        public bool IsPrizeEnabled { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public void SetPrizeModuleStatus(bool status)
        {
            IsPrizeEnabled = status;
        }
    }
}
