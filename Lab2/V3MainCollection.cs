using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.IO;
using System.Globalization;
namespace Lab2
{
    class V3MainCollection
    {
        private List<V3Data> v3s = new List<V3Data>();
        public int Count
        {
            get
            {
                return v3s.Count;
            }
        }
        public V3Data this[int index]
        {
            get
            {
                return v3s[index];
            }
        }

        public bool Contains(string ID)
        {
            for (int i = 0; i < Count; i++)
            {
                if (v3s[i].info == ID)
                    return true;
            }
            return false;
        }
        public bool Add(V3Data newV3Data)
        {
            if (v3s.Contains(newV3Data))
                return false;
            else
            {
                v3s.Add(newV3Data);
                return true;
            }
        }
        public string ToLongString(string format)
        {
            string str = "";
            for (int i = 0; i < Count; i++)
            {
                str += v3s[i].ToLongString(format);
            }
            return str;
        }
        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Count; i++)
            {
                str += v3s[i].ToString();
            }
            return str;
        }

        public IEnumerable<double> squares = Enumerable.Range(1, 100).Select(x => Math.Pow(x, 2));
    }
}
