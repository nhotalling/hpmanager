using System;

namespace DDB.HitPointManager.Core
{
    public static class Calculations
    {
        public static int GetAvgHitPoints(int hitDiceValue)
        {
            return (int)Math.Ceiling((double)hitDiceValue / 2) + 1;
        }

        public static int HalfRoundDown(int value)
        {
            return (int)Math.Floor((double)value / 2);
        }

        public static int GetStatModifier(int statValue)
        {
            return HalfRoundDown(statValue - 10);
        }
    }
}
