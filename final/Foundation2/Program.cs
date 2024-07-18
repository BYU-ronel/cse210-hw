using System;
using System.Collections.Generic;

public class Address
{
    private string streetAddress;
    private string city;
    private string stateProvince;
    private string country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.stateProvince = stateProvince;
        this.country = country;
    }

    public bool IsInPhilippines()
    {
        return country.ToUpper() == "Philippines";
    }

    public string GetFullAddress()
    {
        return $"{streetAddress}\n{city}, {stateProvince} {country}";
    }
}

public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInPhilippines()
    {
        return address.IsInPhilippines();
    }

    public string GetName()
    {
        return name;
    }

    public Address GetAddress()
    {
        return address;
    }
}

public class Product
{
    private string name;
    private string productId;
    private decimal pricePerUnit;
    private int quantity;

    public Product(string name, string productId, decimal pricePerUnit, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.pricePerUnit = pricePerUnit;
        this.quantity = quantity;
    }

    public decimal GetTotalCost()
    {
        return pricePerUnit * quantity;
    }

    public string GetName()
    {
        return name;
    }

    public string GetProductId()
    {
        return productId;
    }
}

public class Order
{
    private List<Product> products;
    private Customer customer;
    private decimal shippingCost;

    public Order(Customer customer)
    {
        this.customer = customer;
        products = new List<Product>();
        shippingCost = customer.LivesInPhilippines() ? 5m : 35m;
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public decimal GetTotalCost()
    {
        decimal total = 0;
        foreach (Product product in products)
        {
            total += product.GetTotalCost();
        }
        return total + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "";
        foreach (Product product in products)
        {
            label += $"{product.GetName()} ({product.GetProductId()})\n";
        }
        return label.Trim();
    }

    public string GetShippingLabel()
    {
        return $"{customer.GetName()}\n{customer.GetAddress().GetFullAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("Tobes St", "Bobon", "Northern Samar", "Philippines");
        Customer customer1 = new Customer("John Dela Cruz", address1);

        Address address2 = new Address("Domsat Rd.", "Davao City", "Davao del Sur", "Philippines");
        Customer customer2 = new Customer("Ronel Espaldon", address2);

        Product product1 = new Product("Widget", "W001", 10.99m, 2);
        Product product2 = new Product("Gizmo", "G002", 5.99m, 3);
        Product product3 = new Product("Thingamajig", "T003", 7.99m, 1);

        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product2);
        order2.AddProduct(product3);

        Console.WriteLine("Order 1:");
        Console.WriteLine($"Packing Label:\n{order1.GetPackingLabel()}");
        Console.WriteLine($"Shipping Label:\n{order1.GetShippingLabel()}");
        Console.WriteLine($"Total Cost: {order1.GetTotalCost():C2}");

        Console.WriteLine("\nOrder 2:");
        Console.WriteLine($"Packing Label:\n{order2.GetPackingLabel()}");
        Console.WriteLine($"Shipping Label:\n{order2.GetShippingLabel()}");
        Console.WriteLine($"Total Cost: {order2.GetTotalCost():C2}");
    }
}