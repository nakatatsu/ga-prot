using System;

namespace GeneticAlgorithm1
{
    public class Gene
    {
        private UInt32 sequence;

        public UInt32 Sequence
        {
            get
            {
                return sequence;
            }
        }

        public Gene(UInt32 sequence)
        {
            this.sequence = sequence;
        }

        // 他のGeneとの適合率を計算する
        public double PrecisionRatio(Gene otherGene)
        {
            UInt32 xor = sequence ^ otherGene.Sequence;
            double rate = 1d - Utils.BitCount(xor) / 32d;

            return rate;
        }

        // 交差
        public Gene Crossover(Gene otherGene)
        {
            UInt32 prefixSequence, suffixSequence;
            if (Rand.Get(0, 1) == 1)
            {
                prefixSequence = sequence;
                suffixSequence = otherGene.Sequence;
            }
            else
            {
                prefixSequence = otherGene.Sequence;
                suffixSequence = sequence;
            }

            UInt32 newSequence = Utils.ExchangeBit(prefixSequence, suffixSequence, Rand.Get(1, 31));

            return new Gene(newSequence);
        }



        // 突然変異を試す
        public void TryMutation()
        {
            if (Rand.Get(1, 100) <= 30)
            {
                int changePoint = Rand.Get(0, 31);
                sequence = Utils.SwitchBit(sequence, changePoint);
            }
        }
    }
}
