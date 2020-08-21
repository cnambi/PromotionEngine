using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace PromotionEngine
{
    class Invoice
    {
        public void GenerateInvoice(List<SkuModel> skus, List<PromotionModel> promotions, List<OrderModel> orders)
        {
            int totalItems = skus.Count;
            Console.WriteLine("************** Order detail ***************");
            int offersCount = 0;
            decimal totalBill = 0;
            foreach (var offer in promotions)
            {
                if (offersCount >= 2)
                    break;

                if(offer.PromotionalOffer.Count == 1)
                {
                    var billAmount = CalculateSinglePromotion(skus, offer, orders);
                    totalBill += billAmount;
                    offersCount++;
                }
                else
                {
                    while (IsOfferEligible(offer.PromotionalOffer, orders))
                    {
                        var billAmount = CalculateMultiplePromotion(skus, offer, orders);
                        totalBill += billAmount;
                        offersCount++;
                        if (offersCount >= 2)
                            break;
                    }
                }
            }
            foreach (var order in orders)
            {
                if (order.Qty > 0)
                {
                    SkuModel sku = skus.Where(x => x.SID == order.SID).FirstOrDefault();
                    Console.WriteLine("Sku name: " + sku.Name + " Order Qty (" + order.Qty + "): " + order.Qty * sku.Price);
                    totalBill += order.Qty * sku.Price;
                }
            }
            Console.WriteLine("------------------------");
            Console.WriteLine("Total bill : " + totalBill);
            Console.ReadLine();
        }

        decimal CalculateSinglePromotion(List<SkuModel> skus, PromotionModel promotion, List<OrderModel> orders)
        {
            decimal billAmount = 0;
            KeyValuePair<int,int> offer = promotion.PromotionalOffer.FirstOrDefault();
            OrderModel order = orders.Where(x => x.SID == offer.Key).FirstOrDefault();
            //if(orders.Where(x => x.SID == item.Key) == null || (orders.Where(x => x.SID == item.Key) != null && order.Qty < item.Value))
            if (orders.Where(x => x.SID == offer.Key) != null && order.Qty >= offer.Value)
            {
                var count = order.Qty / offer.Value;
                var value = promotion.Price * count;
                SkuModel sku = skus.Where(x => x.SID == order.SID).FirstOrDefault();
                Console.WriteLine("Sku name: " + sku.Name + " Order Qty (" + count + " * " + offer.Value + ") offer applied* : " + value);
                order.Qty -= count * offer.Value;
                billAmount += value;
            }
            return billAmount;
        }

        decimal CalculateMultiplePromotion(List<SkuModel> skus, PromotionModel promotion, List<OrderModel> orders)
        {
            decimal billAmount = 0;
            string skuNames = string.Empty;
            string offersCount = string.Empty;
            skuNames = string.Empty;
            foreach (var offer in promotion.PromotionalOffer)
            {
                OrderModel order = orders.Where(x => x.SID == offer.Key).FirstOrDefault();
                SkuModel inSku = skus.Where(x => x.SID == order.SID).FirstOrDefault();
                skuNames += string.IsNullOrEmpty(skuNames) ? (offer.Value + inSku.Name) : " + " + (offer.Value + inSku.Name);
                order.Qty -= offer.Value;
            }
            billAmount += promotion.Price;

            Console.WriteLine("Sku names: (" + skuNames + ") Order Qty (1) offer applied* : " + billAmount);
            return billAmount;
        }

        bool IsOfferEligible(Dictionary<int,int> offers, List<OrderModel> orders)
        {
            bool isOfferApply = true;
            foreach (var promo in offers)
            {
                OrderModel inOrder = orders.Where(x => x.SID == promo.Key).FirstOrDefault();
                if (inOrder == null || (inOrder != null && inOrder.Qty < promo.Value))
                {
                    isOfferApply = false;
                    break;
                }
            }
            return isOfferApply;
        }
    }
}
