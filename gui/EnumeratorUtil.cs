using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Net.Brotherus
{
    public static class EnumeratorUtil
    {
        public static List<object> EnumeratorToList(IEnumerator e)
        {
            var l = new List<object>();
            if (e != null)
            {
                while (e.MoveNext())
                {
                    l.Add(e.Current);
                }
            }
            return l;
        }
    }
}
