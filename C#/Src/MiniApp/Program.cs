namespace MiniApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, MiniApp!");
            // lista de productos
            var product1 = new Models.Products.Product(1, "Laptop", 999.99m, 5);
            var product2 = new Models.Products.Product(2, "Mouse", 25.50m, 0); // out of stock
            var productList = new CRUD.Lists.ProductList.ProductList();
            productList.CreateAsync(product1).Wait();
            productList.CreateAsync(product2).Wait();
            productList.DisplayProducts(false);
            productList.DisplayProducts(true);
        }
    }
}