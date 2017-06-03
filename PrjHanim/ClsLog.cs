using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjHikariwoAnim
{
    public class ClsLog
    {
        /// <summary>
        /// ログを出力する処理
        /// </summary>
        /// <param name="clLine">ログ</param>
        public static void Info(string clLine)
        {
            if (ClsAll.m_Debug)
            {
                Console.WriteLine(clLine);
            }
        }
    }
}
