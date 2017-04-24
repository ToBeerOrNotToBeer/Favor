using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Models.FavorModels
{
    public class AccomplishedTradeModel
    {
        public AccomplishedTradeModel()
        {
            this.TimeSent = DateTime.Now;
        }

        public int Id { get; set; }

        public string SenderId { get; set; }

        public string SenderFullName { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverFullName { get; set; }

        public string FavorTitle { get; set; }

        public string FavorContent { get; set; }

        public DateTime TimeSent { get; set; }

        public string TradeOff { get; set; }

        public static AccomplishedTradeModel ToAccomplishedTradeModel(AccomplishedTradeModel toTransfer)
        {
            return new AccomplishedTradeModel
            {
                SenderId = toTransfer.SenderId,
                SenderFullName = toTransfer.SenderFullName,
                ReceiverId = toTransfer.ReceiverId,
                ReceiverFullName = toTransfer.ReceiverFullName,
                FavorTitle = toTransfer.FavorTitle,
                FavorContent = toTransfer.FavorContent,
                TimeSent = toTransfer.TimeSent,
                TradeOff = toTransfer.TradeOff
            };
        }
    }
}