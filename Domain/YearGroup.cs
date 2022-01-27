namespace LunchPriceCharging.Domain
{
    public class YearGroup
    {
        public int YearNumber;

        // students in years 0 (reception), 1 and 2 always get a free meal
        public bool FreeMeal => (YearNumber <= 2);
    }
}
