using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public class PayOff
    {
        public PayOff(string withDescription, decimal withMoney)
        {
            this.WithDescription = withDescription;
            this.WithMoney = withMoney;
        }

        public string WithDescription { get; set; }

        public decimal WithMoney { get; set; }
    }
}