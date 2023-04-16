using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemRazorV1.Models;

namespace ItemRazor.Comperators
{
    public class PriceComperator : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return (int) (x.Price - y.Price);
        }
    }
}
