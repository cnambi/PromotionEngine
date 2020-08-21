using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PromotionEngine
{
    class Promotion : IPromotion
    {        
        public PromotionModel CreatePromotion()
        {
            PromotionModel model = new PromotionModel();
            bool skipCondition = false;
            Console.Write("Enter Promotion Id: ");
            model.PID = Convert.ToInt32(Console.ReadLine());
           
            do
            {                
                Console.Write("Enter Sku Id: ");
                var skuId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Sku Qty: ");
                var skuQty = Convert.ToInt32(Console.ReadLine());
                
                model.PromotionalOffer.Add(skuId, skuQty);

                Console.Write("Do you want to add another Sku for this promotion(y/n): ");
                skipCondition = Console.ReadLine().ToLower() == "y" ? true : false;
            } while (skipCondition);
            Console.Write("Enter Promotion price: ");
            model.Price = Convert.ToInt32(Console.ReadLine());
            return model;
        }
    }
}
