using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

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
            List<string> IDs = getIds();

            //List of used IDs to avoid duplication
            List<int> UsedIDs = new List<int>();

            //For each Id, we get a response from the server
            foreach (string id in IDs)
            {
                if (id.Length == 0 || UsedIDs.Contains(int.Parse(id))) { continue; }
                getInfoFromServer(id);
                //Adding the ID to the list of used
                UsedIDs.Add(int.Parse(id));
            }

            //Adding information to a table
            OutputDataTable.ItemsSource = TableData;
            OutputDataTable.Items.Refresh();
        }

        /// <summary>
        /// Splits the text from the input string into an ID
        /// </summary>
        /// <returns>Array of IDs</returns>
        private List<string> getIds()
        {
            string ids = Identificators_Box.Text;
            List<string> splittedIDs = new List<string>();

            // If there are 2 types of separators, then we make one type
            if (ids.Contains(";") && ids.Contains(","))
            {
                ids = ids.Replace(",", ";");
            }

            // Splitting the string by delimiters and returning an array
            foreach(string id in ids.Split(";"))
            {
                splittedIDs.Add(id);
            }

            // Deleting all invalid IDs from the request list
            for (int i = 0; i < splittedIDs.Count; i++)
            {
                string id = splittedIDs[i];
                if (int.Parse(id) is > 20 or < 1)
                {
                    while (splittedIDs.Remove(id));
                    i--;
                }
            }
            return splittedIDs;
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
            catch (Exception e)
            {
                //Showing the received error message
                MessageBox.Show("При обработке запроса для ID " + id +
                                " сервер вернул ошибку:\n" + e.Message);
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
        private void Identificators_Box_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Back) { return; }

            Regex regex = new Regex("[0-9]|[,;]");
            e.Handled = e.Key == Key.Space ||
                        !regex.IsMatch(new KeyConverter().ConvertToString(e.Key));
        }

        // Allocate incorrect identifiers to the user
        private void Identificators_Box_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string[] ids = Identificators_Box.Text.Replace(",", ";").Split(";");
            int pointer = 0;
            Identificators_Box.Focus();

            foreach (string id in ids)
            {
                if (id == "") { continue; }
                if (int.Parse(id) is > 20 or < 1)
                {
                    Identificators_Box.SelectionStart = pointer;
                    Identificators_Box.SelectionLength = id.Length;
                    Identificators_Box.SelectionBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                }
                pointer += id.Length + 1;
            }
        }
    }
}