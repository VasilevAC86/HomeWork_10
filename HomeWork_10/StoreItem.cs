using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_10
{
    public class StoreItem:IStoreItem
    {
        public Int64 Id { get; set; }            
        public double Price { get; set; }
        public StoreItem(Int64 id, double price)
        {
            Id = id;            
            Price = price;            
        }   
    }
}
