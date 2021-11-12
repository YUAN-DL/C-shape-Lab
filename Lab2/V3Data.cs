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
    public abstract class V3Data
    {
        public string info { get; protected set; }
        public abstract int Count { get; }
        public abstract double MaxDistance { get; }

        public DateTime date_time { get; protected set; }
        public V3Data(string str, DateTime time)
        {
            //  info = str;
            // date_time = time;
        }
        public abstract string ToLongString(string format);
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
