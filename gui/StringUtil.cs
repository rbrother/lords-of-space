using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace net.brotherus.game
{
    public static class StringUtil
    {
        private static readonly string[] chars = new string[] { 
            "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", 
            "L", "M", "N", "P", "R", "S", "T", "U", "V", "X", "Y", "Z" 
        };

        /// <summary>
        /// Returns letter A,B,C,D,... based on index
        /// </summary>
        /// <param name="index">0,1,2,3,...</param>
        /// <returns>The letter</returns>
        public static string Letter(int index)
        {
            return chars[index]; 
        }
    }
}
