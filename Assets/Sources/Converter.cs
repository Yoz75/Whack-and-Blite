
namespace WhackAndBlite
{
    public static class Converter
    {
        private const float Coins2ScoreMultiplier = 1000;
        public static float Score2Coins(long score)
        {
            return score / Coins2ScoreMultiplier;
        }
    }
}