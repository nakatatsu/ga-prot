using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeneticAlgorithm1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GeneticAlgorithm1.Tests
{
    [TestClass()]
    public class CreatureTests
    {
        [TestMethod()]
        public void TimeActionTest()
        {
            World world = new World();

            // 0ターン目
            Assert.AreEqual(world.Time, 0u);

            // 一匹目をとる
            Creature creature = world.Creatures.First();
            PrivateObject privateObjectCreature = new PrivateObject(creature);

            // 幼年判定は真
            Assert.AreEqual(creature.Child, true);

            // 無理やりアクションを起こす
            privateObjectCreature.Invoke("TimeAction", new object[] { world });

            // 幼年判定は偽に。
            Assert.AreEqual(creature.Child, false);


            // １ターン目
            var field = world.GetType().GetField("time", BindingFlags.GetField | BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(world, 1u);
            Assert.AreEqual(world.Time, 1u);
        }

        [TestMethod()]
        public void MateTest()
        {
            Creature husband = new Creature(new Gene(0x0000));
            Creature wife    = new Creature(new Gene(0xFFFF));

            PrivateObject PrivateObjectHusband;
            Creature child;

            for (int i=0;i<=19;i++)
            {
                PrivateObjectHusband = new PrivateObject(husband);
                child = (Creature) PrivateObjectHusband.Invoke("Mate", new object[] { wife });

                Console.WriteLine("============");
                Console.WriteLine(child.Gene);
                Console.WriteLine("============");

                // 突然変異があるので可能性としてどれにでもなりうる。

            }

        }

        
        [TestMethod()]
        public void BattleTest()
        {
            World world = new World();

            for (UInt16 i = 0; i <= 16; i++)
            {
                Creature creatureOne = new Creature(new Gene(i));
                PrivateObject PrivateObjectCreatureOne = new PrivateObject(creatureOne);
                PrivateObjectCreatureOne.Invoke("GrowUp");
                world.AcceptNewCreature(creatureOne);
            }

            // 世界開始時の２体がいるので19体
            Assert.AreEqual(world.Creatures.Count, 19);

            Creature creature = world.Creatures.First();
            var PrivateObjectCreature = new PrivateObject(creature);
            PrivateObjectCreature.Invoke("Battle", new object[] { world });

            var PrivateObjectWorld = new PrivateObject(world);
            PrivateObjectWorld.Invoke("AfterTimeEvent");

            // 1体減って18体
            Assert.AreEqual(world.Creatures.Count, 18);
        }

        [TestMethod()]
        public void DieTest()
        {
            Creature creature = new Creature(new Gene(0xFFu));
            Assert.AreEqual(creature.Alive, true);

            creature.Die();

            Assert.AreEqual(creature.Alive, false);
        }
    }
}