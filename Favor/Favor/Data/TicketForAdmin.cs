using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public class TicketForAdmin
    {
        public TicketForAdmin()
        {
            this.TimeSent = DateTime.Now;
            this.Type = MessageType.Ticket;
        }

        public int Id { get; set; }

        public string SenderId { get; set; }

        public string SenderFullName { get; set; }

        public string RecieverEmail { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime TimeSent { get; private set; }

        public MessageType Type { get; private set; }
    }
}