using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildfordBoroughCouncil.Linq
{
    /// <summary>
    /// Source: http://weblogs.asp.net/Justin_Rogers/archive/2004/06/09/151675.aspx
    /// </summary>
    public class NumberToWords
    {
        private static string[] onesMapping = new string[] {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

        private static string[] tensMapping = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        private static string[] groupMapping = new string[] { "Hundred", "Thousand", "Million", "Billion", "Trillion" };

        public static string Convert(long number)
        {
            if (number == 0)
            {
                return onesMapping[number];
            }

            number = Math.Abs(number);

            string retVal = null;
            int group = 0;
            while (number > 0)
            {
                int numberToProcess = (int)(number % 1000);
                number = number / 1000;

                string groupDescription = ProcessGroup(numberToProcess);
                if (groupDescription != null)
                {
                    if (group > 0)
                    {
                        retVal = groupMapping[group] + " " + retVal;
                    }
                    retVal = groupDescription + " " + retVal;
                }

                group++;
            }

            return retVal;
        }

        private static string ProcessGroup(int number)
        {
            int tens = number % 100;
            int hundreds = number / 100;

            string retVal = null;
            if (hundreds > 0)
            {
                retVal = onesMapping[hundreds] + " " + groupMapping[0];
            }
            if (tens > 0)
            {
                if (tens < 20)
                {
                    retVal += ((retVal != null) ? " " : "") + onesMapping[tens];
                }
                else
                {
                    int ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    retVal += ((retVal != null) ? " " : "") + tensMapping[tens];

                    if (ones > 0)
                    {
                        retVal += ((retVal != null) ? " " : "") + onesMapping[ones];
                    }
                }
            }

            return retVal;
        }
    }
}
