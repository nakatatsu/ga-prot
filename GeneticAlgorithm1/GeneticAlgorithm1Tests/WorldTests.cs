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
    public class WorldTests
    {
        [TestMethod()]
        public void WorldTest()
        {
            World world = new World();
            Assert.AreEqual(world.Creatures.Count, 2);
        }

        [TestMethod()]
        public void FootstepsOfTimeTest()
        {
            World world = new World();
            Assert.AreEqual(world.Time, 0u);
            world.FootstepsOfTime();
            Assert.AreEqual(world.Time, 1u);

        }

        [TestMethod()]
        public void AcceptNewCreatureTest()
        {
            World world = new World();
            Assert.AreEqual(world.Creatures.Count, 2);
            world.AcceptNewCreature(new Creature(new Gene(0x0101u)));
            Assert.AreEqual(world.Creatures.Count, 3);
            Assert.AreEqual(world.Creatures[2].Gene.Sequence, 0x0101u);
        }

        [TestMethod()]
        public void EndOfDaysTest()
        {
            World world = new World();
            Assert.IsFalse(world.EndOfDays());
            world.AcceptNewCreature(new Creature(new Gene(Creature.BestGeneSequence)));
            Assert.IsTrue(world.EndOfDays());
        }

        [TestMethod()]
        public void GetMaxPrecisionRatioTest()
        {
            World world = new World();
            Assert.AreEqual(world.GetMaxPrecisionRatio(), 24d / 32d);
            world.AcceptNewCreature(new Creature(new Gene(Creature.BestGeneSequence)));
            Assert.AreEqual(world.GetMaxPrecisionRatio(), 1d);
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            World.Execute();
        }
    }
}