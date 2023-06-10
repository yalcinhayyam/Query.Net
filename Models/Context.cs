
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Models;
class Context : DbContext
{
    public DbSet<Person> Peoples { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    private readonly IConfiguration configuration;

    public Context(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Seller>()
            .HasMany(s => s.Items)
            .WithOne(i => i.Seller)
            .HasForeignKey(i => i.SellerId);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId);

        var person = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Description = "Some Content"
        };

        var address = new Address
        {
            Id = 1,
            Title = "Home Address for John Doe",
            Description = "..... address",
            PersonId = person.Id 
        };

        modelBuilder.Entity<Customer>()
                 .HasData(
                     new Customer { Id = 2, Name = "Louise" }
                 );

        modelBuilder.Entity<Seller>()
                 .HasData(
                     new Seller
                     {
                         Id = 1,  
                         Name = "Joe",
                         Code = "TXT423"
                     }
                 );

        var order = new Order
        {
            Id = 1,
            ProductTitle = "..... product",
            Count = 3,
            CustomerId = 2  
        };

        var item = new Item
        {
            Id = 1,
            Title = "..... item",
            Price = 12.7,
            SellerId = 1  
        };

        modelBuilder.Entity<Person>().HasData(person);
        modelBuilder.Entity<Address>().HasData(address);
        modelBuilder.Entity<Order>().HasData(order);
        modelBuilder.Entity<Item>().HasData(item);

    }

    // dotnet ef migrations script -o output.sql
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            options => options.EnableRetryOnFailure()
        );
    }
}


