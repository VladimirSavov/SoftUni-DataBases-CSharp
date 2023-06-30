using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext db = new ProductShopContext();

            string inputJson = File.ReadAllText("../../../Datasets/users.json");

            string result = ImportCategoryProducts(db, inputJson);

            Console.WriteLine(result);

        }
        private static void ResetDatabase(ProductShopContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");
        }
        //Problem 1
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] users = JsonConvert
                .DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);  
            context.SaveChanges();

            return $"Successfully imported {users.Length}";

        }
        //Problem 2
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            Product[] products = JsonConvert
                .DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }
        //Problem 3
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            Category[] categoryProducts = JsonConvert
                .DeserializeObject<Category[]>(inputJson)
                .Where(x => x.Name != null)
                .ToArray();

            context.Categories.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }
        //Problem 4
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            CategoryProduct[] categoryProducts = JsonConvert
                .DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }
        //Problem 5
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context
                .Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new
                {
                    name = x.Name,
                    price = x.Price,
                    seller = x.Seller.FirstName + " " + x.Seller.LastName    
                })
                .OrderBy(x => x.price)
                .ToList();
            string json = JsonConvert .SerializeObject(products, Formatting.Indented);
            return json;
        }
        //Problem 6
        public static string GetSoldProducts(ProductShopContext context)
        {
            var products = context
                .Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer != null))
                .Select(x => new
                {
                    firstName = x.FirstName,
                    lastName = x.LastName,
                    soldProducts = x.ProductsSold
                    .Where(x => x.Buyer != null)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyerFirstName = p.Buyer.FirstName,
                        buyerLastName = p.Buyer.LastName
                    })
                    .ToArray()
                })
                .OrderBy(x => x.lastName)
                .ThenBy(x => x.firstName)
                .ToArray();

            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            return json;
        }
        //Problem 7
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context
                .Categories
                .Select(x => new
                {
                    category = x.Name,
                    productsCount = x.CategoriesProducts.Count,
                    averagePrice = x.CategoriesProducts.Average(cp => cp.Product.Price).ToString("f2"),
                    totalRevenue = x.CategoriesProducts.Sum(cp => cp.Product.Price).ToString("f2")

                })
                .OrderByDescending(x => x.productsCount) 
                .ToArray();

            string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            return json;
        }
        //Problem 8
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context
                .Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer != null))
                .OrderByDescending(x => x.ProductsSold.Count)
                .Select(x => new
                {
                    lastName = x.LastName,
                    age = x.Age,
                    soldProduct = new
                    {
                        count = x.ProductsSold.Count(p => p.Buyer != null),
                        product = x.ProductsSold.Where(p => p.Buyer != null)
                        .Select(x => new
                        {
                            name = x.Name,
                            price = x.Price.ToString("f2")
                        })
                        .ToArray()

                    }
                })
                .OrderByDescending(p => p.soldProduct.count)
                .ToArray();

            var result = new
            {
                userCount = users.Length,
                users = users
            };
            JsonSerializerSettings settings = new JsonSerializerSettings 
            { 
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
            };
            string json = JsonConvert.SerializeObject(result, settings);
            return json;

        }
    }
}