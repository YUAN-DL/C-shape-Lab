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
    class V3MainCollection : IEnumerable<Dataltem>
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
                str += "-------------------Collection " + i.ToString()+":-------------------";
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

        public IEnumerator<Dataltem> GetEnumerator()
        {
            return ((IEnumerable<Dataltem>)v3s).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)v3s).GetEnumerator();
        }

        public Dataltem? Max
        {
            get
            {
                if (v3s.Count == 0)
                    return null;
                IEnumerable<Dataltem> items = from elem in v3s
                                             from item in elem
                                             select item;
                return items.OrderByDescending(x => x.vec.Length()).First();
            }

        }
        public IEnumerable<double> query_x
        {
            get
            {
                if (v3s.Count == 0)
                    return null;
               
                IEnumerable<Dataltem> items = from elem in  v3s
                                              from item in elem
                                              select item;
                IEnumerable<double> query = from i in items
                                            select i.x;
                var group = query.GroupBy(x=>x);
                IEnumerable<double> query2 = from i in @group
                                             where i.Count() > 1
                                             select i.Key;
                return query2;
            }
        }
        public IEnumerable<V3Data> query_time
        {
            get
            {
                if (v3s.Count == 0)
                    return null;
                IEnumerable<V3Data> v3Datas = from elem in v3s
                                              select elem;
                v3Datas=v3Datas.OrderBy(x=>x.date_time);
                IEnumerable<V3Data> query = from i in v3Datas
                                            where (i.date_time == v3Datas.First().date_time)&& (i.Count>0)
                                            select i;
                return query;

            }
        }

    }
}
