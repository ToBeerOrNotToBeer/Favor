﻿using Favor.Data;
using Favor.Models.FavorModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var fullFavor = db.Favors.Find(favorTradeModel.FavorId);

            if (!ParamsAreValid(favorTradeModel, fullFavor))
            {
                return RedirectToAction("Profile", "Profile");
            }

            var receiverUser = db.Users.Include(u => u.PendingFavors).Include(u => u.AccomplishedFavors).FirstOrDefault(u => u.Id == favorTradeModel.RecieverId);
            var senderUser = db.Users.Include(u => u.SentFavors).Include(u => u.AccomplishedFavors).FirstOrDefault(u => u.Id == favorTradeModel.SenderId);

            var accomplishedTradeModelForTheReceiver = new AccomplishedTradeModel
            {
                FavorTitle = favorTradeModel.FavorTitle,
                FavorContent = favorTradeModel.FavorContent,
                TradeOff = favorTradeModel.TradeOff,
                SenderId = favorTradeModel.SenderId,
                SenderFullName = favorTradeModel.SenderFullName,
                ReceiverId = favorTradeModel.RecieverId,
                ReceiverFullName = favorTradeModel.RecieverFullName
            };

            var accomplishedTradeForTheSender = AccomplishedTradeModel.ToAccomplishedTradeModel(accomplishedTradeModelForTheReceiver);

            if (!ParamsAreValid(receiverUser, senderUser))
            {
                return RedirectToAction("Profile", "Profile");
            }

            List<FavorTradeModel> allFavorTradeModels = db.FavorTradeModels.Where(ftm => ftm.FavorId == favorTradeModel.FavorId).ToList();

            for (int currFavorTradeModel = 0; currFavorTradeModel < allFavorTradeModels.Count; currFavorTradeModel++)
            {
                db.FavorTradeModels.Remove(allFavorTradeModels[currFavorTradeModel]);
            }
            
            receiverUser.AccomplishedFavors.Add(accomplishedTradeModelForTheReceiver);
            senderUser.AccomplishedFavors.Add(accomplishedTradeForTheSender);

            db.Favors.Remove(fullFavor);

            db.SaveChanges();

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

            var senderId = User.Identity.GetUserId();

            if (db.FavorTradeModels.Any(f => f.SenderId == senderId && f.FavorId == favorId))
            {
                return RedirectToAction("Details", "Favor", new { @id = favorId });
            }

            var exchangeFavor = db.Favors.Find(favorId);
            
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