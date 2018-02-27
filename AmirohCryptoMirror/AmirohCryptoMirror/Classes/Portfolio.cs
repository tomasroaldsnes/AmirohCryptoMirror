using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AmirohCryptoMirror.Classes
{
    public class Portfolio : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _CoinName;
        private string _Amount;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string CoinName {
            get { return _CoinName; }
            set
            {
                if (_CoinName == value)
                    return;
                _CoinName = value;

                OnPropertyChanged();
            }

                
        }

        [MaxLength(255)]
        public string Amount
        {
            get { return _Amount; }
            set
            {
                if (_Amount == value)
                    return;
                _Amount = value;

                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


    }
}
