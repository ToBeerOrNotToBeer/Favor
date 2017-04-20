using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public class PayOff
    {
        public PayOff(decimal withMoney, Category withFavor)
        {
            this.WithFavor = withFavor;
            this.WithMoney = withMoney;
        }

        public Category WithFavor { get; set; }

        public decimal WithMoney { get; set; }
    }
}