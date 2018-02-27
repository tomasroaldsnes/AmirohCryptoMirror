using AmirohCryptoMirror.Classes;
using AmirohCryptoMirror.Persistence;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AmirohCryptoMirror
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AmountPage : ContentPage
    {
        Coin _coin;
        private SQLiteAsyncConnection _connection;
        public AmountPage(Coin c)
        {
            InitializeComponent();
            _connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            _connection.CreateTableAsync<Portfolio>();

            _coin = c;
        }


        private async void Amount_Completed(object sender, EventArgs e)
        {
            try
            {
                
                var portfolio_coin_instance = new Portfolio { CoinName = _coin.Name, Amount = entryAmount.Text };
                await _connection.InsertAsync(portfolio_coin_instance);
                await Navigation.PushAsync(new MainPage());
            }
            catch (Exception)
            {
                //
            }

        }
    }
}