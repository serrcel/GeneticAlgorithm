namespace GeneticAlgorithm.Logic
{
    public class FitnessFunction
    {
        public static double Function(Individual individual)
        {
            int x = individual.DecimalGen;
            return 62 - x - 86 * Math.Pow(x, 2) + 2 * Math.Pow(x, 3);
        }
    }
}