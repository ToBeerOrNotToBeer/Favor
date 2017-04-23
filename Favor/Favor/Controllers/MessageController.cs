using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Favor.Data;
using System.Data.Entity;

namespace Favor.Controllers
{
    public class MessageController : Controller
    {
        [HttpPost]
        [Authorize]
        public ActionResult SendMessage(Message sentMessage)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            var receiverUser = db.Users.Include(u => u.Messages).FirstOrDefault(u => u.Id == sentMessage.ReceiverId);
            var senderUser = db.Users.Include(u => u.Messages).FirstOrDefault(u => u.UserName == sentMessage.SenderEmail);

            if (receiverUser == null || senderUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var messageForTheReceiver = Message.ToMessage(sentMessage);
            messageForTheReceiver.Type = MessageType.Received;

            var messageForTheSender = Message.ToMessage(sentMessage);
            messageForTheSender.Type = MessageType.Sent;

            receiverUser.Messages.Add(messageForTheReceiver);
            senderUser.Messages.Add(messageForTheSender);

            db.SaveChanges();

            return RedirectToAction("OtherProfile", "Profile", new { @otherProfileId = receiverUser.Id});
        }
    }
}