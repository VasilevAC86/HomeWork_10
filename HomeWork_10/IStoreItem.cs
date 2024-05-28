using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_10
{
    public interface IStoreItem
    {
        Int64 Id { get; set; } // Уникальный идентификатор товара       
        double Price { get; } // Цена        
    }
}
