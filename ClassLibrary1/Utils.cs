using System;

namespace GeneticAlgorithm1
{
    public static class Utils
    {

        /// <summary>
        /// intについてビットが立っている数を取得します。
        /// 参考: http://zecl.hatenablog.com/entry/20100228/p1
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static int BitCount(UInt32 self)
        {
            var x = self - ((self >> 1) & 0x55555555);
            x = ((x >> 2) & 0x33333333) + (x & 0x33333333);
            x = (x >> 4) + x & 0x0f0f0f0f;
            x += x >> 8;
            return (int) ((x >> 16) + x & 0xff);
        }


        // BIT演算による一部交換
        public static UInt32 ExchangeBit(UInt32 prefix, UInt32 suffix, int changePoint)
        {
            if (changePoint < 1 || 31 < changePoint)
            {
                throw new Exception("データ範囲ミス. changePoint: " + changePoint.ToString());
            }

            prefix >>= changePoint; // 後ろを削る
            prefix <<= changePoint; // そして位置を戻す
            suffix <<= (32 - changePoint); // こちらは前を削る
            suffix >>= (32 - changePoint); // そして位置を戻す

            return prefix | suffix;
        }


        // BIT演算による一点切り替え
        public static UInt32 SwitchBit(UInt32 value, int changePoint)
        {
            if(changePoint < 0 || 31 < changePoint)
            {
                throw new Exception("データ範囲ミス. changePoint: " + changePoint.ToString());
            }

            UInt32 flag = 1u << changePoint;

            if ((value & flag) == flag)
            {
                // フラグが立ってるので落とす
                value &= ~flag;
            }
            else
            {
                // フラグが落ちてるので立てる
                value |= flag;
            }

            return value;
        }
    }
}
