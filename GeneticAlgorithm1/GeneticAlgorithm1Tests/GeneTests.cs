using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm1.Tests
{
    [TestClass()]
    public class GeneTests
    {
        [TestMethod()]
        public void PrecisionRatioTest()
        {
            Gene gene = new Gene(Convert.ToUInt32("01010101010101010101010101010101", 2));
            Assert.AreEqual(gene.PrecisionRatio(new Gene(Convert.ToUInt32("01010101010101010101010101010101", 2))), 1);
            Assert.AreEqual(gene.PrecisionRatio(new Gene(Convert.ToUInt32("10101010101010101010101010101010", 2))), 0);
            Assert.AreEqual(gene.PrecisionRatio(new Gene(Convert.ToUInt32("00000000000000000000000000000000", 2))), 0.5);
            Assert.AreEqual(gene.PrecisionRatio(new Gene(Convert.ToUInt32("11111111111111111111111111111111", 2))), 0.5);
            Assert.AreEqual(gene.PrecisionRatio(new Gene(Convert.ToUInt32("00001010101010101010101010101010", 2))), 0.0625);
            Assert.AreEqual(gene.PrecisionRatio(new Gene(Convert.ToUInt32("01010101010101010101010101010000", 2))), 0.9375);

        }

        [TestMethod()]
        public void CrossoverTest()
        {
            UInt32 sequence = Convert.ToUInt32("01010101010101010101010101010101", 2);

            Gene gene1 = new Gene(sequence);
            Gene gene2 = new Gene(Convert.ToUInt32("10101010101010101010101010101010", 2));

            Gene newGene = gene1.Crossover(gene2);

            Assert.AreNotEqual(gene1.Sequence, newGene.Sequence);
            Assert.AreNotEqual(gene2.Sequence, newGene.Sequence);
        }

        [TestMethod()]
        public void TryMutationTest()
        {
            Gene gene = new Gene(0x00);

            // 3%あるので３００回も繰り返せば大抵当たるだろうというテストに似たなにか。
            for (int i = 0;i<=300;i++)
            {
                gene.TryMutation();
            }

            Assert.AreNotEqual(gene.Sequence, 0x00);

        }


    }
}