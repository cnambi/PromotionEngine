using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    interface IOrder
    {
        OrderModel CreateOrder();
    }
}
