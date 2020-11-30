using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore
{
    public static class OrderSystem
    {
        private static Dictionary<Guid, Dictionary<int, int>> orders = new Dictionary<Guid, Dictionary<int, int>>();

        public static bool AdjustStock(Dictionary<int, int> items)
        {
            try
            {
                var item = Catalogue.RemoveItems(items);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static bool ProcessPayment(dynamic payment)
        {
            try
            {
                Dictionary<int, int> items;
                orders.TryGetValue(payment.Id, out items);
                var res = AdjustStock(items);
                if (!res)
                {
                    return false;
                }

                orders.Remove(payment.Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ProcessOrder(Dictionary<int,int> itemIds)
        {
            var result = true;
            try
            {
                orders.Add(new Guid(), itemIds);
                return result;
            }
            catch(Exception)
            {
                result = false;
                return result;
            }
            
        }
    }
}
