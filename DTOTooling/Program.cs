using System;
using Ho.FileHelper.Lib;
using System.IO;
using System.Text.RegularExpressions;
using Ho.DTOTooling.Lib;

namespace DTOTooling
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderToParse = @"C:\Users\hanih\source\repos\DTOTooling\filesToParse";
            var myDTOTooling = new Ho.DTOTooling.Lib.DTOTooling(folderToParse, "results");
            myDTOTooling.ListDownPropertiesAndMethods();

            return;
        }
    }
}
