using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace LDrawPartLib
{
    public static class LDrawHelper
    {
/*
        private static Regex _wordsEx = new Regex(
                    @"\S+",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
*/

        public static string LDrawLocation {
            get {
                return ConfigurationManager.AppSettings["LDRAW"];
            }
        }
        public static string PartsFileExtension {
            get { return ".DAT"; }
        }
        public static string LDrawConfigLocation {
            get {
                return LDrawLocation + "ldconfig.ldr";
            }
        }
        public static string LDrawPartsListLocation {
            get {
                return LDrawLocation + "parts.lst";
            }
        }
        public static string CategoryListLocation {
            get {
                return "PartCategoryList.txt";
            }
        }
        public static string LDrawPartLibLocation {
            get {
                return LDrawLocation + "Parts\\";
            }
        }

        public static string LDrawSubPartLibLocation {
            get {
                return LDrawLocation + "P\\";
            }
        }

        public static List<Category> ReadAllCategories() {
            string[] lines = File.ReadAllLines(CategoryListLocation);

            return lines.Select(line => line.Split('|'))
                                        .Select(values => new Category(values[0], values[1]))
                                        .ToList();
        }

        //public static List<PartHeader> ReadAllParts() {
        //    List<PartHeader> partList = new List<PartHeader>();

        //    string [] parts =  Directory.GetFiles(LDrawPartLibLocation, "*.dat", SearchOption.TopDirectoryOnly);

        //    foreach (string file in parts) {

        //        string[] fileName = file.Split('\\');

        //        string fileInfo = ReadFile(fileName[fileName.Count() - 1]);

        //        partList.Add(ParseFile(fileInfo));
        //    }

        //    return partList;
        //}

        public static List<Part> ReadPartList() {
            List<Part> partList = new List<Part>(2700);
            //string filePath =  LDrawLocation + "parts.lst"; // "C:\\LDraw\\parts.lst"

            string[] partlines = File.ReadAllLines(LDrawPartsListLocation);

            foreach (string line in partlines) {
                int index = line.ToUpper().IndexOf(PartsFileExtension);
                if (index > 0) {
                    string partNumber = line.Substring(0, index);
                    string description = line.Substring(index + 4);

                    partList.Add(new Part(partNumber, description));
                }
            }

            return partList;
        }

        //private static bool IsValidLine0(ref string line) {
        //    bool result = false;
        //    line = line.Trim();
        //    if (line.Length > 0) {
        //        if (line[0] == '0') {
        //            // remove '0 ' from the line.
        //            line = line.Substring(1).Trim();

        //            if (line.Length > 0) {
        //                result = true;
        //                // remove '0 ' from the line.
        //                line = line.Trim();
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public static PartHeader ParseFile(string info) {
        //    PartHeader part = new PartHeader();
        //    int lineNo = 0;

        //    foreach (Match m in sentenceEx.Matches(info)) {
        //        string line = m.Value.Trim();
        //        if (IsValidLine0(ref line)) {

        //            switch (lineNo) {
        //                case 0:
        //                    part.PartDescription = line;
        //                    break;
        //                case 1:
        //                    part.FileName = line.Replace("Name:", "");
        //                    part.FileName = part.FileName.Replace("name", "");
        //                    part.PartNumber = part.FileName.Replace(".dat", "").Trim();
        //                    break;
        //                case 2:
        //                    part.Author = line.Replace("Author:", "");
        //                    break;
        //                default:
        //                    MatchCollection cmds = wordsEx.Matches(line);
        //                    string cmd = cmds[0].Value.Trim();

        //                    switch (cmd) {
        //                        case "!CATEGORY":
        //                        case "CATEGORY":
        //                            part.Category = line.Replace(cmd, "").Trim();
        //                            break;
        //                        case "!KEYWORDS":
        //                        case "KEYWORDS":
        //                            part.Keyword += line.Replace(cmd, "");
        //                            break;
        //                    }
        //                    break;
        //            }
        //        }
        //        else
        //            break;
        //        lineNo += 1;
        //    }

        //    // If category not found then the first word in the part desc is the category.
        //    if (part.Category == null) {
        //        MatchCollection words = wordsEx.Matches(part.PartDescription);
        //        part.Category = words[0].Value;
        //    }
        //    return part;
        //}

        public static string[] ReadFile(string fileName) {
            string filePath = LDrawPartLibLocation + fileName;

            if (!File.Exists(filePath))
                filePath = LDrawSubPartLibLocation + fileName;

            return File.ReadAllLines(filePath);
        }
    }
}
