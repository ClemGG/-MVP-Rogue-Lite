namespace Project.DiceRandom
{
    /// <summary>
    /// Used to simulate the randomness of a dice.
    /// </summary>
    public static class Dice
    {
        /// <summary>
        /// Returns all results of the dice roll.
        /// </summary>
        /// <param name="nbDies"> Number of rolls.</param>
        /// <param name="nbSides">Number of sides a dice has.</param>
        /// <param name="nbSides">All results of the dice roll (must have been initialized prior).</param>
        public static int[] Roll(int nbDies, int nbSides, int[] results)
        {
            for (int i = 0; i < nbDies; i++)
            {
                results[i] = UnityEngine.Random.Range(1, nbSides + 1);
            }

            return results;
        }

        public static int RollSum(int nbDies, int nbSides)
        {
            int result = 0;
            for (int i = 0; i < nbDies; i++)
            {
                result += UnityEngine.Random.Range(1, nbSides + 1);
            }

            return result;
        }
    }
}