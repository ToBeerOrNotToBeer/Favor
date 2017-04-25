using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Favor.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        [Authorize]
        public ActionResult SendTicket(string title, string content, string senderEmail)
        {
            if (this.User.IsInRole("Admin") ||
                senderEmail == null ||
                title == null ||
                content == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            List<User> allAdmins = new List<User>();
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var adminRole = roleManager.FindByName("Admin");
            allAdmins = db.Users.Where(x => x.Roles.Any(s => s.RoleId == adminRole.Id)).ToList();
            
            if (allAdmins.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var senderUser = db.Users.FirstOrDefault(u => u.Email == senderEmail);

            var ticketForSender = new TicketForSender
            {
                Title = title,
                Content = content
            };

            senderUser.TicketsForSender.Add(ticketForSender);

            foreach (var user in allAdmins)
            {
                var ticketForAdmin = new TicketForAdmin
                {
                    SenderId = senderUser.Id,
                    SenderFullName = senderUser.FullName,
                    RecieverEmail = user.Email,
                    Title = title,
                    Content = content,
                };

                user.TicketsForAdmin.Add(ticketForAdmin);
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}