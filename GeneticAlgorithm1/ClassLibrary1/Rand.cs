using System;

namespace GeneticAlgorithm1
{
    // もっと徹底するならこちらを参考に。
    // http://neue.cc/2013/03/06_399.html
    public static class Rand
    {
        private static int seed = Environment.TickCount;

        // min以上max以下の整数をランダムで返す。
        public static int Get(int min, int max)
        {
            // 溢れることなどありえないとは思うが念のため
            if (seed >= 0x7FFFFFF0)
            {
                seed = 0;
            }

            return new Random(seed++).Next(min, max + 1);
        }
    }
}
