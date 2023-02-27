namespace Yetzee;

public class YetzeeTests
{
    private static int[] ParseDie(string s) => s.Split(",").Select(int.Parse).ToArray();

    [Theory]
    [InlineData("1,1,3,3,6", 14)]
    [InlineData("4,5,5,6,1", 21)]
    public void Chance_Is_Sum_Of_Die(string dieString, int expected)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.Chance);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public void Yahtzee_Scores_50_If_All_Die_Have_Same_Value(int dieValue)
    {
        var die = Enumerable.Repeat(dieValue, 5).ToArray();
        var actual = YetzeeGame.Score(die, Category.Yahtzee);
        Assert.Equal(50, actual);
    }

    [Theory]
    [InlineData("1,1,1,2,1")]
    [InlineData("1,2,3,4,5")]
    [InlineData("3,3,3,3,4")]
    public void Yahtzee_Scores_0_If_Not_All_Die_Have_Same_Value(string dieString)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.Yahtzee);
        Assert.Equal(0, actual);
    }

    [Theory]
    [InlineData("1,1,2,4,4", Category.Ones, 2)]
    [InlineData("2,3,2,5,1", Category.Ones, 1)]
    [InlineData("3,3,3,4,5", Category.Ones, 0)]
    [InlineData("1,1,2,4,4", Category.Twos, 2)]
    [InlineData("2,3,2,5,1", Category.Twos, 4)]
    [InlineData("3,3,3,4,5", Category.Twos, 0)]
    [InlineData("1,1,2,4,4", Category.Threes, 0)]
    [InlineData("2,3,2,5,1", Category.Threes, 3)]
    [InlineData("3,3,3,4,5", Category.Threes, 9)]
    [InlineData("1,1,2,4,4", Category.Fours, 8)]
    [InlineData("2,3,2,5,1", Category.Fours, 0)]
    [InlineData("3,3,3,4,5", Category.Fours, 4)]
    [InlineData("1,1,2,4,4", Category.Fives, 0)]
    [InlineData("2,3,2,5,1", Category.Fives, 5)]
    [InlineData("3,5,3,4,5", Category.Fives, 10)]
    [InlineData("1,1,2,4,4", Category.Sixes, 0)]
    [InlineData("6,3,2,5,1", Category.Sixes, 6)]
    [InlineData("3,5,6,6,6", Category.Sixes, 18)]
    public void Numbers_Score_The_Sum_Of_The_Die_With_The_Number(string dieString, Category category, int expected)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, category);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("2,2,1,3,4", 4)]
    [InlineData("1,2,4,3,3", 6)]
    public void A_Pair_Is_Scored_As_Its_Sum(string dieString, int expected)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.Pairs);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("2,2,2,3,4")]
    [InlineData("3,2,4,3,3")]
    [InlineData("3,2,3,3,3")]
    [InlineData("3,3,3,3,3")]
    public void More_Than_2_Are_Not_Pairs(string dieString)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.Pairs);
        Assert.Equal(0, actual);
    }

    [Theory]
    [InlineData("2,3,2,3,4", 6)]
    [InlineData("3,6,6,1,3", 12)]
    public void Pairs_Scores_The_Highest_Pair(string dieString, int expected)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.Pairs);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Two_Pairs_Scores_0_If_There_Is_Only_One_Pair()
    {
        var die = new[] {1, 2, 3, 4, 4};
        var actual = YetzeeGame.Score(die, Category.TwoPairs);
        Assert.Equal(0, actual);
    }

    [Fact]
    public void More_Than_2_Numbers_Are_Not_A_Pair()
    {
        var die = new[] {2, 2, 4, 4, 4};
        var actual = YetzeeGame.Score(die, Category.TwoPairs);
        Assert.Equal(0, actual);
    }

    [Theory]
    [InlineData("1,1,2,3,3", 8)]
    [InlineData("1,2,2,3,3", 10)]
    public void Two_Pairs_Scores_The_Sum_Of_The_Die_In_The_Pairs(string dieString, int expected)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.TwoPairs);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Three_Of_A_Kind_Scores_0_If_There_Are_4_Of_A_Kind()
    {
        var die = new[] {1, 1, 1, 1, 2};
        var actual = YetzeeGame.Score(die, Category.ThreeOfAKind);
        Assert.Equal(0, actual);
    }

    [Fact]
    public void Three_Of_A_Kind_Scores_0_If_There_Is_No_Triplet()
    {
        var die = new[] {1, 1, 3, 2, 2};
        var actual = YetzeeGame.Score(die, Category.ThreeOfAKind);
        Assert.Equal(0, actual);
    }

    [Fact]
    public void Three_Of_A_Kind_Scores_Sum_Of_Triplet()
    {
        var die = new[] {1, 1, 2, 2, 2};
        var actual = YetzeeGame.Score(die, Category.ThreeOfAKind);
        Assert.Equal(6, actual);
    }

    [Fact]
    public void Four_Of_A_Kind_Scores_0_If_There_Are_5_Of_A_Kind()
    {
        var die = new[] {1, 1, 1, 1, 1};
        var actual = YetzeeGame.Score(die, Category.FourOfAKind);
        Assert.Equal(0, actual);
    }

    [Fact]
    public void Four_Of_A_Kind_Scores_0_If_There_Is_No_Quadruplet()
    {
        var die = new[] {1, 1, 1, 2, 3};
        var actual = YetzeeGame.Score(die, Category.FourOfAKind);
        Assert.Equal(0, actual);
    }

    [Theory]
    [InlineData("1,1,1,2,1", 4)]
    [InlineData("3,2,3,3,3", 12)]
    [InlineData("2,2,3,2,2", 8)]
    public void Four_Of_A_Kind_Scores_Sum_Of_Quadruplet(string dieString, int expected)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.FourOfAKind);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("1,2,3,4,5")]
    [InlineData("1,3,2,5,4")]
    [InlineData("5,2,3,4,1")]
    public void Small_Straight_Scores_15_For_Die_From_1_To_5(string dieString)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.SmallStraight);
        Assert.Equal(15, actual);
    }

    [Theory]
    [InlineData("1,1,1,2,1")]
    [InlineData("3,2,3,3,3")]
    [InlineData("2,2,3,2,2")]
    [InlineData("1,2,3,4,6")]
    [InlineData("2,3,4,5,6")]
    public void Small_Straight_Scores_0_For_Invalid_Combinations(string dieString)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.SmallStraight);
        Assert.Equal(0, actual);
    }

    [Theory]
    [InlineData("2,3,4,5,6")]
    [InlineData("6,3,2,5,4")]
    [InlineData("5,2,3,4,6")]
    public void Large_Straight_Scores_20_For_Die_From_2_To_6(string dieString)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.LargeStraight);
        Assert.Equal(20, actual);
    }

    [Theory]
    [InlineData("1,1,1,2,1")]
    [InlineData("3,2,3,3,3")]
    [InlineData("2,2,3,2,2")]
    [InlineData("1,2,3,4,6")]
    [InlineData("1,2,3,4,5")]
    public void Large_Straight_Scores_0_For_Invalid_Combinations(string dieString)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.LargeStraight);
        Assert.Equal(0, actual);
    }

    [Theory]
    [InlineData("1,1,2,2,2", 8)]
    [InlineData("2,2,3,3,4", 0)]
    [InlineData("4,4,4,4,4", 0)]
    public void Full_House_Is_Scored_Correctly(string dieString, int expected)
    {
        var die = ParseDie(dieString);
        var actual = YetzeeGame.Score(die, Category.FullHouse);
        Assert.Equal(expected, actual);
    }
}