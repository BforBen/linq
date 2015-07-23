using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildfordBoroughCouncil.Linq
{
    public static partial class IntExtensions
    {
        public static string FormatBytes(this Int64 bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i = 0;
            double dblSByte = bytes;
            if (bytes > 1024)
            {
                for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                {
                    dblSByte = bytes / 1024.0;
                }
            }
            return String.Format("{0:0.##}{1}", dblSByte, Suffix[i]);
        }

        public static string ToWords(this Int64 number)
        {
            return NumberToWords.Convert(number);
        }

        public static string ToWords(this Int64? number)
        {
            return number.HasValue ? NumberToWords.Convert(number.Value) : String.Empty;
        }

        public static string ToWords(this Int32 number)
        {
            return NumberToWords.Convert(number);
        }

        public static string ToWords(this Int32? number)
        {
            return number.HasValue ? NumberToWords.Convert(number.Value) : String.Empty;
        }
    }
}
