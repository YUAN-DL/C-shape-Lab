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
    class V3DataArray : V3Data, IEnumerable<Dataltem>
    {
        public int Count_node_x { get; private set; }
        public int Count_node_y { get; private set; }
        public double Scale_x { get; private set; }
        public double Scale_y { get; private set; }
        public Vector2[,] Array { get; private set; }

        public V3DataArray(string str, DateTime time) : base(str, time)
        {
            info = str;
            date_time = time;
            Array = new Vector2[,] { };
        }
        public V3DataArray(string str, DateTime time, int count_x, int count_y, double shak_x, double shak_y, FdblVector2 F) : base(str, time)
        {
            info = str;
            date_time = time;
            Count_node_x = count_x;
            Count_node_y = count_y;

            Scale_x = shak_x;
            Scale_y = shak_y;
            Array = new Vector2[count_x, count_y];
            for (int i = 0; i < count_x; i++)
            {
                for (int j = 0; j < count_y; j++)
                {
                    Array[i, j] = F(i * shak_x, j * shak_y);
                }
            }
        }
        public override int Count
        {
            get
            {
                return Count_node_x * Count_node_y;
            }
        }
        public override double MaxDistance
        {
            get
            {
                double max = 0;
                max = Math.Sqrt(Math.Pow((Count_node_x - 1) * Scale_x, 2) + Math.Pow((Count_node_y - 1) * Scale_y, 2));
                return max;
            }
        }
        public override string ToString()
        {
            //return Array[0,0].GetType().ToString() + 
            return "\nThe element's count of Array is " + Count.ToString() + " \n";
        }
        public override string ToLongString(string format)
        {
            string str = date_time.ToString() + "\n";
            for (int i = 0; i < Count_node_x; i++)
            {
                for (int j = 0; j < Count_node_y; j++)
                {
                    str += "coordinate:  [" + i + "," + j + "] vector:" + Array[i, j].ToString() + "   ";
                    str += "Vector's module:" + (Math.Sqrt(Math.Pow(Array[i, j].X, 2) + Math.Pow(Array[i, j].Y, 2))).ToString(format) + "\n";
                }
            }
            return this.ToString() + str;
        }

        public IEnumerator<Dataltem> GetEnumerator()
        {
            for (int i = 0; i < Count_node_x; i++)
                for (int j = 0; j < Count_node_y; j++)
                {
                    double x = Scale_x * i;
                    double y = Scale_y * j;
                    yield return new Dataltem(x, y, new Vector2((float)x, (float)y));
                }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public static explicit operator V3DataList(V3DataArray DataArray)
        {

            V3DataList list = new V3DataList(DataArray.info, DataArray.date_time);
            Dataltem temp;
            for (int i = 0; i < DataArray.Count_node_x; i++)
            {
                for (int j = 0; j < DataArray.Count_node_y; j++)
                {
                    temp.x = DataArray.Scale_x * i;
                    temp.y = DataArray.Scale_y * j;
                    temp.vec = DataArray.Array[i, j];
                    list.Add(temp);
                }
            }
            return list;
        }
        public bool SaveAsText(string filename)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(filename);
                {
                    sw.WriteLine(info);
                    sw.WriteLine(date_time);
                    sw.WriteLine(Count_node_x + " " + Count_node_y);
                    sw.WriteLine(Scale_x + " " + Scale_y);
                    foreach (var item in Array)
                        sw.WriteLine(item.X + " " + item.Y);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while saving data\n{ex.Message}");
                return false;
            }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
            return true;
        }
        public bool LoadText(string filename, ref V3DataArray v3)
        {
            CultureInfo CI = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            CultureInfo CIen = new CultureInfo("en-US");
            CultureInfo CIru = new CultureInfo("ru-RU");
            CultureInfo CIzh = new CultureInfo("zh-CN");
            if ((CI == CIru) || (CI == CIzh) || (CI == CIen))
                if (CI == CIru)
                    CI = CIru;
                else if (CI == CIzh)
                    CI = CIzh;
                else
                    CI = CIen;
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filename);
                {
                    string str1 = sr.ReadLine();
                    v3.info = str1;
                    string str2 = sr.ReadLine();
                    DateTime date = DateTime.Parse(str2, new CultureInfo("en-US", true));
                    v3.date_time = date;
                    string phrase_nodes = sr.ReadLine();
                    string[] str3 = phrase_nodes.Split(' ');
                    v3.Count_node_x = int.Parse(str3[0],CI);
                    v3.Count_node_y = int.Parse(str3[1],CI);
                    string phrase_scale = sr.ReadLine();
                    str3 = phrase_scale.Split(' ');
                    v3.Scale_x = double.Parse(str3[0],CI);
                    v3.Scale_y = double.Parse(str3[1],CI);
                    v3.Array = new Vector2[v3.Count_node_x, v3.Count_node_y];
                    for (int i = 0; i < v3.Count_node_x; i++)
                    {
                        for (int j = 0; j < v3.Count_node_y; j++)
                        {
                            str3 = sr.ReadLine().Split(' ');
                            v3.Array[i, j] = new Vector2(float.Parse(str3[0],CI), float.Parse(str3[1], CI));
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while reading data\n{ex.Message}");
                return false;
            }
            finally
            {
                if (sr != null)
                    sr.Dispose();
            }
            return true;
        }

    }
}
