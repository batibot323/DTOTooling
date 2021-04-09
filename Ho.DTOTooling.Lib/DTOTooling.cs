using Ho.FileHelper.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            FileInfo[] files = d.GetFiles("*.cs"); 
            foreach (FileInfo file in files)
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

        public void ListIntersectionsBetweenPairs(string subFolderName = "")
        {
            DirectoryInfo d = new DirectoryInfo(folderToParse);
            FileInfo[] files = d.GetFiles("*.cs");
            for (int i = 0; i < files.Length - 1; i++)
            {
                for (int j = i + 1; j < files.Length; j++)
                {
                    regex = new Regex(patternToFindProperties);
                    var firstMatches = regex.Matches(File.ReadAllText(files[i].FullName));
                    var secondMatches = regex.Matches(File.ReadAllText(files[j].FullName));
                    var commonProperties = GetCommonProperties(firstMatches, secondMatches);
                    if (commonProperties.Count > 0)
                    {
                        string fileName = $"{Path.GetFileNameWithoutExtension(files[i].FullName)}-{Path.GetFileNameWithoutExtension(files[j].FullName)}.txt";
                        Ho.FileHelper.Lib.FileHelper.SetOutputToFile($"results\\{subFolderName}", fileName, FileCreation.OverwriteFile);
                        foreach (var commonProperty in commonProperties)
                        {
                            Console.WriteLine(commonProperty);
                        }

                        Ho.FileHelper.Lib.FileHelper.CloseOutputStream();
                    }
                }
            }
        }

        private List<string> GetCommonProperties(params MatchCollection[] listsOfMatches)
        {
            var commonMatches = listsOfMatches[0].Cast<Match>()
                .Select(m => m.Value)
                .ToList();
            foreach (MatchCollection matches in listsOfMatches)
            {
                var matchesArray = matches.Cast<Match>()
                    .Select(m => m.Value)
                    .ToList();
                commonMatches = commonMatches.Intersect(matchesArray).ToList();
            }
            return commonMatches;
        }
    }
}
