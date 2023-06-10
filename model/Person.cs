namespace Models;


public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual ICollection<Address> Addresses { get; set; } = null!;
}


public class Address
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public virtual Person Person { get; set; } = null!;

}


public record FullName(string FirstName, string LastName)
{
    public override string ToString()
    {
        return $"{this.LastName}, {this.FirstName}";
    }
}