namespace GeneticAlgorithm.Logic
{
    public class Population
    {
        private Random rnd;

        public List<Individual> individuals { get; private set; }

        public Population(Random rnd)
        {
            this.rnd = rnd;
        }

        public void SetRandomPopulation(List<Individual> individualsRange, int populationSize)
        {
            individuals = individualsRange.OrderBy(x => rnd.Next()).Take(populationSize).ToList();
        }

        public void SetNewPopulation(List<Individual> individualsRange)
        {
            individuals = individualsRange;
        }
    }
}