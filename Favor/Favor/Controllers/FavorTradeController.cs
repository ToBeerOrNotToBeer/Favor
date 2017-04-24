using Favor.Data;
using Favor.Models.FavorModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Favor.Controllers
{
    public class FavorTradeController : Controller
    {
        [Authorize]
        public ActionResult AcceptTrade(int? id)
        {
            if (!ParamsAreValid(id))
            {
                return RedirectToAction("Profile", "Profile");
            }

            var db = new FavorDbContext();

            var favorTradeModel = db.FavorTradeModels.Find(id);

            if (!ParamsAreValid(favorTradeModel))
            {
                return RedirectToAction("Profile", "Profile");
            }

            var receiverUser = db.Users.Find(favorTradeModel.RecieverId);
            var senderUser = db.Users.Find(favorTradeModel.SenderId);

            if (!ParamsAreValid(receiverUser, senderUser))
            {
                return RedirectToAction("Profile", "Profile");
            }

            return RedirectToAction("Profile", "Profile");
        }

        [Authorize]
        public ActionResult CancelTrade(int? id)
        {
            if (!ParamsAreValid(id))
            {
                return RedirectToAction("Profile", "Profile");
            }

            var db = new FavorDbContext();

            var favorTradeModel = db.FavorTradeModels.Find(id);

            if (!ParamsAreValid(favorTradeModel))
            {
                return RedirectToAction("Profile", "Profile");
            }

            db.FavorTradeModels.Remove(favorTradeModel);

            db.SaveChanges();

            return RedirectToAction("Profile", "Profile");
        }

        [Authorize]
        public ActionResult ExecuteTheTrade(int? favorId, string recieverId, string tradeOff)
        {
            if (User.Identity.GetUserId() == recieverId)
            {
                return RedirectToAction("Details", "Favor", new { @id = favorId });
            }

            if (!ParamsAreValid(favorId, recieverId, tradeOff))
            {
                return RedirectToAction("Index", "Home");
            }

            var db = new FavorDbContext();

            if (db.FavorTradeModels.Any(f=> f.FavorId == favorId))
            {
                return RedirectToAction("Details", "Favor", new { @id = favorId });
            }

            var exchangeFavor = db.Favors.Find(favorId);

            var senderId = User.Identity.GetUserId();

            var senderUser = db.Users
                .FirstOrDefault(u=> u.Id == senderId);

            var recieverUser = db.Users.Find(recieverId);

            if (!ParamsAreValid(exchangeFavor,recieverUser,senderUser))
            {
                return RedirectToAction("Index", "Home");
            }


            var favorTradeModel = new FavorTradeModel
            {
                FavorTitle = exchangeFavor.Title,
                FavorContent = exchangeFavor.Description,
                RecieverFullName = recieverUser.FullName,
                SenderFullName = senderUser.FullName,
                SenderId = senderUser.Id,
                RecieverId = recieverUser.Id,
                TradeOff = tradeOff,
                FavorId = exchangeFavor.Id
            };

            recieverUser.PendingFavors.Add(favorTradeModel);
            senderUser.SentFavors.Add(favorTradeModel);
            db.SaveChanges();

            return RedirectToAction("Details", "Favor", new { @id = favorId });
        }

        private bool ParamsAreValid(params object[] paramsToCheck)
        {
            bool areValid = true;

            foreach (object param in paramsToCheck)
            {
                if (param== null)
                {
                    areValid = false;
                    break;
                }
            }

            return areValid;
        }
    }
}