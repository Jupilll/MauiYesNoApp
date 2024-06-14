using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauiYesNoApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoadButtonClicked(object sender, EventArgs e)
        {
            string url = "https://yesno.wtf/api";
            var data = await GetDataAsync(url);

            if (data != null)
            {
                lblAnswer.Text = data.Answer.ToUpper();
                imgAnswer.Source = new UriImageSource
                {
                    Uri = new Uri(data.Image),
                    CachingEnabled = false // This ensures the latest image is fetched
                };
            }
        }

        private async Task<Data> GetDataAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<Data>(response);
            }
        }

        public class Data
        {
            public string Answer { get; set; }
            public bool Forced { get; set; }
            public string Image { get; set; }
        }
    }
}
