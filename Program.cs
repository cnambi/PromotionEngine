using System;
using System.Collections.Generic;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SkuModel> listSkus = new List<SkuModel>();
            List<PromotionModel> listPromotions = new List<PromotionModel>();
            List<OrderModel> listOrders = new List<OrderModel>();

            bool skipCondition;
            Console.WriteLine("Hello World!");

            // Read Skus
            Sku sku;
            do
            {
                sku = new Sku();
                listSkus.Add(sku.CreateSku());
                Console.Write("Do you want to add another Sku (y/n): ");
                skipCondition = Console.ReadLine().ToLower() == "y" ? true : false;
            } while (skipCondition);

            // Read promotional offers
            Promotion promotion;
            Console.Write("Do you want to add Promotional Offers (y/n): ");
            skipCondition = Console.ReadLine().ToLower() == "y" ? true : false;
            while(skipCondition)
            {
                promotion = new Promotion();
                listPromotions.Add(promotion.CreatePromotion());
                Console.Write("Do you want to add another Promotion (y/n): ");
                skipCondition = Console.ReadLine().ToLower() == "y" ? true : false;
            };

            // Read orders
            Order order;
            bool stoporders;
            do
            {
                listOrders = new List<OrderModel>();
                Console.Write("Create your Order #100 ");
                do
                {
                    order = new Order();
                    listOrders.Add(order.CreateOrder());
                    Console.Write("Do you want to add another Sku to your order (y/n): ");
                    skipCondition = Console.ReadLine().ToLower() == "y" ? true : false;
                } while (skipCondition);

                //Generate Invoice
                Invoice invoice = new Invoice();
                invoice.GenerateInvoice(listSkus, listPromotions, listOrders);

                Console.Write("Do you want to create another order (y/n): ");
                stoporders = Console.ReadLine().ToLower() == "y" ? true : false;
            } while (stoporders);
        }
    }
}
