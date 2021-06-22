using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;

namespace Task_1
{
    public partial class MainWindow : Window
    {
        private readonly string serverSRC = @"http://tmgwebtest.azurewebsites.net/api/textstrings/";
        private readonly string headerName = "TMG-Api-Key";
        private readonly string headerValue = "0J/RgNC40LLQtdGC0LjQutC4IQ==";
        private List<TableWorkers> TableData = new List<TableWorkers>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] IDs = getIds();
            TableData.Clear();
            OutputDataTable.ItemsSource = null;
            foreach (string id in IDs)
            {
                if (id.Length == 0) { continue; }
                getInfoFromServer(id);
            }
            OutputDataTable.ItemsSource = TableData;
            OutputDataTable.Items.Refresh();
        }

        private string[] getIds()
        {
            string ids = Identificators_Box.Text.Replace(" ", "");
            if (ids.Contains(";") && ids.Contains(","))
            {
                ids = ids.Replace(",", ";");
            }
            return ids.Split(";");
        }

        private void getInfoFromServer(string id)
        {
            WebRequest webRequest = WebRequest.Create(serverSRC + id);
            webRequest.Headers.Add(headerName, headerValue);
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (Exception)
            {
                getInfoFromServer(id);
                return;
            }

            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
            {
                JObject response = JObject.Parse(sr.ReadToEnd());
                TableWorkers tableWorker = new TableWorkers(response.GetValue("text").ToString());
                TableData.Add(tableWorker);
            }
        }

        private void Identificators_Box_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Handled = Identificators_Box.Text.Length == 0 && (e.Text == "," || e.Text == ";" || e.Text == " ")) { return; }
            Regex regex = new Regex("[0-9]|[,;]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
