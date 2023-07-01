using ProductShop.Data;
using System.Xml;
using XmlFacade;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using ProductShop.DTOs.Export;
using System.ComponentModel.DataAnnotations;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {

            using ProductShopContext context = new ProductShopContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();   
            var usersXml = File.ReadAllText("../../../Datasets/products.xml");

            Console.WriteLine(ImportUsers(context, usersXml));
        }
        //Problem 01
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            const string rootElement = "Users";

            var usersResult = XMLConverter.Deserializer<ImportUsersDto>(inputXml, rootElement);

            var users = usersResult
                .Select(x => new User
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age
                })
                .ToArray();
            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }
        //Problem 02
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            const string rootElement = "Products";

            var productsResult = XMLConverter.Deserializer<ImportProductsDto>(inputXml, rootElement);

            var products = productsResult
                .Select(x => new Product
                {
                    Name = x.Name,
                    Price = x.Price,
                    SellerId = x.sellerId,
                    BuyerId = x.buyerId
                })
                .ToArray();
            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }
        //Problem 03
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            const string rootElement = "Categories";

            var categoriesResult = XMLConverter.Deserializer<ImportCategoriesDto>(inputXml, rootElement);
            List<Category> categories = new List<Category>();
            var resultCategories = categoriesResult
                .Select(x => new Category
                {
                    Name = x.Name,
                })
                .ToArray();
            foreach (var item in resultCategories)
            {
                if(item.Name == null)
                {
                    continue;
                }
                var category = new Category
                {
                    Name = item.Name,
                };
                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count}";
        }
        //Problem 04
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            const string rootElement = "CategoryProducts";

            var categoryProductsResult = XMLConverter.Deserializer<ImportCategoryProductsDto>(inputXml , rootElement);

            var resultCategories = categoryProductsResult
                .Select (x => new CategoryProduct
                {
                    CategoryId = x.CategoryId,
                    ProductId = x.ProductId
                })
                .ToArray();
            context.CategoryProducts.AddRange(resultCategories);    
            context.SaveChanges();

            return $"Successfully imported {resultCategories.Length}";
        }
        //Problem 05
        public static string GetProductsInRange(ProductShopContext context)
        {
            const string rootElement = "Products";
            var products = context
                .Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ExportProducts
                {
                    Name = x.Name,
                    Price = x.Price,
                    Buyer = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .Take(10)
                .ToList();

            var result = XMLConverter.Serialize(products, rootElement);
            return result;
        }
        //Problem 06
        public static string GetSoldProducts(ProductShopContext context)
        {
            const string rootElement = "Users";
            var soldProducts = context
                .Users
                .Where(x => x.ProductsSold.Any())
                .Select(x => new ExportSoldProducts
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold
                    .Select(x => new UserProducts {
                        Name = x.Name,
                        price = x.Price
                    })
                    .ToArray()
                })
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .Take(5)
                .ToList();

            var output = XMLConverter.Serialize(soldProducts, rootElement);
            return output;
        }
        //Problem 07
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            const string rootElement = "Categories";

            var categories = context
                .Categories
                .Select(x => new ExportCategoriesByProductsDto
                {
                    Name = x.Name,
                    Count = x.CategoryProducts.Count(),
                    TotalRevenue = x.CategoryProducts
                    .Select(x => x.Product).Sum(x => x.Price),
                    AveragePrice = x.CategoryProducts
                    .Select(x => x.Product).Average(x => x.Price)
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToList();

            var output = XMLConverter.Serialize (categories, rootElement);
            return output;
        }
    }
}