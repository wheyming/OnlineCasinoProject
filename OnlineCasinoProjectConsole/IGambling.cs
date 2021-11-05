namespace OnlineCasinoProjectConsole
{
    public interface IGambling
    {
        (int[], double, SlotsResultType) PlaySlot(double betAmount, string username);
    }
}
