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
    
    class Program
    {
        static void Main(string[] args)
        {
          // test1();
           test2();
        }
        static void test1()
        {
            Console.WriteLine("****************************V3DataArray*************************");
            string file1name = "input.txt";
            Console.WriteLine("1.Создать объект V3DataArray.");
            FdblVector2 fdbl = new FdblVector2(sta.init_vector2);
            V3DataArray v3Data = new V3DataArray("f2", DateTime.Now, 2, 3, 0.3, 0.8, fdbl);
            Console.WriteLine("2. Сохранить его в файле.");
            v3Data.SaveAsText(file1name);
            Console.WriteLine("3.Восстановить объект из файла");
            V3DataArray v3 = new V3DataArray("f2", DateTime.Now); 
            v3.LoadText(file1name, ref v3);
            Console.WriteLine("4.Вывести исходный объект:");
            Console.WriteLine(v3Data.ToLongString("f2"));
            Console.WriteLine("5.Вывести сохраненный объект:");
            Console.WriteLine(v3.ToLongString("f2"));
            Console.WriteLine("****************************V3DataArray*************************");
            Console.WriteLine("\n\n\n\n");
            Console.WriteLine("----------------------------V3DataList----------------------------");
            string file2name = "input2.txt";
            Console.WriteLine("1.Создать объект V3DataList.");
            V3DataList list1 = new V3DataList("f2", DateTime.Now);
            list1.AddDefaults(5, fdbl);
            Console.WriteLine("2. Сохранить его в файле.");
            list1.SaveBinary(file2name);
            Console.WriteLine("3.Восстановить объект из файла");
            V3DataList list2 = new V3DataList("f2", DateTime.Now);
            list2.LoadBinary(file2name, ref list2);
            Console.WriteLine("4.Вывести исходный объект:");
            Console.WriteLine(list1.ToLongString("f2"));
            Console.WriteLine("5.Вывести сохраненный объект:");
            Console.WriteLine(list2.ToLongString("f2"));
            Console.WriteLine("----------------------------V3DataList----------------------------");
        }
        static void test2()
        {
            Console.WriteLine("1.Создать объект типа V3MainCollection");
            V3MainCollection collection = new V3MainCollection();
            Console.WriteLine("2.Добавить элементы в коллекцию List<V3Data>"); 
            FdblVector2 fdbl = new FdblVector2(sta.init_vector2);
            V3DataArray v3Data1 = new V3DataArray("f2", DateTime.Now, 2, 3, 0.3, 0.8, fdbl);
            V3DataList list1 = new V3DataList("f2", new DateTime(2021,10,10,15,3,4,5));
            list1.AddDefaults(5, fdbl);
            list1.Add(new Dataltem(8, 8, new Vector2(1, 1)));
            list1.Add(new Dataltem(7, 8, new Vector2(1, 1)));
            V3DataArray v3Data2 = new V3DataArray("f2", DateTime.Now); // в этом массиве число узлов сетки равно 0.
            V3DataList list2 = new V3DataList("f2", new DateTime(2021, 10, 10, 15, 3, 4, 5)); // в этом списке нет элементов.
           
            list2.Add(new Dataltem(8, 8, new Vector2(1, 1)));
            list2.Add(new Dataltem(7, 8, new Vector2(1, 1)));
            collection.Add(v3Data1);
            collection.Add(list1);
            collection.Add(v3Data2);
            collection.Add(list2);
            Console.WriteLine("3.Вывести всю коллекцию");
            Console.WriteLine(collection.ToLongString("f2"));
            Console.WriteLine("4.DataItem с максимальным расстоянием от точки до начала координат в коллекции V3MainCollection:");
            Dataltem data = (Dataltem)collection.Max;
            Console.WriteLine(data.TolongString("f2"));

            Console.WriteLine("5.Все координаты x ,которые появились более один раз:");
            foreach (var x in collection.query_x)
            {
                Console.WriteLine(x+" ");
            }
            Console.WriteLine("------------------------------\n");
            Console.WriteLine("6.Все элементы из V3MainCollection с минимальным значением даты измерения:");
            foreach(var x in collection.query_time)
            {
                Console.WriteLine(x.ToLongString("f2")+"\n");
            }
            
        }
    }
}
