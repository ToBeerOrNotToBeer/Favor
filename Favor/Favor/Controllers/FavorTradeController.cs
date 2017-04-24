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
        public ActionResult ExecuteTheTrade(int favorId, string recieverId, string tradeOff)
        {
            if (!ParamsAreValid(recieverId, tradeOff))
            {
                return RedirectToAction("Index", "Home");
            }
            var db = new FavorDbContext();
            var exchangeFavor = db.Favors.Find(favorId);

            var senderUser = db.Users
                .FirstOrDefault(u => u.Id == User.Identity.GetUserId());

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