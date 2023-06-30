using CarDealer.Data;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            //string inputJson = File.ReadAllText("../../../Datasets/parts.json");
            CarDealerContext db = new CarDealerContext();
            Console.WriteLine(GetSalesWithAppliedDiscount(db));
        }
        private static void ResetDatabase(CarDealerContext db)
        {
            db.Database.EnsureDeleted();
            Console.WriteLine("Database was successfully deleted!");
            db.Database.EnsureCreated();
            Console.WriteLine("Database was successfully created!");
        }
        //Problem 10
        public static string ImportDataFromParts(CarDealerContext context, string input)
        {
            Part[] parts = JsonConvert
                .DeserializeObject<Part[]>(input);
            List<Part> validParts = new List<Part>();
            foreach (var part in parts)
            {
                bool supplierExists = context.Suppliers.Any(s => s.Id == part.SupplierId);
                if (supplierExists)
                {
                    validParts.Add(part);
                }
            }
            context.Parts.AddRange(validParts);
            context.SaveChanges();
            
            return $"Successfully imported {validParts.Count}";
        }
        //Problem 14
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context
                .Customers
                .Select(x => new
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    IsYoungDriver = x.IsYoungDriver
                })
                .OrderBy (s => s.BirthDate)
                .ThenBy (s => s.IsYoungDriver == false)
                .ToArray();

            string stringJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            return stringJson;
        }
        //Problem 15
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context
                .Cars
                .Where(x => x.Make == "Toyota")
                .Select(x => new
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TraveledDistance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TraveledDistance)
                .ToArray();

            string inputJson = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return inputJson;
        }
        //Problem 16
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context
                .Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToArray();
            var stringJson = JsonConvert.SerializeObject(suppliers,
                Formatting.Indented);
            return stringJson;
        }
        //Problem 17
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var partsCar = context
                .PartsCars
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TraveledDistance = x.Car.TravelledDistance
                    },
                    parts = x.Part.PartsCars
                    .Select(x => new
                    {
                        Name = x.Part.Name,
                        Price = x.Part.Price.ToString("f2")
                    })
                    .ToArray()
                })
                .ToArray();
            var stringJson = JsonConvert.SerializeObject (partsCar, Formatting.Indented);
            return stringJson;
        }
        //Problem 19
        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context
                .Sales
                .Take(10)
                .Select(x => new
                {
                    car = new
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TraveledDistance = x.Car.TravelledDistance
                    },
                    customerName = x.Customer.Name,
                    discount = x.Discount,
                })
                .ToArray();
            var stringJson = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return stringJson;
        }
    }
}