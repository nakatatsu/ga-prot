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
    public class UtilsTests
    {
        [TestMethod()]
        public void BitCountTest()
        {
            Assert.AreEqual(Utils.BitCount(0x0u), 0);
            Assert.AreEqual(Utils.BitCount(0xffffffffu), 32);
            Assert.AreEqual(Utils.BitCount(Convert.ToUInt32("00000000000000000000000000000000", 2)), 0);
            Assert.AreEqual(Utils.BitCount(Convert.ToUInt32("11111111000000000000000000000000", 2)), 8);
            Assert.AreEqual(Utils.BitCount(Convert.ToUInt32("11111111111111110000000000000000", 2)), 16);
            Assert.AreEqual(Utils.BitCount(Convert.ToUInt32("11111111111111111111111100000000", 2)), 24);
            Assert.AreEqual(Utils.BitCount(Convert.ToUInt32("00000000000000000000000011111111", 2)), 8);
            Assert.AreEqual(Utils.BitCount(Convert.ToUInt32("00000000000000001111111111111111", 2)), 16);
            Assert.AreEqual(Utils.BitCount(Convert.ToUInt32("00000000111111111111111111111111", 2)), 24);
            Assert.AreEqual(Utils.BitCount(Convert.ToUInt32("11111111111111111111111111111111", 2)), 32);
            
            string str = "1";
            for (int i=0;i<=31;i++)
            {
                Assert.AreEqual(Utils.BitCount(Convert.ToUInt32(str, 2)), 1 + i);
                str = str + "1";
            }
        }


        [TestMethod()]
        public void ExchangeBitTest()
        {
            // ChangePointの値が大きいほどprefixが削れる。
            UInt32 result;
            result = Utils.ExchangeBit(Convert.ToUInt32("00000000000000000000000000000000", 2), Convert.ToUInt32("11111111111111111111111111111111", 2), 16);
            Assert.AreEqual(result, Convert.ToUInt32("00000000000000001111111111111111", 2));

            result = Utils.ExchangeBit(Convert.ToUInt32("11111111111111111111111111111111", 2), Convert.ToUInt32("00000000000000000000000000000000", 2), 16);
            Assert.AreEqual(result, Convert.ToUInt32("11111111111111110000000000000000", 2));

            result = Utils.ExchangeBit(Convert.ToUInt32("00000000000000000000000000000000", 2), Convert.ToUInt32("11111111111111111111111111111111", 2), 1);
            Assert.AreEqual(result, Convert.ToUInt32("00000000000000000000000000000001", 2));

            result = Utils.ExchangeBit(Convert.ToUInt32("00000000000000000000000000000000", 2), Convert.ToUInt32("11111111111111111111111111111111", 2), 31);
            Assert.AreEqual(result, Convert.ToUInt32("01111111111111111111111111111111", 2));
        }

        [TestMethod()]
        public void SwitchBitTest()
        {
            UInt32 result;
            result = Utils.SwitchBit(Convert.ToUInt32("00000000000000000000000000000000", 2), 0);
            Assert.AreEqual(result,  Convert.ToUInt32("00000000000000000000000000000001", 2));

            result = Utils.SwitchBit(Convert.ToUInt32("00000000000000000000000000000000", 2), 31);
            Assert.AreEqual(result,  Convert.ToUInt32("10000000000000000000000000000000", 2));

            result = Utils.SwitchBit(Convert.ToUInt32("00000000000000000000000000000000", 2), 16);
            Assert.AreEqual(result,  Convert.ToUInt32("00000000000000010000000000000000", 2));

            result = Utils.SwitchBit(Convert.ToUInt32("11111111111111111111111111111111", 2), 0);
            Assert.AreEqual(result,  Convert.ToUInt32("11111111111111111111111111111110", 2));

            result = Utils.SwitchBit(Convert.ToUInt32("11111111111111111111111111111111", 2), 31);
            Assert.AreEqual(result,  Convert.ToUInt32("01111111111111111111111111111111", 2));

            result = Utils.SwitchBit(Convert.ToUInt32("11111111111111111111111111111111", 2), 16);
            Assert.AreEqual(result,  Convert.ToUInt32("11111111111111101111111111111111", 2));

        }
    }
}