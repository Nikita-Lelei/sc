using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore
{
    public class StoreFront
    {
        public string GetCustomerCatalogue()
        {
            return Catalogue.GetCatalogue(false);
        }

        public string GetAdminCatalogue()
        {
            return Catalogue.GetCatalogue(true);
        }
        
        public bool ProcessOrder(string item)
        {
            return OrderSystem.ProcessOrder(item);
        }
    }
}
