using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favor.Data
{
    public class PayOff
    {
        public PayOff()
        {
        }

        public PayOff(decimal withMoney, Category withFavor)
        {
            this.WithFavor = withFavor;
            this.WithMoney = withMoney;
        }

        public int Id { get; set; }

        public Category WithFavor { get; set; }

        public decimal WithMoney { get; set; }
    }
}