using System;
using System.Collections.Generic;

namespace GeneticAlgorithm1
{
    public class Creature
    {
        public static readonly UInt32 BestGeneSequence = 0x00004EA5u; // 0000 0000 0000 0000 0100 1110 1010 0101

        private Boolean alive = true; // 生存判定
        private Boolean child = true; // 幼年判定
        private Gene gene;
        private double? precisionRatio = null;

        public uint       Id { get; set; }
        public Boolean Alive { get { return alive; } }
        public Boolean Child { get { return child; } }
        public Gene    Gene  { get { return gene; } }

        public Creature(Gene gene)
        {
            this.gene = gene;
        }

        // BestGeneとの適合率を図る
        public double? PrecisionRatio
        {
            get
            {
                if (precisionRatio != null) return precisionRatio;

                double rate = gene.PrecisionRatio(new Gene(BestGeneSequence));

                precisionRatio = rate;
                
                return precisionRatio;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="world"></param>
        public void RegistTimeEventHandler(World world)
        {
            world.TimeEvent += new World.TimeEventHandler(TimeAction);
        }

        // 時期ごとの行動
        private void TimeAction(World world)
        {
            // 自分が死亡済の場合は何もしない
            if (! Alive) return;

            // Creatureは成人し、子孫を生み、（＝交差、突然変異） 生存競争をして数を一定数以下にする(＝評価、選択)。
            // 現実世界であればそれぞれバラバラにそれが起こるのだろうが、今回は時期に合わせて一斉に起こる想定。
            switch (world.Time % 3)
            {
                case 0:
                    // 子孫を生む
                    Reproduction(world);
                    break;
                case 1:
                    // 成人
                    GrowUp();
                    break;
                case 2:
                    // 生存競争
                    Battle(world);
                    break;
            }
        }

        // 成人
        private void GrowUp()
        {
            child = false;
        }


        // 生殖活動
        // Crossover: 交叉
        // Mutation : 突然変異
        private void Reproduction(World world)
        {
            // 自分が子供の場合は何もしない
            if (Child) return;

            Creature partner = Search(world.Creatures);

            if (partner != null)
            {
                // 交尾して四匹の子を設ける。
                for (int i = 1; i <= 4; i++)
                {
                    Creature child = Mate(partner);
                    // 設けた子は必ず世界に認知させる
                    world.AcceptNewCreature(child);
                }

                // 相手は死亡。
                partner.Die();
            }

            // 自分も死亡。
            Die();
        }

        // 相手を選ぶ。Creatureは、①生存している ②成人のうち ③成績が自分より上かつもっとも成績が自分に近い相手とツガイになる。
        // もしいなければ自分以下かつ成績が自分にもっとも近い相手とツガイになる。
        private Creature Search(List<Creature> creatures)
        {
            // まず自分より上の相手を探す
            int partnerIndex;
            partnerIndex = creatures.FindLastIndex(creature => this.Id != creature.Id
                                                                && creature.Child == false
                                                                && creature.Alive == true
                                                                && this.PrecisionRatio < creature.PrecisionRatio);

            // いなければ自分以下の相手を探す
            if (partnerIndex < 0)
            {
                partnerIndex = creatures.FindIndex(creature => this.Id != creature.Id
                                                                && creature.Child == false
                                                                && creature.Alive == true
                                                                && this.PrecisionRatio >= creature.PrecisionRatio);
            }

            // それでもいなければ諦める
            if (partnerIndex < 0)
            {
                return null;
            }

            return creatures[partnerIndex];
        }


        // 交尾
        // ビットフラグについてはこちら参照。http://qiita.com/satoshinew/items/566bf91707b5371b62b6
        private Creature Mate(Creature wife)
        {
            // Crossover: 交叉
            Gene childGene = gene.Crossover(wife.gene);

            // Mutation : 突然変異
            childGene.TryMutation();

            return new Creature(childGene); 
        }

        // 生存競争
        private void Battle(World world)
        {
            List<Creature> alives = world.Creatures.FindAll(creature => this.Id != creature.Id
                                                                        && creature.Child == false
                                                                        && creature.Alive == true);

            // 32体より多く生き残っている場合は生存競争開始。
            if (alives.Count >= 32)
            {


                int seed = Environment.TickCount;

                // 自分以外の生き残っている誰かをランダムで選ぶ
                int creatureIndex = Rand.Get(0, alives.Count - 1);

                Creature rival = alives[creatureIndex];

                // (合致率 ^ 2 * rnd.Next(10000))を相手と比較し、互角以上の数字を出せたら勝利。
                //                if (Math.Pow((double) PrecisionRatio, 2) * Rand.Get(1, 10000) >= Math.Pow((double) rival.PrecisionRatio, 2) * Rand.Get(1, 10000))
                if (PrecisionRatio >= rival.PrecisionRatio)
                {
                    // 勝ったなら相手が死ぬ。
                    rival.Die();
                }
                else
                {
                    // 負けたなら自分が死ぬ。
                    Die();
                }
            }
        }

        // 死
        public void Die()
        {
            alive = false;
        }
    }

}
