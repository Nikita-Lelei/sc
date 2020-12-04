using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore
{
    public static class OrderSystem
    {
        private static Dictionary<Guid, string> orders = new Dictionary<Guid, string>();

        public static bool AdjustStock(string item)
        {
            try
            {
                var product = Catalogue.RemoveItems(item);
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
              var item = Catalogue.item;
                orders.TryGetValue(payment.Id, out item);
                var res = AdjustStock(item);
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

        public static bool ProcessOrder(string item)
        {
            var result = true;
            try
            {
                orders.Add(new Guid(), item);
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
