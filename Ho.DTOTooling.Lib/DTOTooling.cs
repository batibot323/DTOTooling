using Ho.FileHelper.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Ho.DTOTooling.Lib
{
    public class DTOTooling
    {
        private const string patternToFindProperties = @"(public|private).*.+?(?=\{)";
        private const string patternToFindMethods = @"(public|private).*.+?(?=\()";
        private string folderToParse;
        private string resultsFolder;
        private Regex regex;

        public DTOTooling(string pathOfFolderToParse, string nameOfResultsFolder)
        {
            this.folderToParse = pathOfFolderToParse;
            this.resultsFolder = nameOfResultsFolder;
        }

        public void ListDownPropertiesAndMethods()
        {
            DirectoryInfo d = new DirectoryInfo(folderToParse);
            FileInfo[] Files = d.GetFiles(); foreach (FileInfo file in Files)
            {
                string fileName = $"{Path.GetFileNameWithoutExtension(file.Name)}.txt";
                Ho.FileHelper.Lib.FileHelper.SetOutputToFile("results", fileName, FileCreation.OverwriteFile);
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

                Ho.FileHelper.Lib.FileHelper.CloseOutputStream();
            }
        }
    }
}
