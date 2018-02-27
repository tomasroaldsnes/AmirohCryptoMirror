﻿using AmirohCryptoMirror.Classes;
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

            listviewPortfolio.ItemsSource = coinCollection;

        }

        protected async override void OnAppearing()
        {
            var pList = await _connection.Table<Portfolio>().ToListAsync();
            string url_cmc = "https://api.coinmarketcap.com/v1/ticker/?limit=10";

            HttpClient _client = new HttpClient(new NativeMessageHandler());

            var content_coins = await _client.GetStringAsync(url_cmc);
            var coinList = JsonConvert.DeserializeObject<List<Coin>>(content_coins);

            coinCollection = new ObservableCollection<Coin>();

            int index = 0;
            foreach (var coin in coinList)
            {
                if(coin.Name == pList[index].CoinName)
                {
                    coinCollection.Add(coin);
                }
                index++;
            }

           

            SetPriceColor();

            listviewPortfolio.ItemsSource = coinCollection;

        }

        private async void AddCoin_Tapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new AddCoinPage());
            }
            catch (Exception)
            {
                //
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