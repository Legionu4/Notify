using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomWindowNamespace
{
    public static class Arguments
    {
        public static object[] args;

        public static string GetString()
        {
            if (args == null || args.Length <= 0) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var item in args)
            {
                sb.AppendLine(item == null ? "" : item.ToString());
            }

            return sb.ToString();
        }
    }
}
