using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm1
{
    // 世界
    public class World
    {
        // 時間。今回の場合≒ターン数。
        // もっと複雑になったら時間もクラスを分けてFootstepsOfTime()による時間経過はそちらでやる手もある。
        private UInt64 time = 0;
        public UInt64 Time
        {
            get { return time; }
        }

        private List<Creature> creatures;
        public List<Creature> Creatures
        {
            get { return creatures; }
        }

        // 時期イベント
        public event TimeEventHandler TimeEvent;
        public delegate void TimeEventHandler(World w);

        // 創世記()
        public World()
        {
            creatures = new List<Creature>();
            AcceptNewCreature(new Creature(new Gene(Convert.ToUInt32("10101010101010101010101010101010", 2))));
            AcceptNewCreature(new Creature(new Gene(Convert.ToUInt32("01010101010101010101010101010101", 2))));
        }

        public void FootstepsOfTime()
        {
            // 時期イベント
            TimeEvent(this);
            AcceptNewCreature(new Creature(new Gene(Convert.ToUInt32("10101010101010101010101010101010", 2))));
            AcceptNewCreature(new Creature(new Gene(Convert.ToUInt32("01010101010101010101010101010101", 2))));

            // イベント後の始末
            AfterTimeEvent();

            // ターン数追加
            time++;
        }

        private void AfterTimeEvent()
        {
            // 死んだCreatureを清掃
            creatures.RemoveAll(creature => creature.Alive == false);
        }

        public static void Execute()
        {
            World world = new World();
            // リミッタ
            int max = 1000;
            while (!world.EndOfDays() && max > 0)
            {
                world.FootstepsOfTime();

                Console.WriteLine("turn {0}: {1} : {}", world.Time, world.GetMaxPrecisionRatio().ToString());

                max--;
            }
        }


        // 新しいCreatureの受理
        public void AcceptNewCreature(Creature creature)
        {
            uint id;

            // TODO P/Invoke を使用して共有メモリを作成し，その共有メモリ上に Interlocked.Increment を行う
            if (creatures.Count == 0)
            {
                id = 1;
            } else
            {
                // こうなるのは仕方ないとしてもここにあるのはひどい。あと運が悪いとマルチプロセスの時にダブるからロックする必要がある。
                id = creatures.Max(c => c.Id) + 1;
            }
            creature.Id = id;

            // 先にイベントハンドラを追加しておく
            creature.RegistTimeEventHandler(this);
            creatures.Add(creature);
        }

        // 完了チェック
        public Boolean EndOfDays()
        {
            return creatures.Exists(creature => creature.PrecisionRatio == 1);
        }


        // もっとも合致率が高い奴を代表で。
        public double? GetMaxPrecisionRatio()
        {
            return creatures.Max(creature => creature.PrecisionRatio);
        }
    }
}
