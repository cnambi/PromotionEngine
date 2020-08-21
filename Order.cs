using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    class Order:IOrder
    {
        public OrderModel CreateOrder()
        {
            OrderModel model = new OrderModel();
            //Console.Write("Enter Order Id: ");
            //model.OrderId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Sku Id: ");
            model.SID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Quantity: ");
            model.Qty = Convert.ToInt32(Console.ReadLine());

            return model;
        }
    }
}
