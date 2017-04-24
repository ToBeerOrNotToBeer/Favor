using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string SenderEmail { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int MatchingId { get; set; }

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