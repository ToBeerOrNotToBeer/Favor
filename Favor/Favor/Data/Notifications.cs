using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public class Notifications
    {
        public int FromMessages { get; set; }

        public int FromPendingFavors { get; set; }

        public int TotalNotifications
        {
            get { return (this.FromMessages + this.FromPendingFavors); }
        }

        public void ClearNotifications()
        {
            this.FromMessages = 0;
            this.FromPendingFavors = 0;
        }
    }
}