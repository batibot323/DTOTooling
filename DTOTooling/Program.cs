using System;
using Ho.FileHelper.Lib;
using System.IO;
using System.Text.RegularExpressions;

namespace DTOTooling
{
    class Program
    {
        static void Main(string[] args)
        {
            string patternToFindProperties = @"(public|private).*.+?(?=\{)";
            string patternToFindMethods = @"(public|private).*.+?(?=\()";
            Regex regex;
            string folderToParse = @"C:\Users\hanih\source\repos\DTOTooling\filesToParse";
            DirectoryInfo d = new DirectoryInfo(folderToParse);
            FileInfo[] Files = d.GetFiles("*.cs"); //Getting Text files
            foreach (FileInfo file in Files)
            {
                string fileName = $"{Path.GetFileNameWithoutExtension(file.Name)}.txt";
                FileHelper.SetOutputToFile("results", fileName, FileCreation.OverwriteFile);
                string text = File.ReadAllText(file.FullName);
                regex = new Regex(patternToFindProperties);
                var matches = regex.Matches(text);
                Console.WriteLine(file.Name);
                Console.WriteLine("\nProperties:");
                foreach (Match match in matches)
                {
                    Console.WriteLine($"\t{match.Value}");
                }

                regex = new Regex(patternToFindMethods);
                matches = regex.Matches(text);
                Console.WriteLine("\nMethods:");
                foreach (Match match in matches)
                {
                    Console.WriteLine($"\t{match.Value}");
                }

                FileHelper.CloseOutputStream();
            }

            return;
        }
    }
}
