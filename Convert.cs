using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildfordBoroughCouncil
{
    /// <summary>
    /// A Base36 De- and Encoder
    /// </summary>
    /// <remarks>
    /// http://www.stum.de/2008/10/20/base36-encoderdecoder-in-c/
    /// </remarks>
    public partial class Convert
    {
        private const string Base36CharList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //private static char[] Base36CharArray = Base36CharList.ToCharArray();

        /// <summary>
        /// Encode the given number into a Base36 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToBase36(long input)
        {
            if (input < 0) throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            char[] clistarr = Base36CharList.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[input % 36]);
                input /= 36;
            }
            return new string(result.ToArray());
        }

        /// <summary>
        /// Decode the Base36 Encoded string into a number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Int64 FromBase36(string input)
        {
            var reversed = input.ToUpper().Reverse();
            long result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += Base36CharList.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }
    }
}