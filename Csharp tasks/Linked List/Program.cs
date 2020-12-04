using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linked_List
{
    class Program
    {
        static void Main(string[] args)
        {
            List testList = new List();

            testList.AddNode("a");
            testList.AddNode("b");
            testList.AddNode("c");
            testList.ShowList();
            Console.WriteLine("__after adding_________");
            testList.FindByIndex(1);
            testList.AddByIndex(4, "d");
            testList.AddByIndex(11, "e");
            testList.AddByIndex(8, "f");
            Console.WriteLine("__after adding by index");
            testList.ShowList();
            testList.AddByIndex(0, "0");
            Console.WriteLine("__after replace________");
            testList.ShowList();
            testList.DeleteByIndex(4);
            Console.WriteLine("after delete___________");
            testList.ShowList();
            testList.ShowList();
            Console.ReadLine();
        }
    }
}
