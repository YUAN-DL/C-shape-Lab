using System;
using System.Numerics;
using System.Collections.Generic;

namespace Lab1
{
    struct Dataltem
    {
        public double x, y; 
        public Vector2 vec;
        public Dataltem(double arc_x, double arc_y, Vector2 arc_vec)
        {
            x = arc_x;
            y = arc_y;
            vec = arc_vec;
        }
        public string TolongString(string format)
        {
            return x.ToString(format) + " " + y.ToString(format) + "\n" + vec.ToString();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
   
    
   public delegate Vector2 FdblVector2(double x, double y);

   public abstract class V3Data
   {
        public string info;
        public abstract int Count  { get; }
        public abstract double MaxDistance { get; }
        
        public DateTime date_time;
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
    class V3DataList : V3Data
    {
        List<Dataltem> list;

        public V3DataList(string str,DateTime time):base(str,time)
        {
            info = str;
            date_time =time;
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
        public int AddDefaults(int nltems,FdblVector2 F)
        {
            int add_count = 0;
            for(int i=0;i<nltems;i++)
            {
                Random ran = new Random();
                double x = ran.Next(10);
                double y = ran.Next(10);
                Dataltem dataltem = new Dataltem(x,y,F(x,y));
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
                for(int i=0;i<list.Count;i++)
                {
                    for (int j = i+1; j < list.Count; j++)
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
              return "\nThe count of list is " +list.Count+" \n";
        }
        public override string ToLongString(string format)
        {
            string str=date_time.ToString()+"\n";
            for(int i=0;i<list.Count;i++)
            {
                str += "Point "+i+" x:"+(list[i].x).ToString(format) + "  y:" + (list[i].y).ToString(format)+"\n";
                str += "Vector " + i + " x:" + (list[i].vec.X).ToString(format) + "  y:" + (list[i].vec.Y).ToString(format) +"  ";
                str += "Vector"+i+"'s module:" + (Math.Sqrt(Math.Pow(list[i].vec.X,2) + Math.Pow(list[i].vec.Y ,2))).ToString(format)+"\n";
            }
            return this.ToString()+str;
        }
    }

    class V3DataArray : V3Data
    {

        public int Count_node_x {get;} 
        public int Count_node_y {get;}
        public double Scale_x { get;}
        public double Scale_y { get;}
        public Vector2 [,] Array { get; }

        public V3DataArray(string str,DateTime time ):base(str,time)
        {
            info = str;
            date_time = time;
            Array = new Vector2[,]{ };
        }
        public V3DataArray(string str,DateTime time, int count_x, int count_y,double shak_x,double shak_y,FdblVector2 F):base (str,time)
        {
            info = str;
            date_time = time;
            Count_node_x = count_x;
            Count_node_y = count_y;
        
            Scale_x = shak_x;
            Scale_y = shak_y;
            Array = new Vector2[count_x, count_y];
            for(int i=0;i<count_x;i++)
            {
                for(int j=0;j<count_y;j++)
                {
                    Array[i,j] = F(i*shak_x,j*shak_y);
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

                /*for (int l = 0; l < Count_node_x; l++)
                 {
                     for (int k = 0; k < Count_node_y; k++)
                     {

                         for ( int i=0; i < Count_node_x; i++)
                         {

                             for (int j=0; j < Count_node_y; j++)
                             {
                                 if ((i == l && j > k) || (i > l))
                                 {
                                     double temp = Vector2.Distance(Array[l, k], Array[i, j]);
                                     if (temp > max)
                                         max = temp;
                                 }
                             }
                         }
                     }
                 } */
                max = Math.Sqrt(Math.Pow((Count_node_x-1) * Scale_x, 2) + Math.Pow((Count_node_y-1) * Scale_y, 2));
                return max;
            }
        }
        public override string ToString()
        {
            //return Array[0,0].GetType().ToString() + 
                return "\nThe count of Array is " + Count.ToString() + " \n";
        }
        public override string ToLongString(string format)
        {
            string str = date_time.ToString() + "\n";
            for (int i = 0; i < Count_node_x; i++)
            {
                for (int j = 0; j < Count_node_y; j++)
                {
                    str += "coordinate:  [" + i + ","+j+ "] vector:" + Array[i,j].ToString() +"   ";
                    str += "The module of this vector:" + (Math.Sqrt(Math.Pow(Array[i,j].X, 2) + Math.Pow(Array[i,j].Y, 2))).ToString(format) + "\n";
                }
            }
            return this.ToString() + str;
        }
        public static explicit operator V3DataList(V3DataArray DataArray)
        {   
         
            V3DataList list = new V3DataList(DataArray.info,DataArray.date_time);
            Dataltem temp;
            for(int i=0;i<DataArray.Count_node_x;i++)
            {
                for(int j=0;j<DataArray.Count_node_y;j++)
                {
                    temp.x = DataArray.Scale_x * i;
                    temp.y = DataArray.Scale_y * j;
                    temp.vec = DataArray.Array[i, j];
                    list.Add(temp);
                }
            }
            return list;
        }
    }
    class V3MainCollection
    {
        private List<V3Data> v3s=new List<V3Data>();
        public int Count
        {
            get
            {
                return v3s.Count;
            }
        }
        public V3Data this [int index]
        {
            get
            {
                return v3s[index];
            }
        }

        public bool Contains(string ID)
        {
            for(int i=0;i<Count;i++)
            {
                if(v3s[i].info==ID)
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
            string str="";
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
    }
     static class sta
    {
        public static Vector2 init_vector2(double x,double y)
        {
            return new Vector2((float)x,(float)y);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            /*
             * Vector2 vec1 = new Vector2(5.0f,4.0f);
            Vector2 vec2 = new Vector2(0.0f, 1.0f);
            int a=3;
            string str = " a b \n a";
            System.Console.WriteLine(Vector2.Distance(vec1,vec2));
            Dataltem first_data = new Dataltem(1.4, 2.5,vec1);
            System.Console.WriteLine(a.GetType());
            System.Console.WriteLine(str);
            System.Console.WriteLine(Math.Sqrt(Math.Pow(vec1.X,2) + Math.Pow(vec1.Y ,2)));
            System.Console.WriteLine(first_data.TolongString("f2"));
            FdblVector2 fdbl = new FdblVector2(sta.init_vector2);
            V3DataList list1 = new V3DataList("f2",DateTime.Now);
            list1.AddDefaults(4, fdbl);
            System.Console.WriteLine(list1.ToLongString("f2"));
            System.Console.WriteLine(list1.MaxDistance);
            */
            FdblVector2 fdbl = new FdblVector2(sta.init_vector2);
            V3DataArray v3Data = new V3DataArray("f2", DateTime.Now,5,5,0.3f,0.8f,fdbl);
            System.Console.WriteLine(v3Data.ToLongString("f2"));
            V3DataList dataList = (V3DataList)v3Data;
            System.Console.WriteLine("The count of element in array =" + v3Data.Count);
            System.Console.WriteLine("The count of element in list =" + dataList.Count);

            
            System.Console.WriteLine("The MaxDistance in array ="+ v3Data.MaxDistance);
            System.Console.WriteLine("The MaxDistance in list =" + dataList.MaxDistance);

            V3MainCollection collection = new V3MainCollection();
            collection.Add(v3Data);
            collection.Add(dataList);
            System.Console.WriteLine(collection.ToLongString("f2"));

            for(int i=0;i<collection.Count;i++)
            {
                System.Console.WriteLine("The count of element =" + collection[i].Count);

                System.Console.WriteLine("The MaxDistance  =" + collection[i].MaxDistance);
            }
        }
    }
}
