namespace Yetzee;

public static class YetzeeGame
{

    private static readonly int[] dieValues = {1, 2, 3, 4, 5, 6};
    
    public static int Score(int[] die, Category category)
    {
        int CountOf(int num) =>
            die.Count(it => it == num);

        bool HasPairOf(int pairNumber) =>
            CountOf(pairNumber) == 2;

        bool HasAllSame() => 
            die.Distinct().Count() == 1;

        int[] FindPairs() => 
            dieValues.Where(HasPairOf).ToArray();

        if (category == Category.Yahtzee)
            if (HasAllSame())
                return 50;
            else
                return 0;

        if (category == Category.Ones)
            return CountOf(1);
        if (category == Category.Twos)
            return CountOf(2) * 2;
        if (category == Category.Threes)
            return CountOf(3) * 3;
        if (category == Category.Fours)
            return CountOf(4) * 4;
        if (category == Category.Fives)
            return CountOf(5) * 5;
        if (category == Category.Sixes)
            return CountOf(6) * 6;
        if (category == Category.Pairs)
        {
            var pairs = FindPairs();
            if (pairs.Length >= 1)
                return pairs.Max() * 2;
            return 0;
        }
        if (category == Category.TwoPairs)
        {
            var pairs = FindPairs();
            if (pairs.Length == 2)
                return pairs.Sum() * 2;
            return 0;
        }
        
        return die.Sum();
    }
}

public enum Category
{
    Chance,
    Yahtzee,
    Ones,
    Twos,
    Threes,
    Fours,
    Fives,
    Sixes,
    Pairs,
    TwoPairs
}