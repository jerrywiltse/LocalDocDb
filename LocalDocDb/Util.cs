using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDocDb
{
    class Util
    {
        public static bool ContainsNullOrEmpty(params object[] Items)
        {
            if (Items == null || Items.Length < 1)
                return true;

            foreach (object item in Items)
            {
                if (item == null)
                    return true;

                if (item is string)
                {
                    if (string.IsNullOrWhiteSpace(item as String))
                        return true;
                }

            }

            return false;
        }
    }
}
