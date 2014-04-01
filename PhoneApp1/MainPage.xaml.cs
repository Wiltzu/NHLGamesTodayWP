using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            loadGames();
        }

        private void loadGames()
        {
            String url = "http://192.168.11.2/nhlsport";

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            wc.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET4.0C; .NET4.0E)";
            wc.DownloadStringAsync(new Uri(url));
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(handleFoods);
            
        }

        private void handleFoods(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            String jsonStr = e.Result;
            System.Diagnostics.Debug.WriteLine(jsonStr);
            JObject jObject = (JObject)JsonConvert.DeserializeObject(jsonStr);
            String results = jObject["query"]["results"].ToString();

            TextBlock textBlock = new TextBlock();
            textBlock.Text = results;
            this.ContentPanel.RowDefinitions.Add(new RowDefinition());
            this.ContentPanel.Children.Add(textBlock);
        }

    }
}