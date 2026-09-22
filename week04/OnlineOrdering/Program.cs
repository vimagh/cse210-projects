using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address(
            "123 Main Street",
            "Rexburg",
            "Idaho",
            "USA"
        );

        Customer customer1 = new Customer(
            "John Smith",
            address1
        );

        Product product1 = new Product(
            "Wireless Mouse",
            "P001",
            25.50,
            2
        );

        Product product2 = new Product(
            "Keyboard",
            "P002",
            45.00,
            1
        );

        Product product3 = new Product(
            "Laptop Stand",
            "P003",
            30.00,
            2
        );

        Order order1 = new Order(customer1);

        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Address address2 = new Address(
            "15 Admiralty Way",
            "Lagos",
            "Lagos State",
            "Nigeria"
        );

        Customer customer2 = new Customer(
            "Mary Johnson",
            address2
        );

        Product product4 = new Product(
            "USB-C Cable",
            "P004",
            12.00,
            3
        );

        Product product5 = new Product(
            "Webcam",
            "P005",
            55.00,
            1
        );

        Product product6 = new Product(
            "Headphones",
            "P006",
            40.00,
            2
        );

        Order order2 = new Order(customer2);

        order2.AddProduct(product4);
        order2.AddProduct(product5);
        order2.AddProduct(product6);

        Console.WriteLine("ORDER 1");
        Console.WriteLine();

        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());

        Console.WriteLine();
        Console.WriteLine(
            $"Total Price: ${order1.CalculateTotalCost():F2}"
        );

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();

        Console.WriteLine("ORDER 2");
        Console.WriteLine();

        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());

        Console.WriteLine();
        Console.WriteLine(
            $"Total Price: ${order2.CalculateTotalCost():F2}"
        );
    }
}