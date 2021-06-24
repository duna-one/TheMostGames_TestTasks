using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using Task_2.Properties;

namespace Task_2
{
    /// <summary>
    /// A class that describes the structure of the data used
    /// </summary>
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

            // Filling in the list with data from the input file
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

            //We give the creation of the internal structure of the EXEL file to a separate thread, so as not to waste time on this
            new System.Threading.Thread(() => CreateExelStructure()).Start();

            //Creating an XML document that is the markup of an excel table
            CreateXmlDocument();

            //Creating an Exel file
            CreateExcelFile(outputPath, filename);
        }

        /// <summary>
        /// Сreates the internal structure of the excel file
        /// </summary>
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

        /// <summary>
        /// Creates an excel file (zip archive with xlsx format) from a pre-prepared structure
        /// </summary>
        /// <param name="outputPath">Directory for saving the output file</param>
        /// <param name="filename">Output file name</param>
        private void CreateExcelFile(string outputPath, string filename)
        {
            string fileFormat = ".xlsx";
            if (!outputPath.EndsWith(@"\"))
            {
                outputPath += @"\";
            }

            // If a file with the specified name exists on the specified path, then delete it
            if (File.Exists(outputPath + filename + fileFormat))
            {
                File.Delete(outputPath + filename + fileFormat);
            }

            // Creating an xslx archive
            ZipFile.CreateFromDirectory("ExelFile", outputPath + filename + fileFormat);

            // Deleting the folder with the internal structure of the file
            Directory.Delete("ExelFile", true);
        }


        /// <summary>
        /// Creating an XML document that is the markup of an excel table
        /// </summary>
        private void CreateXmlDocument()
        {
            XNamespace xmlns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

            // Parsing a document template from resources
            XDocument xmlDocument = XDocument.Parse(Resources.ResourceManager.GetString("sheet1"));

            // Adding data to the document
            for (int i = 0; i < Data.Count; i++)
            {
                addtoXML(ref xmlDocument, Data[i].SourceText, Data[i].Translation, Data[i].Key, i + 2);
            }

            //Saving the document
            xmlDocument.Save(@"ExelFile\xl\worksheets\sheet1.xml");
        }

        /// <summary>
        /// Adding the specified data to the specified XML document
        /// </summary>
        /// <param name="xmlDocument">XML document</param>
        /// <param name="cell_1">Data to add to column # 1</param>
        /// <param name="cell_2">Data to add to column # 2</param>
        /// <param name="cell_3">Data to add to column # 3</param>
        /// <param name="rowCounter">Row number</param>
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