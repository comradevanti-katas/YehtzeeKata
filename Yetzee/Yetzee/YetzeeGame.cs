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

        switch (category)
        {
            case Category.Yahtzee when HasAllSame():
                return 50;
            case Category.Yahtzee:
                return 0;
            case Category.Ones:
                return CountOf(1);
            case Category.Twos:
                return CountOf(2) * 2;
            case Category.Threes:
                return CountOf(3) * 3;
            case Category.Fours:
                return CountOf(4) * 4;
            case Category.Fives:
                return CountOf(5) * 5;
            case Category.Sixes:
                return CountOf(6) * 6;
            case Category.Pairs:
            {
                var pairs = FindPairs();
                if (pairs.Length >= 1)
                    return pairs.Max() * 2;
                return 0;
            }
            case Category.TwoPairs:
            {
                var pairs = FindPairs();
                if (pairs.Length == 2)
                    return pairs.Sum() * 2;
                return 0;
            }
            case Category.Chance:
                return die.Sum();
            default:
                throw new ArgumentException("Unknown category");
        }
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