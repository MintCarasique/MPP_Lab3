using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter source folder path:");
            string sourcePath = Console.ReadLine();
            Console.WriteLine("Enter destination folder path:");
            string destPath = Console.ReadLine();
            Copier fileCopier = new Copier(sourcePath, destPath);
            fileCopier.StartCopying();
            Console.ReadKey();
        }
    }
}
