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
    public class RandTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // よほどの不運に恵まれなければ発生しないが、実は偶然二回続けて同じ値が出ると失敗する。発生率の低さを見てもいいが結局本質的に一緒。
            for (int i=0;i<=5;i++)
                Assert.AreNotEqual(Rand.Get(0, 0x7FFFFFFE), Rand.Get(0, 0x7FFFFFFE));
        }
    }
}