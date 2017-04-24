using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Models.FavorModels
{
    public class FavorTradeModel
    {
        public FavorTradeModel()
        {
            TimeSent = DateTime.Now;
        }

        public int Id { get; set; }

        public string SenderId { get; set; }

        public string RecieverId { get; set; }

        public string SenderFullName { get; set; }

        public string RecieverFullName { get; set; }

        public int FavorId { get; set; }

        public string FavorTitle { get; set; }

        public string FavorContent { get; set; }

        public DateTime TimeSent { get; set; }

        public string TradeOff { get; set; }
    }
}