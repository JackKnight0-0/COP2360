using System;

public class Product
{
    public decimal Price;
    public string? Name { get; set; }

    public int Quantity;

}

public class Wine : Product
{
    public int Year;
    public Wine(decimal price) => Price = price;
    public Wine(decimal price, int year) : this(price) => Year = year;

    public void PrintInfo()
    {
        if (this.Quantity <= 0)
        {
            Console.WriteLine($"{this.Name} is out of stock.");
        }
        else
        {
            Console.WriteLine($"Wine: {this.Name}\nYear: {this.Year}\nPrice: {this.Price}\nQuantity: {this.Quantity}");
        }
    }
}


class Program
{
    static void Main()
    {
        Wine wine1 = new Wine(9.99m, 2021) { Name = "Good wine", Quantity=100};

        wine1.PrintInfo();
    }
}
