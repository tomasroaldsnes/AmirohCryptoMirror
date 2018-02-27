using AmirohCryptoMirror.Classes;
using AmirohCryptoMirror.Persistence;
using ModernHttpClient;
using Newtonsoft.Json;
using SQLite;
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
    public partial class PortfolioPage : ContentPage
    { 

        public ObservableCollection<Portfolio> _portfolio;
        public ObservableCollection<Coin> coinCollection;
        SQLiteAsyncConnection _connection;
        public PortfolioPage()
        {
            InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            


        }

        protected async override void OnAppearing()
        {

            try
            {
                await _connection.CreateTableAsync<Portfolio>();
                var pList = await _connection.Table<Portfolio>().ToListAsync();

                string url_cmc = "https://api.coinmarketcap.com/v1/ticker";

                HttpClient _client = new HttpClient(new NativeMessageHandler());

                var content_coins = await _client.GetStringAsync(url_cmc);
                var coinList = JsonConvert.DeserializeObject<List<Coin>>(content_coins);

                coinCollection = new ObservableCollection<Coin>();

                
                if (pList.Count > 0)
                {
                    foreach (var coin in coinList)
                    {
                        foreach (var portfolioCoin in pList)
                        {
                            if (coin.Name == portfolioCoin.CoinName)
                                coinCollection.Add(coin);
                        }
                    }
                    
                }


                SetImages();
                SetPriceColor();

                listviewPortfolio.ItemsSource = coinCollection;
            }
            catch(Exception e)
            {
                //
            }

        }

        private async void AddCoin_Tapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new AddCoinPage());
                //var portfolio_coin_instance = new Portfolio { CoinName = "Bitcoin", Amount = "10" };
                //await _connection.InsertAsync(portfolio_coin_instance);

            }
            catch (Exception)
            {
                //
            }
        }
        public void SetImages()
        {
            foreach (var coin in coinCollection)
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
        public void SetPriceColor()
        {
            foreach (var coin in coinCollection)
            {
                if (coin.Percent_change_24h.Contains("-"))
                {
                    coin.Price_color = "Red";
                }
                else
                {
                    coin.Price_color = "Green";
                }
            }
        }

    }
}