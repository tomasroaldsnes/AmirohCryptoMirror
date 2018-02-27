using AmirohCryptoMirror.Classes;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AmirohCryptoMirror
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddCoinPage : ContentPage
	{
        ObservableCollection<Coin> coinList;
        public AddCoinPage()
		{
			InitializeComponent();
            coinListview.ItemsSource = coinList;
		}

        protected async override void OnAppearing()
        {
            string url_cmc = "https://api.coinmarketcap.com/v1/ticker/?limit=100";

            HttpClient _client = new HttpClient(new NativeMessageHandler());

            var content_coins = await _client.GetStringAsync(url_cmc);
            var cList = JsonConvert.DeserializeObject<List<Coin>>(content_coins);

            coinList = new ObservableCollection<Coin>(cList);
            SetImages();
            coinListview.ItemsSource = coinList;
        }

        public void SetImages()
        {
            foreach (var coin in coinList)
            {
                try
                {
                    string image = coin.Symbol.ToLower() + ".png";
                    coin.Image = image;
                }
                catch
                {
                    //
                }
            }
        }

        private async void Coin_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            try
            {
                if (coinListview.SelectedItem != null)
                {

                    var obj = e.Item as Coin;

                    await Navigation.PushAsync(new AmountPage(obj));
                }

            }
            catch (Exception ex)
            {
                //
            }
        }

    }
}