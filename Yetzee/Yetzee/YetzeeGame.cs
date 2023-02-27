namespace Yetzee;

public static class YetzeeGame
{
    private static readonly int[] allDieValues = {1, 2, 3, 4, 5, 6};

    public static int Score(int[] die, Category category)
    {
        int CountOf(int num) =>
            die.Count(it => it == num);
        
        bool HasPairOf(int pairNumber) =>
            CountOf(pairNumber) == 2;

        bool HasTripletsOf(int tripletNumber) =>
            CountOf(tripletNumber) == 3;

        bool HasQuadrupletsOf(int quadNumber) =>
            CountOf(quadNumber) == 4;

        bool HasAllSame() =>
            die.Distinct().Count() == 1;

        int[] FindPairs() =>
            allDieValues.Where(HasPairOf).ToArray();
        
        int? TryFindTriplet()
        {
            var triplets = allDieValues.Where(HasTripletsOf).ToArray();
            if (triplets.Length > 0) return triplets[0];


            return null;
        }

        int? TryFindQuadruplet()
        {
            var quad = allDieValues.Where(HasQuadrupletsOf).ToArray();
            if (quad.Length > 0) return quad[0];
            return null;
        }

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
            case Category.ThreeOfAKind:
                var triplet = TryFindTriplet();
                if (triplet != null)
                    return triplet.Value * 3;
                return 0;
            case Category.FourOfAKind:
                var quad = TryFindQuadruplet();
                if (quad != null)
                    return quad.Value * 4;
                return 0;
            case Category.SmallStraight:
                return die.Distinct().Sum() == 15 ? 15 : 0;
            case Category.LargeStraight:
                return die.Distinct().Sum() == 20 ? 20 : 0;
            case Category.FullHouse:
                if (TryFindTriplet() != null && FindPairs().Length == 1)
                    return die.Sum();
                return 0;
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
    TwoPairs,
    ThreeOfAKind,
    FourOfAKind,
    SmallStraight,
    LargeStraight,
    FullHouse
}