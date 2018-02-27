using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmirohCryptoMirror.Classes
{
    public class Portfolio
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255)]
        public string CoinName { get; set; }

        [MaxLength(255)]
        public string Amount { get; set; }




    }
}
