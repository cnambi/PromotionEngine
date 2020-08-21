using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine
{
    class Sku : ISku
    {
        public SkuModel CreateSku()
        {
            SkuModel sku = new SkuModel();
            Console.Write("Enter Sku ID: ");
            sku.SID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Sku Name: ");
            sku.Name = Console.ReadLine();
            Console.Write("Enter Sku Price: ");
            sku.Price = Convert.ToDecimal(Console.ReadLine());
            return sku;
        }        
    }
}
