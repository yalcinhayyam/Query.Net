

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");
var configuration = builder.Build();


var serviceCollection = new ServiceCollection();

serviceCollection.AddSingleton<IConfiguration>(configuration);
serviceCollection.AddSingleton<Service>();
serviceCollection.AddDbContext<Context>();

var serviceProvider = serviceCollection.BuildServiceProvider();


using Context context = serviceProvider.GetRequiredService<Context>();

QueryAnonymous();
QueryFullName();
QueryIncludeAll();
QueryIncludeFirst();
QueryCustomer();
QueryLinq();
TraceString();



void QueryAnonymous()
{
    var query = context.Peoples.Select(e => new { e.FirstName, e.LastName });

}

void QueryFullName()
{
    var query = context.Peoples.Select(e => new FullName(e.FirstName, e.LastName));
}

void QueryIncludeAll()
{
    var queryInclude = context.Peoples.Include(x => x.Addresses).Select(e => new { e.FirstName, e.LastName, AddressesTittle = e.Addresses.Select(x => x.Title) });
    Console.WriteLine(queryInclude.First());
    Console.WriteLine(queryInclude.ToQueryString());
}
void QueryIncludeFirst()
{
    var queryInclude = context.Peoples.Include(x => x.Addresses).Select(e => new { e.FirstName, e.LastName, AddressesTittle = e.Addresses.Select(x => x.Title).First() });
    Console.WriteLine(queryInclude.ToQueryString());

}
void QueryCustomer()
{
    var customers = context.Set<Customer>().Select(x => x.Name);
    Console.WriteLine(customers.ToQueryString());
    Console.WriteLine(customers.First());

}


void QueryLinq()
{
    IQueryable<string> raw_query = from x in context.Peoples select x.FirstName;

    var raw = raw_query.FirstOrDefault();

    Console.WriteLine(raw_query.ToQueryString());
    Console.WriteLine(raw);

}
void TraceString()
{
    var query = from e in context.Peoples select e.FirstName;

    var sql = ((dynamic)query).ToTraceString();
    Console.WriteLine(sql);

}




public class Service
{
    public string getText()
    {
        return "Some Text";
    }
}