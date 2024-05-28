using System.Diagnostics.Metrics;

namespace HomeWork_10
{
    internal class Program
    {        
        static void Main(string[] args)
        {            
            StoreInventory<IStoreItem> s = new StoreInventory<IStoreItem>();
            Random r = new Random();
            int number_of_items = 20; // Кол-во товаров в магазине
            for (int i = 0; i < number_of_items; i++) // Цикл заполнения списка товаров
            {                   
                s.AddItem(new StoreItem(r.NextInt64(1, Int64.MaxValue), Math.Round(r.NextDouble()*10 + r.Next(0, 10000), 2)));
            }
            s.Sort_Id_Order_By(); // Сортируем коллекцию товара по возрастанию Id
            s.Print("\nСписок товаров по возрастанию Id:");

            // Добавление нового товара в список товаров
            Console.Write("\nДобавление нового товара в список товаров.\nВведите Id товара -> ");
            Int64 id = Exc_Int64(Console.ReadLine());
            Console.Write("Введите цену товара -> ");                      
            s.AddItem(new StoreItem(id, Math.Round(Exc_Double(Console.ReadLine()), 2)));
            s.Sort_Id_Order_By();
            s.Print("\nСписок товаров по возрастанию Id:");

            // Удаление товара из список товаров
            Console.Write("\nУдаление товара из списока товаров.\nВведите Id товара -> ");
            id = Exc_Int64(Console.ReadLine());
            s.RemoveItem(id);
            s.Sort_Id_Order_By();
            s.Print("\nСписок товаров по возрастанию Id:");

            // Поиск товара по Id
            Console.Write("\nПоиск товара по Id.\nВведите Id товара -> ");
            id = Exc_Int64(Console.ReadLine());
            s.FindItemById(id);
            Console.Write("\nДля продолжения нажмите любую клавишу ");
            Console.ReadKey(true);
            Console.WriteLine();

            // Сортировка коллекции товара по убыванию Id
            s.Sort_Id_Order_By_Descending();
            s.Print("\nСписок товаров по убыванию Id:");

            // Сортировка коллекции товара по возрастанию цены
            s.Sort_Price_Order_By();
            s.Print("\nСписок товаров по возрастанию цены:");

            // Сортировка коллекции товара по убыванию цены
            s.Sort_Price_Order_By_Descending();
            s.Print("\nСписок товаров по убыванию цены:");

            // Изменение цены всех товаров
            Console.Write("\nИзменение цены всех товаров.\nВведите процент изменения цены (рост - любое положительное число " +
                " снижение от 0.0 до -100.0) -> ");            
            s.Change_Price_All(Exc_Double(Console.ReadLine(), 100));
            s.Print("\nСписок товаров с изменённой ценой:");

            // Изменение цены товара с конкретным Id
            Console.Write("\nИзменение цены конкретного товара.\nВведите Id товара -> ");
            id = Exc_Int64(Console.ReadLine());
            Console.Write("Введите процент изменения цены (рост - любое положительное число, снижение от 0.0 до -100.0) -> ");
            s.Change_Price_One(Exc_Double(Console.ReadLine(), 100), id);
            s.Sort_Id_Order_By();
            s.Print($"\nОбновлённый список товаров:");
            Console.Write("\nДля продолжения нажмите любую клавишу ");
            Console.ReadKey(true);
            Console.WriteLine();

            // Группировка товаров по ценовым диапазонам
            Console.WriteLine("\nГруппировка товаров по ценовым диапазонам.\nЦеновой диапазон от 0 до 1000 руб:");
            Dictionary<Int64, double> dictionary = s.Range(0, 1000);
            Print(dictionary);
            Console.WriteLine("\nЦеновой диапазон от 1000 до 5000 руб:");
            dictionary = s.Range(1000, 5000);
            Print(dictionary);            
            Console.WriteLine("\nЦеновой диапазон от 5000 до 10000 руб:");
            dictionary = s.Range(5000, 10000);
            Print(dictionary);
            Console.WriteLine("\nЦеновой диапазон свыше 10000 руб:");
            dictionary = s.Range(10000, int.MaxValue);
            Print(dictionary);
        }
        static void Print(Dictionary<Int64, double> obj) // Метод вывода в консоль отчётов по группировке товаров в ценовых диапазонах
        {
            if (obj.Count > 0)
            {
                int counter = 1;                
                foreach (var el in obj)
                {
                    Console.WriteLine($"{counter}. Id товара = {el.Key}, цена = {el.Value} руб.");
                    counter++;
                }
            }
            else
                Console.WriteLine("Нет товаров в указанном ценовом диапазоне!");
        }
        static Int64 Exc_Int64(string message) // Метод обработки введённого пользователем значения типа Int64
        {
            Int64 number = 0;
            // Если введённое значение можно преобразовать в int, то записываем его в number
            if (Int64.TryParse(message, out number)) { }
            if (!Int64.TryParse(message, out Int64 value) || number < 1) // если введено не положительное целочисленное число, то 
            {
                while (!Int64.TryParse(message, out value) || number < 1)
                {
                    Console.Write("Введённое некорректное значение! Введите идентификатор товара (целое положительное число)" +
                        " ещё один раз -> ");
                    message = Console.ReadLine();
                    if (Int64.TryParse(message, out number)) { }
                }
            }
            return number;
        }
        // Метод обработки введённого пользователем значения Double с параметром по умолчанию
        static Double Exc_Double(string message, double min = 0.0) 
        {
            double number = 0;
            // Если введённое значение можно преобразовать в double, то записываем его в number
            if (Double.TryParse(message, out number)) { }
            if (!Double.TryParse(message, out double value) || number < -min)
            {
                while (!Double.TryParse(message, out value) || number < -min)
                {
                    Console.Write("Введённое некорректное значение! Введите вещественное число ещё раз -> ");
                    message = Console.ReadLine();
                    if (Double.TryParse(message, out number)) { }
                }
            }
            return number;
        }                
    }
}