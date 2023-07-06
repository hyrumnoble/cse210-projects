using System;
using System.Collections.Generic;

class GroceryItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public bool IsPurchased { get; set; }
}

abstract class AbstractList
{
    protected List<GroceryItem> items = new List<GroceryItem>();

    public void AddItem(string name, int quantity)
    {
        var item = new GroceryItem { Name = name, Quantity = quantity };
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
                Console.WriteLine($"{item.Name} ({item.Quantity})");
        }
    }

    public abstract void DisplayList();
}

class BasicShoppingList : AbstractList
{
    public override void DisplayList()
    {
        Console.WriteLine("Shopping List:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} ({item.Quantity}){(item.IsPurchased ? " - Purchased" : "")}");
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
            Console.WriteLine($"{item.Name} ({item.Quantity}){(item.IsPurchased ? " - Purchased" : "")}");
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

    public void AddItemToShoppingList(string name, int quantity, int listIndex)
    {
        if (listIndex >= 0 && listIndex < shoppingLists.Count)
        {
            var list = shoppingLists[listIndex];
            list.AddItem(name, quantity);
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
}

class Program
{
    static void Main(string[] args)
    {
        var manager = new ShoppingListManager();

        // Creating shopping lists
        manager.CreateBasicShoppingList();
        manager.CreatePremiumShoppingList();

        // Adding items to shopping lists
        manager.AddItemToShoppingList("Apples", 5, 0);
        manager.AddItemToShoppingList("Milk", 2, 0);
        manager.AddItemToShoppingList("Chicken", 1, 1);
        manager.AddItemToShoppingList("Eggs", 6, 1);

        // Marking items as purchased
        manager.MarkItemAsPurchased("Apples", 0);
        manager.MarkItemAsPurchased("Eggs", 1);

        // Displaying shopping lists
        manager.DisplayShoppingList(0);
        manager.DisplayShoppingList(1);

        // Displaying purchased items
        manager.DisplayPurchasedItems(0);
        manager.DisplayPurchasedItems(1);
    }
}
