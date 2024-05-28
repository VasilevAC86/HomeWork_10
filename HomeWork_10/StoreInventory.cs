using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

namespace HomeWork_10
{
    public class StoreInventory<T> where T : IStoreItem
    {
        // Коллекция уникальных (по усл.задачи) товаров
        private Dictionary<Int64, double> items_ = new Dictionary<Int64, double>(); 
        public void AddItem(T obj) // Метод добавления нового товара в список товаров
        {
            try // Проверка на попытку добавить уже существующий товар в список товаров
            {
                if (items_.ContainsKey(obj.Id))
                    throw new ElementException($"Товар с индексом {obj.Id} уже есть в списке товаров!");
                else
                    items_.Add(obj.Id, obj.Price);
            }
            catch (ElementException ex) // Обработка исключения
            {                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nОшибка добавления нового товара: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;                
            }                        
        }
        public void RemoveItem(Int64 id) // Метод удаление товара из списка товаров
        {
            try // Проверка на попытку добавить уже существующий товар в список товаров
            {
                if (!items_.ContainsKey(id))
                    throw new ElementException($"Товара с Id = {id} не существует в списке товаров!");
            }
            catch (ElementException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nОшибка удаления товара: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            items_.Remove(id);
        }        
        public void Sort_Id_Order_By() // Метод сортировки коллекции товара по возрастанию Id
        {
            items_ = items_.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }
        public void Sort_Id_Order_By_Descending() // Метод сортировки коллекции товара по убыванию Id
        {
            items_ = items_.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }
        public void Sort_Price_Order_By() // Метод сортировки коллекции товара по возрастанию цены
        {
            items_ = items_.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        public void Sort_Price_Order_By_Descending() // Метод сортировки коллекции товара по убыванию цены
        {
            items_ = items_.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        public void Print(string message) // Метод вывод в консоль коллекции товаров
        {             
            int counter = 1; // Счётчик кол-ва товаров
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in items_)
            {
                Console.WriteLine($"{counter}. Id товара = {item.Key}, цена = {item.Value} руб.");
                counter++;
            }            
        }
        public void FindItemById(Int64 id) // Метод поиска товара в списке товаров по Id
        {
            if (items_.ContainsKey(id))
                Console.WriteLine($"Товар найден! Id = {id}, цена = {items_[id]} руб.");
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Товара с Id = {id} не найдено!");
                Console.ForegroundColor= ConsoleColor.White;
            }
        }
        public void Change_Price_All(double percent) // Метод изменения цены всех товаров на процент percent
        {
            items_ = items_.ToDictionary(x => x.Key, x => Math.Round(x.Value * (1 + percent / 100), 2));
        }
        public void Change_Price_One(double percent, Int64 id) // Метод изменения цены товара с Id на процент percent
        {
            if (items_.ContainsKey(id))
            {
                double tmp = Math.Round(items_[id] * (1 + percent / 100), 2);                
                items_.Remove(id);
                items_.Add(id, tmp);
            }            
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Товара с Id = {id} не найдено!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public Dictionary<Int64, double> Range(int start, int end)
        {
            return items_.Where(x => x.Value > start && x.Value < end).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}