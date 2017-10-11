using System;
using GeneticAlgorithm1;

namespace GeneticAlgorithm1Console
{
    class Program
    {
        static void Main(string[] args)
        {
            World world = new World();
            // リミッタ
            int max = 10000;
            while (! world.EndOfDays() && max > 0)
            {
                Console.WriteLine("turn {0}: {1}", world.Time, world.GetMaxPrecisionRatio());
                world.FootstepsOfTime();
                max--;
            }

            foreach (Creature creature in world.Creatures)
            {
                Console.WriteLine("creature {0}", Convert.ToString(creature.Gene.Sequence, 2));
            }

        }
    }
}
