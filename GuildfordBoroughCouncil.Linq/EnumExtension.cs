#region Copyright Ben Cheetham 2011
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in 
// any form or by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner. 
// 
// Filename: EnumExtension.cs
// 
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GuildfordBoroughCouncil.Linq
{
    public static partial class EnumHelper
    {
        public static string ToString<TEnum>(this TEnum e)
        {
            return e.ToString().Replace("___", " - ").Replace("__", "-").Replace("_", " ");
        }
    }
}