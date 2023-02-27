namespace Yetzee;

public static class YetzeeGame
{
    
    private static int Count(this IEnumerable<int> numbers, int num) =>
        numbers.Count(it => it == num);

    private static bool HasPairOf(this int[] die, int pairNumber)
    {
        return die.Count(pairNumber) == 2;
    }

    private static int[] FindPairs()
    {
        
    }
    
    
    public static int Score(int [] die, Category category)
    {
        if(category==Category.Yahtzee)
            if (die.Distinct().Count() == 1)
                return 50;
            else
            {
                return 0;
            }

        if (category == Category.Ones)
            return die.Count(1);
        if (category == Category.Twos)
            return die.Count(2)*2;
        if (category == Category.Threes)
            return die.Count(3)*3;
        if (category == Category.Fours)
            return die.Count(4)*4;
        if (category == Category.Fives)
            return die.Count(5)*5;
        if (category == Category.Sixes)
            return die.Count(6)*6;
        if (category == Category.Pairs)
        {
            for (int i = 6; i >=1; i--)
            {
                if (die.HasPairOf(i))
                    return i*2;
            }

            return 0;

        }
            
        
        return die.Sum();
    }

    
}
public enum Category
{
    Chance,Yahtzee,Ones,Twos,Threes,Fours,Fives,Sixes,Pairs,TwoPairs
}