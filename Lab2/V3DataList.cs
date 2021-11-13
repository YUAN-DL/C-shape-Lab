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
    class V3DataList : V3Data, IEnumerable<Dataltem>
    {
        List<Dataltem> list;

        public V3DataList(string str, DateTime time) : base(str, time)
        {
            info = str;
            date_time = time;
            list = new List<Dataltem>();
        }
        public bool Add(Dataltem newltem)
        {
            if (list.Contains(newltem))
                return false;
            else
            {
                list.Add(newltem);
                return true;
            }

        }
        public int AddDefaults(int nltems, FdblVector2 F)
        {
            int add_count = 0;
            for (int i = 0; i < nltems; i++)
            {
                Random ran = new Random();
                double x = ran.Next(10);
                double y = ran.Next(10);
                Dataltem dataltem = new Dataltem(x, y, F(x, y));
                if (Add(dataltem))
                    add_count++;
            }
            return add_count;
        }
        public override int Count
        {
            get
            {
                return list.Count;
            }
        }
        public override double MaxDistance
        {
            get
            {
                double max = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        double temp = Vector2.Distance(list[i].vec, list[j].vec);
                        if (temp > max)
                            max = temp;
                    }
                }
                return max;
            }
        }
        public override string ToString()
        {
            // return list[0].GetType()+" "+list[0].x.GetType().ToString()+
            return "\nThe element's count of list is " + list.Count + " \n";
        }
        public override string ToLongString(string format)
        {
            string str = date_time.ToString() + "\n";
            for (int i = 0; i < list.Count; i++)
            {
                str += "Point" + i + ": x:" + (list[i].x).ToString(format) + "  y:" + (list[i].y).ToString(format) + " ";
                str += "Vector" + i + ": x:" + (list[i].vec.X).ToString(format) + "  y:" + (list[i].vec.Y).ToString(format) + "  ";
                str += "Vector" + i + "'s module:" + (Math.Sqrt(Math.Pow(list[i].vec.X, 2) + Math.Pow(list[i].vec.Y, 2))).ToString(format) + "\n";
            }
            return this.ToString() + str;
        }

        public IEnumerator<Dataltem> GetEnumerator()
        {
            for (int i = 0; i < list.Count; i++)
                yield return list[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public bool SaveBinary(string filename)
        {
            BinaryWriter bw = null;
            try
            {
                bw = new BinaryWriter(new FileStream(filename, FileMode.Create));
                {
                    bw.Write(info);
                    bw.Write(date_time.ToString());
                    for (int i = 0; i < list.Count; i++)
                    {
                        bw.Write(list[i].x+" "+list[i].y);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while saving data\n{ex.Message}");
                return false;
            }
            finally
            {
                if (bw != null)
                    bw.Dispose();
            }
            bw.Close();
            return true;

        }
        public bool LoadBinary(string filename,ref V3DataList v3)
        {
            CultureInfo CI = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            CultureInfo CIen = new CultureInfo("en-US");
            CultureInfo CIru = new CultureInfo("ru-RU");
            CultureInfo CIzh = new CultureInfo("zh-CN");
            BinaryReader br = null;
            try
            {
                br = new BinaryReader(new FileStream(filename, FileMode.Open), Encoding.UTF8);
                {
                    v3.info = br.ReadString();
                    v3.date_time=DateTime.Parse(br.ReadString(), new CultureInfo("en-US", true));
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        string str = br.ReadString();
                        string[] temp = str.Split(' ');
                        v3.Add(new Dataltem(double.Parse(temp[0],CI), double.Parse(temp[1], CI), new Vector2(float.Parse(temp[0], CI), float.Parse(temp[1], CI))));
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
                if (br != null)
                    br.Dispose();
            }
            br.Close();
            return true;
        }
    }
}