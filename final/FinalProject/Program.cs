using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class GroceryItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public bool IsPurchased { get; set; }
    public decimal Price { get; set; }
}

abstract class AbstractList
{
    public List<GroceryItem> items = new List<GroceryItem>();

    public void AddItem(string name, int quantity, decimal price)
    {
        var item = new GroceryItem { Name = name, Quantity = quantity, Price = price };
        items.Add(item);
        Console.WriteLine($"Added item: {name} ({quantity})");
    }

    public void MarkItemAsPurchased(string name)
    {
        var item = items.Find(i => i.Name == name);
        if (item != null)
        {
            item.IsPurchased = true;
            Console.WriteLine($"Marked item as purchased: {name}");
        }
        else
        {
            Console.WriteLine($"Item not found: {name}");
        }
    }

    public void DisplayPurchasedItems()
    {
        Console.WriteLine("Purchased Items:");
        foreach (var item in items)
        {
            if (item.IsPurchased)
                Console.WriteLine($"{item.Name} ({item.Quantity}) - Price: {item.Price:C}");
        }
    }

    public abstract void DisplayList();

    public decimal CalculateTotalCost()
    {
        decimal totalCost = 0;
        foreach (var item in items)
        {
            totalCost += item.Price * item.Quantity;
        }
        return totalCost;
    }
}

class BasicShoppingList : AbstractList
{
    public override void DisplayList()
    {
        Console.WriteLine("Shopping List:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} ({item.Quantity}) - Price: {item.Price:C}{(item.IsPurchased ? " - Purchased" : "")}");
        }
    }
}

class PremiumShoppingList : AbstractList
{
    public void CategorizeItems()
    {
        // Implementation for categorizing items in the list
        Console.WriteLine("Categorizing items...");
    }

    public override void DisplayList()
    {
        Console.WriteLine("Premium Shopping List:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} ({item.Quantity}) - Price: {item.Price:C}{(item.IsPurchased ? " - Purchased" : "")}");
        }
    }
}

class ShoppingListManager
{
    private List<AbstractList> shoppingLists = new List<AbstractList>();

    public void CreateBasicShoppingList()
    {
        var list = new BasicShoppingList();
        shoppingLists.Add(list);
        Console.WriteLine("Basic shopping list created.");
    }

    public void CreatePremiumShoppingList()
    {
        var list = new PremiumShoppingList();
        shoppingLists.Add(list);
        Console.WriteLine("Premium shopping list created.");
    }

    public void AddItemToShoppingList(string name, int quantity, decimal price, int listIndex)
    {
        if (listIndex >= 0 && listIndex < shoppingLists.Count)
        {
            var list = shoppingLists[listIndex];
            list.AddItem(name, quantity, price);
        }
        else
        {
            Console.WriteLine("Invalid shopping list index.");
        }
    }

    public void MarkItemAsPurchased(string name, int listIndex)
    {
        if (listIndex >= 0 && listIndex < shoppingLists.Count)
        {
            var list = shoppingLists[listIndex];
            list.MarkItemAsPurchased(name);
        }
        else
        {
            Console.WriteLine("Invalid shopping list index.");
        }
    }

    public void DisplayPurchasedItems(int listIndex)
    {
        if (listIndex >= 0 && listIndex < shoppingLists.Count)
        {
            var list = shoppingLists[listIndex];
            list.DisplayPurchasedItems();
        }
        else
        {
            Console.WriteLine("Invalid shopping list index.");
        }
    }

    public void DisplayShoppingList(int listIndex)
    {
        if (listIndex >= 0 && listIndex < shoppingLists.Count)
        {
            var list = shoppingLists[listIndex];
            list.DisplayList();
        }
        else
        {
            Console.WriteLine("Invalid shopping list index.");
        }
    }

    public decimal CalculateTotalCost(int listIndex)
    {
        if (listIndex >= 0 && listIndex < shoppingLists.Count)
        {
            var list = shoppingLists[listIndex];
            return list.CalculateTotalCost();
        }
        else
        {
            Console.WriteLine("Invalid shopping list index.");
            return 0;
        }
    }

    public void SaveShoppingLists(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var list in shoppingLists)
            {
                foreach (var item in list.items)
                {
                    writer.WriteLine($"{list.GetType().Name},{item.Name},{item.Quantity},{item.Price},{item.IsPurchased}");
                }
            }
        }
        Console.WriteLine("Shopping lists saved to a file.");
    }

    public void LoadShoppingLists(string fileName)
    {
        if (File.Exists(fileName))
        {
            shoppingLists.Clear();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 5)
                    {
                        string listType = parts[0];
                        string name = parts[1];
                        int quantity = int.Parse(parts[2]);
                        decimal price = decimal.Parse(parts[3]);
                        bool isPurchased = bool.Parse(parts[4]);

                        if (listType == nameof(BasicShoppingList))
                        {
                            CreateBasicShoppingList();
                            AddItemToShoppingList(name, quantity, price, shoppingLists.Count - 1); // Fix: Added price argument
                            if (isPurchased)
                                MarkItemAsPurchased(name, shoppingLists.Count - 1);
                        }
                        else if (listType == nameof(PremiumShoppingList))
                        {
                            CreatePremiumShoppingList();
                            AddItemToShoppingList(name, quantity, price, shoppingLists.Count - 1); // Fix: Added price argument
                            if (isPurchased)
                                MarkItemAsPurchased(name, shoppingLists.Count - 1);
                        }
                    }
                }
            }
            Console.WriteLine("Shopping lists loaded from a file.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var manager = new ShoppingListManager();

        // Creating shopping lists
        manager.CreateBasicShoppingList();
        manager.CreatePremiumShoppingList();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Add item to a shopping list");
            Console.WriteLine("2. Mark item as purchased");
            Console.WriteLine("3. Display shopping lists");
            Console.WriteLine("4. Display purchased items");
            Console.WriteLine("5. Display total cost of items in a list");
            Console.WriteLine("6. Save shopping lists to a file");
            Console.WriteLine("7. Load shopping lists from a file");
            Console.WriteLine("8. Exit");

            string option = Console.ReadLine();
            Console.WriteLine();

            switch (option)
            {
                case "1":
                    AddItemToShoppingList(manager);
                    break;
                case "2":
                    MarkItemAsPurchased(manager);
                    break;
                case "3":
                    DisplayShoppingLists(manager);
                    break;
                case "4":
                    DisplayPurchasedItems(manager);
                    break;
                case "5":
                    DisplayTotalCost(manager);
                    break;
                case "6":
                    SaveShoppingLists(manager);
                    break;
                case "7":
                    LoadShoppingLists(manager);
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void AddItemToShoppingList(ShoppingListManager manager)
    {
        Console.WriteLine("Enter the name of the item:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the quantity:");
        int quantity = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the price:");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Select the shopping list (0 for Basic Shopping List, 1 for Premium Shopping List):");
        int listIndex = int.Parse(Console.ReadLine());

        manager.AddItemToShoppingList(name, quantity, price, listIndex);
    }

    static void MarkItemAsPurchased(ShoppingListManager manager)
    {
        Console.WriteLine("Enter the name of the item to mark as purchased:");
        string name = Console.ReadLine();

        Console.WriteLine("Select the shopping list (0 for Basic Shopping List, 1 for Premium Shopping List):");
        int listIndex = int.Parse(Console.ReadLine());

        manager.MarkItemAsPurchased(name, listIndex);
    }

    static void DisplayShoppingLists(ShoppingListManager manager)
    {
        Console.WriteLine("Select the shopping list to display (0 for Basic Shopping List, 1 for Premium Shopping List):");
        int listIndex = int.Parse(Console.ReadLine());

        manager.DisplayShoppingList(listIndex);
    }

    static void DisplayPurchasedItems(ShoppingListManager manager)
    {
        Console.WriteLine("Select the shopping list to display purchased items (0 for Basic Shopping List, 1 for Premium Shopping List):");
        int listIndex = int.Parse(Console.ReadLine());

        manager.DisplayPurchasedItems(listIndex);
    }

    static void DisplayTotalCost(ShoppingListManager manager)
    {
        Console.WriteLine("Select the shopping list to display total cost (0 for Basic Shopping List, 1 for Premium Shopping List):");
        int listIndex = int.Parse(Console.ReadLine());

        decimal totalCost = manager.CalculateTotalCost(listIndex);
        Console.WriteLine($"Total cost of items in the list: {totalCost:C}");
    }

    static void SaveShoppingLists(ShoppingListManager manager)
    {
        Console.WriteLine("Enter the file name to save the shopping lists:");
        string fileName = Console.ReadLine();

        manager.SaveShoppingLists(fileName);
    }

    static void LoadShoppingLists(ShoppingListManager manager)
    {
        Console.WriteLine("Enter the file name to load the shopping lists:");
        string fileName = Console.ReadLine();

        manager.LoadShoppingLists(fileName);
    }
}
