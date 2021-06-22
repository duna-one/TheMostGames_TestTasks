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
        //Data for working with the server
        private readonly string serverSRC = @"http://tmgwebtest.azurewebsites.net/api/textstrings/";
        private readonly string headerName = "TMG-Api-Key";
        private readonly string headerValue = "0J/RgNC40LLQtdGC0LjQutC4IQ==";

        //Table Data
        private readonly List<TableWorkers> TableData = new List<TableWorkers>();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button Click Handler
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Clearing a table and the structure that stores its data
            TableData.Clear();
            OutputDataTable.ItemsSource = null;

            //Getting the id
            string[] IDs = getIds();

            //For each Id, we get a response from the server
            foreach (string id in IDs)
            {
                if (id.Length == 0) { continue; }
                getInfoFromServer(id);
            }

            //Adding information to a table
            OutputDataTable.ItemsSource = TableData;
            OutputDataTable.Items.Refresh();
        }

        /// <summary>
        /// Splits the text from the input string into an ID
        /// </summary>
        /// <returns>Array of IDs</returns>
        private string[] getIds()
        {
            //Remove spaces
            string ids = Identificators_Box.Text.Replace(" ", "");

            //If there are 2 types of separators, then we make one type
            if (ids.Contains(";") && ids.Contains(","))
            {
                ids = ids.Replace(",", ";");
            }

            //Splitting the string by delimiters and returning an array
            return ids.Split(";");
        }

        /// <summary>
        /// Getting information from the server
        /// </summary>
        /// <param name="id">Id used to get the string</param>
        private void getInfoFromServer(string id)
        {
            WebRequest webRequest = WebRequest.Create(serverSRC + id);
            webRequest.Headers.Add(headerName, headerValue);
            WebResponse webResponse;
            try
            {
                //Request to server
                webResponse = webRequest.GetResponse();
            }
            catch (Exception)
            {
                //If server returns error, try again
                getInfoFromServer(id);
                return;
            }

            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
            {
                //Parse response to json
                JObject response = JObject.Parse(sr.ReadToEnd());
                //Getting a string from json and creating a table data object
                TableWorkers tableWorker = new TableWorkers(response.GetValue("text").ToString());
                //Add to table data list
                TableData.Add(tableWorker);
            }
        }

        /// <summary>
        /// Input text filter
        /// </summary>
        private void Identificators_Box_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //No separator as first symbol
            if (e.Handled = Identificators_Box.Text.Length == 0 && (e.Text == "," || e.Text == ";" || e.Text == " ")) { return; }

            Regex regex = new Regex("[0-9]|[,;]");
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}