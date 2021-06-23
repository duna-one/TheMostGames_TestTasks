using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Task_2
{
    class DataStructure
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

    class Parcer
    {
        public List<DataStructure> Data { get; private set; }

        public Parcer(string inputPath, string outputPath, string filename)
        {
            using (StreamReader fileReader = new StreamReader(inputPath))
            {
                JObject dataJSON = JObject.Parse(inputPath);

                foreach (JToken chld in dataJSON["Children"])
                {
                    Data.Add(new DataStructure((string)chld["Source"]["Text"],
                                               (string)chld["Translation"]["Text"],
                                               (string)chld["Key"]));
                }
            }

            CreateXmlDocument();
            CreateExcelFile(outputPath, filename);
        }

        private void CreateExcelFile(string outputPath, string filename)
        {
            string fileFormat = ".xlsx";
            if (!outputPath.EndsWith(@"\"))
            {
                outputPath += @"\";
            }

            ZipFile.CreateFromDirectory("ExelFileTemplate", outputPath+filename+fileFormat);
        }

        private void CreateXmlDocument()
        {
            uint rowCounter = 1;
            XDocument xmlDocument = XDocument.Load(@"\ExelFileTemplate\xl\worksheets\sheet1.xml");
            xmlDocument.Element("worksheet")
                       .Element("sheetData")
                       .Elements("row").Remove();

            addtoXML(ref xmlDocument, "Source text", "Translate", "Key", rowCounter);
            rowCounter++;
           

            foreach(DataStructure dataUnit in Data)
            {
                addtoXML(ref xmlDocument, dataUnit.SourceText, dataUnit.Translation, dataUnit.Key, rowCounter);
                rowCounter++;
            }
            xmlDocument.Save(@"\ExelFileTemplate\xl\worksheets\sheet1.xml");
        }

        private void addtoXML(ref XDocument xmlDocument, string cell_1, string cell_2, string cell_3, uint rowCounter)
        {
            xmlDocument.Element("worksheet")
                       .Element("sheetData")
                       .Add(new XElement("row", new XElement("c", new XElement("v", cell_1),
                                                                  new XAttribute("r", "A" + rowCounter),
                                                                  new XAttribute("t", "s")),

                                                new XElement("c", new XElement("v", cell_2),
                                                                  new XAttribute("r", "B" + rowCounter),
                                                                  new XAttribute("t", "s")),

                                                new XElement("c", new XElement("v", cell_3),
                                                                  new XAttribute("r", "C" + rowCounter),
                                                                  new XAttribute("t", "s"))),

                                                new XAttribute("r", rowCounter),
                                                new XAttribute("spans", "1:3"),
                                                new XAttribute("x14ac:dyDescent", "0.25"));
        }
    }
}
