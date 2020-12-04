using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineStore
{
    public class Catalogue
    {
        public static string item = "product name";

        public static string GetCatalogue(bool isAdmin)
        {
            return item;
        }

        public static bool RemoveItems(string item) 
        {
            return true;
        }
    }
}
