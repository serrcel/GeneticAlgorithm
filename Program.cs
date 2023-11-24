using GeneticAlgorithm.Logic;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main()
        {
            int leftBorder = -10;
            int rightBorder = 53;
            List<Individual> numRange = new List<Individual>();

            for (int i = leftBorder; i < rightBorder + 1; i++)
            {
                numRange.Add(new Individual(i));
            }

            Algorithm geneticAlgorithm = new Algorithm();
            geneticAlgorithm.Run(numRange);
        }
    }
}

