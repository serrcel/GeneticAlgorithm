namespace GeneticAlgorithm.Logic
{
    public class Individual
    {
        public string BinaryGen { get; private set; }
        public int DecimalGen { get; private set; }
        public double Fitness { get; private set; }
        public int Size { get => BinaryGen.Length; }

        public Individual(int decimalGen = 0, string binaryGen = "")
        {
            if (binaryGen != "")
            {
                this.BinaryGen = binaryGen;
                this.DecimalGen = Convert.ToInt32(this.BinaryGen, 2);
            }
            else if (decimalGen != 0)
            {
                this.DecimalGen = decimalGen;
                this.BinaryGen = Convert.ToString(this.DecimalGen, 2);
            }
            else
            {
                this.DecimalGen = 0;
                this.BinaryGen = "0";
            }
            Fitness = FitnessFunction.Function(this);
        }

    }
}