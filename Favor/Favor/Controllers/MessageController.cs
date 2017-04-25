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

            MessageType forTheReciever = MessageType.Received;
            if (this.User != null && this.User.IsInRole("Admin"))
            {
                forTheReciever = MessageType.FromAdmin;
            }

            if (sentMessage.Type == MessageType.System)
            {
                forTheReciever = MessageType.System;
            }

            MessageType forTheSender = MessageType.Sent;
            if (sentMessage.Type == MessageType.System)
            {
                forTheSender = MessageType.System;
            }
            
            var messageForTheReceiver = Message.ToMessage(sentMessage);
            messageForTheReceiver.Type = forTheReciever;

            var messageForTheSender = Message.ToMessage(sentMessage);
            messageForTheSender.Type = forTheSender;

            receiverUser.Messages.Add(messageForTheReceiver);
            senderUser.Messages.Add(messageForTheSender);

            db.SaveChanges();

            MessageManager.MessageHasBeenSent = true;

            return RedirectToAction("OtherProfile", "Profile", new { @otherProfileId = receiverUser.Id});
        }

        [Authorize]
        public ActionResult DeleteMessage(int id)
        {
            var db = new FavorDbContext();

            var messageToRemove = db.Messages.FirstOrDefault(m => m.Id == id);
            if (messageToRemove != null)
            {
                db.Messages.Remove(messageToRemove);

                db.SaveChanges();
            }

            
            return RedirectToAction("Profile", "Profile");
        }
    }
}