using System;
using System.Collections.Generic;
using System.Text;

namespace AmirohCryptoMirror.Classes
{
    public class Coin
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Rank { get; set; }

        public string Price_usd { get; set; }
        public string Price_btc { get; set; }

        public string Marketcap_usd { get; set; }
        public string Available_supply { get; set; }

        public string Total_supply { get; set; }
        public string Percent_change_1h { get; set; }
        public string Percent_change_24h { get; set; }
        public string Percent_change_7d { get; set; }
        public string Last_updated { get; set; }
        
        public string Price_color { get; set; }

        public string Image { get; set; }

        public string OwnedAmount { get; set; }




    }


}
