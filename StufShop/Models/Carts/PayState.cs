using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StufShop.Models.Carts
{
    public enum  PayState:byte
    {
        [Description("پرداخت شده")]
        Paied=1,
        [Description("پرداخت نشده")]
        UnPaied =2
    }
}
