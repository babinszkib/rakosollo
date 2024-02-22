using System;
using System.Collections.Generic;
using System.IO;

class MenuItem
{
    public string Name { get; set; }
    public double Price { get; set; }

    public MenuItem(string name, double price)
    {
        Name = name;
        Price = price;
    }
}

class Order
{
    public List<MenuItem> Items { get; set; }

    public Order()
    {
        Items = new List<MenuItem>();
    }

    public void AddItem(MenuItem item)
    {
        Items.Add(item);
    }

    public double GetTotal()
    {
        double total = 0;
        foreach (var item in Items)
        {
            total += item.Price;
        }
        return total;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<MenuItem> menu = new List<MenuItem>()
        {
            new MenuItem("Pizza", 4100),
            new MenuItem("Hamburger", 2800),
            new MenuItem("Saláta", 1800),
            new MenuItem("Cola", 550),
            new MenuItem("Kávé", 480)
        };

        Order order = new Order();

        Console.WriteLine("Köszöntünk a Rákosollóban");

        while (true)
        {
            Console.WriteLine("\nMenü:");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menu[i].Name} - {menu[i].Price}HUF");
            }

            Console.WriteLine("\nVálaszd ki a rendelésed (Használd a 0-t befejezéshez): ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > menu.Count)
            {
                Console.WriteLine("Ilyen választási lehetőség nincs! Próbáld újra.");
            }

            if (choice == 0)
            {
                break;
            }

            MenuItem selectedItem = menu[choice - 1];
            order.AddItem(selectedItem);
            Console.WriteLine($"{selectedItem.Name} hozzáadva a rendeléseidhez.");
        }


        SaveOrderToFile(order);

        Console.WriteLine("\nA te rendeléseid:");
        foreach (var item in order.Items)
        {
            Console.WriteLine($"{item.Name} - {item.Price}HUF");
        }
        Console.WriteLine($"Végösszeg: {order.GetTotal()}HUF");

        Console.WriteLine("\nKöszönjük a rendelésed! Jó étvágyat tojáska!");
    }

    static void SaveOrderToFile(Order order)
    {
        using (StreamWriter writer = new StreamWriter("kosar.txt"))
        {
            writer.WriteLine("Rendelés részletei:");
            foreach (var item in order.Items)
            {
                writer.WriteLine($"{item.Name} - {item.Price}HUF");
            }
            writer.WriteLine($"Végösszeg: {order.GetTotal()}HUF");
        }

        Console.WriteLine("Rendelés részletei sikeresen mentve a 'kosar.txt' fájlba.");
    }
}
