using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using Task_2.Properties;

namespace Task_2
{
    internal class DataStructure
    {
        public string SourceText { get; private set; }
        public string Translation { get; private set; }
        public string Key { get; private set; }

        public DataStructure(string sourceText, string translation, string key)
        {
            SourceText = sourceText;
            Translation = translation;
            Key = key;
        }
    }

    internal class Parcer
    {
        public List<DataStructure> Data { get; private set; }

        public Parcer(string inputPath, string outputPath, string filename)
        {
            Data = new List<DataStructure>();

            using (StreamReader fileReader = new StreamReader(inputPath))
            {
                JObject dataJSON = JObject.Parse(fileReader.ReadToEnd());

                foreach (JToken chld in dataJSON["Children"])
                {
                    Data.Add(new DataStructure((string)chld["Source"]["Text"],
                                               (string)chld["Translation"]["Text"],
                                               (string)chld["Key"]));
                }
            }

            new System.Threading.Thread(() => CreateExelStructure()).Start();

            CreateXmlDocument();
            CreateExcelFile(outputPath, filename);
        }

        private void CreateExelStructure()
        {
            Directory.CreateDirectory("ExelFile");
            Directory.CreateDirectory(@"ExelFile\_rels");
            Directory.CreateDirectory(@"ExelFile\docProps");
            Directory.CreateDirectory(@"ExelFile\xl");
            Directory.CreateDirectory(@"ExelFile\xl\_rels");
            Directory.CreateDirectory(@"ExelFile\xl\theme");
            Directory.CreateDirectory(@"ExelFile\xl\worksheets");

            StreamWriter fileCreator = new StreamWriter(@"ExelFile\[Content_Types].xml", false);
            fileCreator.WriteLine(Resources.ResourceManager.GetString("Content_Types"));
            fileCreator.Close();

            fileCreator = new StreamWriter(@"ExelFile\_rels\.rels");
            fileCreator.WriteLine(Resources.ResourceManager.GetString("rels"));
            fileCreator.Close();

            fileCreator = new StreamWriter(@"ExelFile\docProps\app.xml");
            fileCreator.WriteLine(Resources.ResourceManager.GetString("app"));
            fileCreator.Close();

            fileCreator = new StreamWriter(@"ExelFile\docProps\core.xml");
            fileCreator.WriteLine(Resources.ResourceManager.GetString("core"));
            fileCreator.Close();

            fileCreator = new StreamWriter(@"ExelFile\xl\_rels\workbook.xml.rels");
            fileCreator.WriteLine(Resources.ResourceManager.GetString("workbook_rels"));
            fileCreator.Close();

            fileCreator = new StreamWriter(@"ExelFile\xl\theme\theme1.xml");
            fileCreator.WriteLine(Resources.ResourceManager.GetString("theme1"));
            fileCreator.Close();

            fileCreator = new StreamWriter(@"ExelFile\xl\worksheets\sheet1.xml");
            fileCreator.WriteLine(Resources.ResourceManager.GetString("app"));
            fileCreator.Close();

            fileCreator = new StreamWriter(@"ExelFile\xl\styles.xml");
            fileCreator.WriteLine(Resources.ResourceManager.GetString("styles"));
            fileCreator.Close();

            fileCreator = new StreamWriter(@"ExelFile\xl\workbook.xml");
            fileCreator.WriteLine(Resources.ResourceManager.GetString("workbook"));
            fileCreator.Close();

        }

        private void CreateExcelFile(string outputPath, string filename)
        {
            string fileFormat = ".xlsx";
            if (!outputPath.EndsWith(@"\"))
            {
                outputPath += @"\";
            }

            if (File.Exists(outputPath + filename + fileFormat))
            {
                File.Delete(outputPath + filename + fileFormat);
            }
            ZipFile.CreateFromDirectory("ExelFile", outputPath + filename + fileFormat);
            Directory.Delete("ExelFile", true);
        }

        private void CreateXmlDocument()
        {
            XNamespace xmlns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
            XDocument xmlDocument = XDocument.Parse(Resources.ResourceManager.GetString("sheet1"));

            for (int i = 0; i < Data.Count; i++)
            {
                addtoXML(ref xmlDocument, Data[i].SourceText, Data[i].Translation, Data[i].Key, i + 2);
            }
            xmlDocument.Save(@"ExelFile\xl\worksheets\sheet1.xml");
        }

        private void addtoXML(ref XDocument xmlDocument, string cell_1, string cell_2, string cell_3, int rowCounter)
        {
            XNamespace x14ac = "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac";
            XNamespace xmlns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
            xmlDocument.Root
                      .Element(xmlns + "sheetData")
                      .Add(new XElement(xmlns + "row", new XElement(xmlns + "c", new XElement(xmlns + "v", cell_1),
                                                                                 new XAttribute("r", "A" + rowCounter),
                                                                                 new XAttribute("t", "s")),

                                                       new XElement(xmlns + "c", new XElement(xmlns + "v", cell_2),
                                                                                 new XAttribute("r", "B" + rowCounter),
                                                                                 new XAttribute("t", "s")),

                                                       new XElement(xmlns + "c", new XElement(xmlns + "v", cell_3),
                                                                                 new XAttribute("r", "C" + rowCounter),
                                                                                 new XAttribute("t", "s")),

                                                       new XAttribute("r", rowCounter),
                                                       new XAttribute("spans", "1:3"),
                                                       new XAttribute(x14ac + "dyDescent", "0.25")));
        }
    }
}
