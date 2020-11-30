using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore
{
    public static class Catalogue
    {
        public static Dictionary<int, int> items = new Dictionary<int, int>();

        public static Dictionary<int, int> GetCatalogue(bool isAdmin)
        {
            return items;
        }

        public static bool RemoveItems(Dictionary<int,int> items) 
        {
            return true;
        }
    }
}
