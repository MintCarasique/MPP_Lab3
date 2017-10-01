using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            const int AmountOfTasks = 10;
            ManualResetEvent[] doneEvents = new ManualResetEvent[AmountOfTasks];
            Console.WriteLine("Enter source folder path:");
            string SourcePath = Console.ReadLine();
            Console.WriteLine("Enter destination folder path:");
            string DestPath = Console.ReadLine();
            Copier FileCopier = new Copier();
            
        }
    }
}
