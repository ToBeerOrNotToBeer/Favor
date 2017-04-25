using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public class TicketForSender
    {
        public TicketForSender()
        {
            this.Type = MessageType.Ticket;
            this.TimeSent = DateTime.Now;
            this.Reciever = "Administration";
        }

        public int Id { get; set; }

        public string Reciever { get; private set; }

        public MessageType Type { get; private set; }

        public DateTime TimeSent { get; private set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}