namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static System.Console;
    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader();
            reader.ImportAndPrintData("data.csv");
            reader.ClearData();
            WriteLine("cleared data");
            reader.AssignNumberOfChildren();
            WriteLine("Assigned");
            reader.PrintAllDatabasesTable();
            WriteLine("Printed");
            ReadKey();
        }
    }
}
