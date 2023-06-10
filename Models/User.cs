namespace Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

}

public class Seller : User
{
    public string Code { get; set; } = null!;

    public ICollection<Item> Items { get; set; } = null!;
}

public class Customer : User
{
    public ICollection<Order> Orders { get; set; } = null!;

}
public class Item
{
    public int Id { get; set; }
    public int SellerId { get; set; }

    public string Title { get; set; } = null!;
    public double Price { get; set; }

    public Seller Seller { get; set; } = null!;


}
public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string ProductTitle { get; set; } = null!;
    public int Count { get; set; }
    public Customer Customer { get; set; } = null!;

}