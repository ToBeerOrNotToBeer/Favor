using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("With Favor From Type")]
        public Category WithFavor { get; set; }

        [DisplayName("With Money")]
        public decimal WithMoney { get; set; }
    }
}