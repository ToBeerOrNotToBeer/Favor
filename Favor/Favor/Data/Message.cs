using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public class Message
    {
        public int Id { get; set; }

        public string ReceiverId { get; set; }

        public string SenderEmail { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public MessageType Type { get; set; }

        public static Message ToMessage(Message messageToPass)
        {
            return new Message
            {
                ReceiverId = messageToPass.ReceiverId,
                SenderEmail = messageToPass.SenderEmail,
                Title = messageToPass.Title,
                Content = messageToPass.Content,
                Type = messageToPass.Type
            };
        }
    }
}