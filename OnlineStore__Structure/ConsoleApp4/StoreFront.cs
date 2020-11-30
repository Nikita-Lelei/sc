using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore
{
    public class StoreFront
    {
        public Dictionary<int, int> GetCustomerCatalogue()
        {
            return Catalogue.GetCatalogue(false);
        }

        public Dictionary<int, int> GetAdminCatalogue()
        {
            return Catalogue.GetCatalogue(true);
        }
        
        public bool ProcessOrder(Dictionary<int, int> itemIds)
        {
            return OrderSystem.ProcessOrder(itemIds);
        }
    }
}
