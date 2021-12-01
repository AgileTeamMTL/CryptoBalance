using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoBalance.Models
{
    public class CryptoCoin
    {

        public string Name { get; set; }

        public int Id { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public double CurrentPrice { get; set; }
        public double Change24h { get; set; }

        public CryptoCoin(int id, string name, int rank, string symbol, double currentPrice, double change24h)
        {
            Id = id;
            Name = name;
            Rank = rank;
            Symbol = symbol;
            CurrentPrice = currentPrice;
            Change24h = change24h;
        }
    }
}