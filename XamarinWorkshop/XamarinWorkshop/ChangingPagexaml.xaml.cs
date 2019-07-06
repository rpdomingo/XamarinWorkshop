using Acr.UserDialogs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinWorkshop.Models;

namespace XamarinWorkshop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangingPagexaml : ContentPage
    {
        private const string KEY = "ListItems";
        private readonly IMemoryCache _memoryCache;

        public ObservableCollection<MakeUpFake> MakeUps { get; set; }

        public ChangingPagexaml()
        {
            InitializeComponent();

            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            SetData();
        }

        private async void SetData()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait getting items from the server");
                string apiUri = "https://makeup-api.herokuapp.com/api/v1/products.json";
                var httpClient = new System.Net.Http.HttpClient();
                var _httpResponse = await httpClient.GetAsync(apiUri);
                var json = _httpResponse.Content.ReadAsStringAsync().Result;

                if (!string.IsNullOrEmpty(json))
                {
                    var fakeItems = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<MakeUpFake>>(json);
                    _memoryCache.Set(KEY, fakeItems, DateTimeOffset.Now.AddMinutes(5));

                    MakeUps = new ObservableCollection<MakeUpFake>(fakeItems);
                    listView.ItemsSource = MakeUps;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //chito.comment this mem get to show caching works
            if (_memoryCache.TryGetValue(KEY, out IEnumerable<MakeUpFake> value))
                MakeUps = new ObservableCollection<MakeUpFake>(value);
            else
                SetData();
        }

        //private async void Button_Clicked_1(object sender, EventArgs e)
        //    => await Navigation.PushAsync(new SettingsPluginPage());
    }
}