using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GeneticAlgorithm.Logic
{
    public class Algorithm
    {
        private double _mutationPropability = 0.3;
        private int _populationSize = 10;
        private int _crossCount = 4;
        private int _mutationCount = 1;

        // Если значение функции повторяется уже 4 раза, значит алгоритм пора останавилвть.
        private int answers = 10;

        private Individual Crossingover(Individual ind1, Individual ind2)
        {
            Random rnd = new Random();
            int minGenSize = Math.Min(ind1.Size, ind2.Size);
            int crossingoverDot = rnd.Next(1, minGenSize);

            string newGenes = ind1.BinaryGen.Substring(0, crossingoverDot) + ind2.BinaryGen.Substring(crossingoverDot);
            int decGen = Convert.ToInt32(newGenes, 2);
            // Проверка: Входит ли ребенок в границы
            if (decGen >= -10 && decGen <= 54)
                return new Individual(binaryGen: newGenes);
            else
                return ind1;
        }

        private Individual Mutate(Individual individual)
        {
            Random rnd = new Random();
            if (rnd.NextDouble() <= _mutationPropability)
            {
                StringBuilder sb = new StringBuilder(individual.BinaryGen);
                int mutationIndex = rnd.Next(0, individual.Size);
                if (sb[mutationIndex] == '0')
                {
                    sb[mutationIndex] = '1';
                }
                else
                {
                    sb[mutationIndex] = '0';
                }
                string newGene = sb.ToString();
                int decGen = Convert.ToInt32(newGene, 2);
                // Проверка: Входит ли ребенок в границы
                if (decGen >= -10 && decGen <= 54)
                    return new Individual(binaryGen: newGene);
                else
                    return individual;
            }
            return individual;
        }

        private void SelectIndividuals(Population population, OptimizationWay way)
        {
            List<Individual> sortedPopulation;
            if (way == OptimizationWay.Max)
            {
                sortedPopulation = population.individuals.OrderByDescending(i => i.Fitness).Take(4).ToList();
            }
            else
            {
                sortedPopulation = population.individuals.OrderBy(i => i.Fitness).Take(4).ToList();
            }
            population.SetNewPopulation(sortedPopulation);
        }

        public void Run(List<Individual> allIndividuals)
        {
            Random rnd = new Random();
            Population population = new Population(rnd);
            population.SetRandomPopulation(allIndividuals, _populationSize);
            int generation = 0;
            int dublicateAnswers = 0;
            double prevAnswer = 0;
            while (dublicateAnswers != answers)
            {
                // Делаем кроссинговеры
                for (int i = 0; i < _crossCount; i++)
                {
                    var ind1 = population.individuals[rnd.Next(0, population.individuals.Count)];
                    var ind2 = population.individuals[rnd.Next(0, population.individuals.Count)];
                    var child = Crossingover(ind1, ind2);
                    // Пробуем мутировать и в итоге детей добавляем в популяцию
                    child = Mutate(child);
                    population.individuals.Add(child);
                }
                // Отбираем тех, у кого y - самый маленький, тк ищем минимум
                SelectIndividuals(population, OptimizationWay.Min);
                generation++;

                Console.WriteLine($"Поколение: {generation}, минимум y = {population.individuals.First().Fitness} при x = {population.individuals.First().DecimalGen}");
                if (prevAnswer == population.individuals.First().Fitness)
                    dublicateAnswers++;
                prevAnswer = population.individuals.First().Fitness;
            }
        }
    }
}