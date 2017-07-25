using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace _02_Salaries
{
    public static class Extensions
    {
        public static void Print<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            Console.WriteLine();
        }
      

        public static void PrintOnePropertyOfSeveralObjects<T>(this List<T> listOfObjects, int numberOfObjectsToPrint, string propertyToPrint, bool countFromBeginning)
        {
            if (numberOfObjectsToPrint <= 0)
                return;
            int startIteration = countFromBeginning ? 0 : listOfObjects.Count - numberOfObjectsToPrint;
            int endIteration = countFromBeginning ? numberOfObjectsToPrint : listOfObjects.Count;
            for (int i = startIteration; i < endIteration; i++)
            {
                PrintInputPropertyOfObject(listOfObjects[i], propertyToPrint, i);
            }
        }

        public static void PrintInputPropertyOfObject<T>(this T currentObject, string propertyToPrint, int iterator)
        {
            Type thisObject = typeof(T);
            PropertyInfo[] properties = thisObject.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == propertyToPrint)
                {
                    Console.WriteLine("{0}: {1}", iterator, property.GetValue(currentObject));
                }
            }
            Console.WriteLine();
        }
    }
}
